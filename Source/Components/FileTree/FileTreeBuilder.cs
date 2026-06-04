#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Renders a Bootstrap compatible Finder-style file and folder tree.
    /// </summary>
    /// <remarks>
    ///     The component accepts a tree of <see cref="FileTreeNode" /> instances and renders folders
    ///     with collapsible child groups, Bootstrap Icons, and lighter visual treatment for dot folders.
    /// </remarks>
    public sealed class FileTreeBuilder :
        HtmlBuilderBase<FileTreeBuilder>,
        ICanUseCustomClasses
    {
        #region Constants

        private const string FileTreeCssPath = "/css/components/FileTree.css";
        private const string FileTreeJsPath = "/js/components/FileTree.js";

        #endregion

        #region Static methods

        private static string GetIconClass(FileTreeNode node, bool expanded)
        {
            if (!string.IsNullOrWhiteSpace(node.IconCssClass))
            {
                return node.IconCssClass;
            }

            if (node.IsDirectory)
            {
                return expanded ? "bi-folder2-open" : "bi-folder2";
            }

            string extension = Path.GetExtension(node.Name).ToLowerInvariant();
            return extension switch
            {
                ".cs" => "bi-filetype-cs",
                ".css" => "bi-filetype-css",
                ".html" or ".cshtml" => "bi-filetype-html",
                ".js" => "bi-filetype-js",
                ".json" => "bi-filetype-json",
                ".md" => "bi-filetype-md",
                ".png" or ".jpg" or ".jpeg" or ".gif" or ".webp" => "bi-file-image",
                ".sln" or ".slnx" or ".csproj" => "bi-file-earmark-code",
                _ => "bi-file-earmark"
            };
        }

        #endregion

        #region Instance fields and properties

        private string? _emptyMessage = "No files to display.";
        private string? _errorMessage;
        private bool _expandedByDefault = true;

        private readonly List<FileTreeNode> _nodes = new();
        private bool _showHeader = true;
        private string? _title;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileTreeBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer used by the current rendering context.</param>
        /// <param name="html">The HTML helper associated with the current Razor view.</param>
        public FileTreeBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "section";
            this.AddClass("file-tree");
            SetData("file-tree", "true");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds one root node to the tree.
        /// </summary>
        /// <param name="node">The root node to add.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder AddNode(FileTreeNode node)
        {
            _nodes.Add(node.Clone());
            return this;
        }

        /// <inheritdoc />
        protected override FileTreeBuilder CreateInstance()
        {
            return new FileTreeBuilder(_textWriter, _htmlHelper);
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(FileTreeCssPath, 10);
            page.SetScriptFile(FileTreeJsPath, PageScriptLocation.EndOfBody, order: 10);
        }

        /// <inheritdoc />
        protected override void InternalClone(FileTreeBuilder source)
        {
            base.InternalClone(source);
            _nodes.Clear();
            _nodes.AddRange(source._nodes.Select(node => node.Clone()));
            _title = source._title;
            _emptyMessage = source._emptyMessage;
            _errorMessage = source._errorMessage;
            _expandedByDefault = source._expandedByDefault;
            _showHeader = source._showHeader;
        }

        /// <summary>
        ///     Renders the file tree to an HTML content instance.
        /// </summary>
        /// <returns>The generated HTML content.</returns>
        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        /// <summary>
        ///     Sets the message displayed when the tree is empty.
        /// </summary>
        /// <param name="message">The empty-state message to display.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder SetEmptyMessage(string? message)
        {
            _emptyMessage = message;
            return this;
        }

        /// <summary>
        ///     Sets an error message displayed instead of the tree content.
        /// </summary>
        /// <param name="message">The optional error message.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder SetErrorMessage(string? message)
        {
            _errorMessage = message;
            return this;
        }

        /// <summary>
        ///     Sets whether folders are expanded during the first render.
        /// </summary>
        /// <param name="value">A value indicating whether folders should start expanded.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder SetExpandedByDefault(bool value = true)
        {
            _expandedByDefault = value;
            return this;
        }

        /// <summary>
        ///     Replaces the displayed root nodes.
        /// </summary>
        /// <param name="nodes">The root file and folder nodes.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder SetNodes(IEnumerable<FileTreeNode>? nodes)
        {
            _nodes.Clear();

            if (nodes != null)
            {
                _nodes.AddRange(nodes.Select(node => node.Clone()));
            }

            return this;
        }

        /// <summary>
        ///     Shows or hides the header surface.
        /// </summary>
        /// <param name="value">A value indicating whether the header should be displayed.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder SetShowHeader(bool value = true)
        {
            _showHeader = value;
            return this;
        }

        /// <summary>
        ///     Sets the optional header title.
        /// </summary>
        /// <param name="title">The title displayed above the tree.</param>
        /// <returns>The current builder instance.</returns>
        public FileTreeBuilder SetTitle(string? title)
        {
            _title = title;
            return this;
        }

        private void WriteHeader(TextWriter writer)
        {
            if (!_showHeader)
            {
                return;
            }

            string title = string.IsNullOrWhiteSpace(_title) ? "Files" : _title;

            writer.Write("<div class=\"file-tree-header\">");
            writer.Write("<span class=\"file-tree-header-icon bi bi-window-sidebar\" aria-hidden=\"true\"></span>");
            writer.Write("<span class=\"file-tree-title\">");
            writer.Write(WebUtility.HtmlEncode(title));
            writer.Write("</span>");
            writer.Write("</div>");
        }

        private void WriteNode(TextWriter writer, HtmlEncoder encoder, FileTreeNode node, int depth, string path)
        {
            string nodeId = string.IsNullOrWhiteSpace(node.Id)
                ? HtmlIdGenerator.CleanId(path) ?? path
                : HtmlIdGenerator.CleanId(node.Id) ?? path;
            string rowId = $"{nodeId}_row";
            string childrenId = $"{nodeId}_children";
            bool hasChildren = node.IsDirectory && node.Children.Count > 0;
            bool expanded = hasChildren && _expandedByDefault;
            string kindClass = node.IsDirectory ? "file-tree-folder" : "file-tree-file";
            string specialClass = node.IsSpecialDirectory ? " file-tree-special" : string.Empty;
            string customClass = string.IsNullOrWhiteSpace(node.CssClass) ? string.Empty : $" {WebUtility.HtmlEncode(node.CssClass)}";
            string iconClass = GetIconClass(node, expanded);

            writer.Write($"<li class=\"file-tree-item {kindClass}{specialClass}{customClass}\" role=\"treeitem\" aria-expanded=\"{expanded.ToString().ToLowerInvariant()}\" data-file-tree-kind=\"{(node.IsDirectory ? "folder" : "file")}\">");
            writer.Write($"<div id=\"{WebUtility.HtmlEncode(rowId)}\" class=\"file-tree-row\" style=\"--file-tree-depth:{depth};\">");

            if (hasChildren)
            {
                writer.Write($"<button type=\"button\" class=\"file-tree-toggle\" aria-expanded=\"{expanded.ToString().ToLowerInvariant()}\" aria-controls=\"{WebUtility.HtmlEncode(childrenId)}\" data-file-tree-toggle=\"true\" title=\"Toggle folder\">");
                writer.Write("<span class=\"bi bi-chevron-down file-tree-chevron\" aria-hidden=\"true\"></span>");
                writer.Write("<span class=\"visually-hidden\">Toggle folder</span>");
                writer.Write("</button>");
            }
            else
            {
                writer.Write("<span class=\"file-tree-toggle-spacer\" aria-hidden=\"true\"></span>");
            }

            writer.Write($"<span class=\"file-tree-icon bi {WebUtility.HtmlEncode(iconClass)}\" aria-hidden=\"true\"></span>");
            writer.Write("<span class=\"file-tree-label\">");
            encoder.Encode(writer, string.IsNullOrWhiteSpace(node.Name) ? "(untitled)" : node.Name);
            writer.Write("</span>");

            if (!string.IsNullOrWhiteSpace(node.Description))
            {
                writer.Write("<span class=\"file-tree-description\">");
                encoder.Encode(writer, node.Description);
                writer.Write("</span>");
            }

            writer.Write("</div>");

            if (hasChildren)
            {
                string hidden = expanded ? string.Empty : " hidden";
                writer.Write($"<ul id=\"{WebUtility.HtmlEncode(childrenId)}\" class=\"file-tree-list file-tree-children\" role=\"group\"{hidden}>");
                for (int index = 0; index < node.Children.Count; index++)
                {
                    WriteNode(writer, encoder, node.Children[index], depth + 1, $"{path}_{index}");
                }

                writer.Write("</ul>");
            }

            writer.Write("</li>");
        }

        private void WriteState(TextWriter writer, string state, string? message, string iconCssClass)
        {
            writer.Write($"<div class=\"file-tree-state file-tree-state-{WebUtility.HtmlEncode(state)}\">");
            writer.Write($"<span class=\"bi {WebUtility.HtmlEncode(iconCssClass)}\" aria-hidden=\"true\"></span>");
            writer.Write("<span>");
            writer.Write(WebUtility.HtmlEncode(message ?? string.Empty));
            writer.Write("</span>");
            writer.Write("</div>");
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            string treeId = string.IsNullOrWhiteSpace(GetId())
                ? _htmlHelper.GenerateUniqueId("file_tree_")
                : GetId();

            SetId(treeId);

            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                SetData("file-tree-state", "error");
            }
            else if (_nodes.Count == 0)
            {
                SetData("file-tree-state", "empty");
            }
            else
            {
                SetData("file-tree-state", "normal");
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            WriteHeader(writer);

            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                WriteState(writer, "error", _errorMessage, "bi-exclamation-triangle");
            }
            else if (_nodes.Count == 0)
            {
                WriteState(writer, "empty", _emptyMessage, "bi-folder-x");
            }
            else
            {
                writer.Write("<ul class=\"file-tree-list\" role=\"tree\">");
                for (int index = 0; index < _nodes.Count; index++)
                {
                    WriteNode(writer, encoder, _nodes[index], 0, $"{treeId}_{index}");
                }

                writer.Write("</ul>");
            }

            writer.Write($"</{GetTag()}>");
        }

        #endregion
    }
}