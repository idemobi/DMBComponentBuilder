#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Renders a Bootstrap compatible mind map from a central <see cref="GDFMindMapNode" />.
    /// </summary>
    /// <remarks>
    ///     The component renders a central topic with balanced left and right branches, recursive child topics,
    ///     Bootstrap variants, optional icons, and empty or error states.
    /// </remarks>
    public sealed class GDFMindMapBuilder :
        HtmlBuilderBase<GDFMindMapBuilder>,
        ICanUseCustomClasses
    {
        #region Constants

        private const string MindMapCssPath = "/css/components/GDFMindMap.css";
        private const string MindMapJsPath = "/js/components/GDFMindMap.js";

        #endregion

        #region Static methods

        private static bool IsEmptyRoot(GDFMindMapNode root)
        {
            return string.IsNullOrWhiteSpace(root.Title) && root.Children.Count == 0;
        }

        private static IReadOnlyList<GDFMindMapNode> ResolveBranches(GDFMindMapNode root, GDFMindMapBranchSide side)
        {
            List<GDFMindMapNode> branches = new();

            for (int index = 0; index < root.Children.Count; index++)
            {
                GDFMindMapNode child = root.Children[index];
                GDFMindMapBranchSide resolvedSide = child.Side == GDFMindMapBranchSide.Auto
                    ? (index % 2 == 0 ? GDFMindMapBranchSide.Left : GDFMindMapBranchSide.Right)
                    : child.Side;

                if (resolvedSide == side)
                {
                    branches.Add(child);
                }
            }

            return branches;
        }

        private static string ResolveLineModeName(GDFMindMapLineMode lineMode)
        {
            return lineMode switch
            {
                GDFMindMapLineMode.Straight => "straight",
                GDFMindMapLineMode.Orthogonal => "orthogonal",
                GDFMindMapLineMode.WeightedBezier => "weighted-bezier",
                _ => "rounded-orthogonal"
            };
        }

        private static void WriteToolbarButton(TextWriter writer, string action, string iconCssClass, string title)
        {
            writer.Write($"<button type=\"button\" class=\"gdf-mind-map-tool\" data-gdf-mind-map-action=\"{WebUtility.HtmlEncode(action)}\" title=\"{WebUtility.HtmlEncode(title)}\">");
            writer.Write($"<span class=\"bi {WebUtility.HtmlEncode(iconCssClass)}\" aria-hidden=\"true\"></span>");
            writer.Write($"<span class=\"visually-hidden\">{WebUtility.HtmlEncode(title)}</span>");
            writer.Write("</button>");
        }

        #endregion

        #region Instance fields and properties

        private bool _autoFit = true;
        private bool _compact;
        private string? _emptyMessage = "No mind map topics to display.";
        private string? _errorMessage;
        private double _initialScale = 1;
        private GDFMindMapLineMode _lineMode = GDFMindMapLineMode.RoundedOrthogonal;

        private GDFMindMapNode? _root;
        private bool _showHeader = true;
        private bool _showZoomControls = true;
        private string? _title;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GDFMindMapBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer used by the current rendering context.</param>
        /// <param name="html">The HTML helper associated with the current Razor view.</param>
        public GDFMindMapBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "section";
            this.AddClass("gdf-mind-map");
            SetData("mind-map", "true");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds a branch to the current central topic.
        /// </summary>
        /// <param name="node">The branch topic to add.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder AddBranch(GDFMindMapNode node)
        {
            ArgumentNullException.ThrowIfNull(node);

            _root ??= new GDFMindMapNode(string.Empty);
            _root.AddChild(node.Clone());
            return this;
        }

        /// <inheritdoc />
        protected override GDFMindMapBuilder CreateInstance()
        {
            return new GDFMindMapBuilder(_textWriter, _htmlHelper);
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(MindMapCssPath, 10);
            page.SetScriptFile(MindMapJsPath, PageScriptLocation.EndOfBody, order: 10);
        }

        /// <inheritdoc />
        protected override void InternalClone(GDFMindMapBuilder source)
        {
            base.InternalClone(source);
            _root = source._root?.Clone();
            _title = source._title;
            _emptyMessage = source._emptyMessage;
            _errorMessage = source._errorMessage;
            _showHeader = source._showHeader;
            _showZoomControls = source._showZoomControls;
            _autoFit = source._autoFit;
            _compact = source._compact;
            _initialScale = source._initialScale;
            _lineMode = source._lineMode;
        }

        /// <summary>
        ///     Renders the mind map to an HTML content instance.
        /// </summary>
        /// <returns>The generated HTML content.</returns>
        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        private string ResolveState()
        {
            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                return "error";
            }

            if (_root == null || IsEmptyRoot(_root))
            {
                return "empty";
            }

            return "normal";
        }

        /// <summary>
        ///     Sets whether the mind map should fit itself to the viewport on first render.
        /// </summary>
        /// <param name="value">A value indicating whether the first render should fit the full map.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetAutoFit(bool value = true)
        {
            _autoFit = value;
            return this;
        }

        /// <summary>
        ///     Enables or disables compact spacing.
        /// </summary>
        /// <param name="value">A value indicating whether compact spacing should be used.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetCompact(bool value = true)
        {
            _compact = value;
            return this;
        }

        /// <summary>
        ///     Sets the message displayed when the mind map is empty.
        /// </summary>
        /// <param name="message">The empty-state message to display.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetEmptyMessage(string? message)
        {
            _emptyMessage = message;
            return this;
        }

        /// <summary>
        ///     Sets an error message displayed instead of the mind map content.
        /// </summary>
        /// <param name="message">The optional error message.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetErrorMessage(string? message)
        {
            _errorMessage = message;
            return this;
        }

        /// <summary>
        ///     Sets the initial mind map zoom scale.
        /// </summary>
        /// <param name="scale">The initial scale. Values are clamped between 0.4 and 2.4.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetInitialScale(double scale)
        {
            _initialScale = Math.Clamp(scale, 0.4, 2.4);
            _autoFit = false;
            return this;
        }

        /// <summary>
        ///     Sets the connector line rendering mode used between topics.
        /// </summary>
        /// <param name="lineMode">The connector line mode to apply.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetLineMode(GDFMindMapLineMode lineMode)
        {
            _lineMode = lineMode;
            return this;
        }

        /// <summary>
        ///     Sets the central topic and its child branches.
        /// </summary>
        /// <param name="root">The root topic to display.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetRoot(GDFMindMapNode? root)
        {
            _root = root?.Clone();
            return this;
        }

        /// <summary>
        ///     Shows or hides the header surface.
        /// </summary>
        /// <param name="value">A value indicating whether the header should be displayed.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetShowHeader(bool value = true)
        {
            _showHeader = value;
            return this;
        }

        /// <summary>
        ///     Shows or hides the zoom toolbar.
        /// </summary>
        /// <param name="value">A value indicating whether the zoom toolbar should be displayed.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetShowZoomControls(bool value = true)
        {
            _showZoomControls = value;
            return this;
        }

        /// <summary>
        ///     Sets the optional header title.
        /// </summary>
        /// <param name="title">The title displayed above the mind map.</param>
        /// <returns>The current builder instance.</returns>
        public GDFMindMapBuilder SetTitle(string? title)
        {
            _title = title;
            return this;
        }

        private void WriteBranch(
            TextWriter writer,
            HtmlEncoder encoder,
            GDFMindMapNode node,
            string path,
            int depth,
            string side
        )
        {
            string nodeId = string.IsNullOrWhiteSpace(node.Id)
                ? HtmlIdGenerator.CleanId(path) ?? path
                : HtmlIdGenerator.CleanId(node.Id) ?? path;
            string childrenId = $"{nodeId}_children";
            bool hasChildren = node.Children.Count > 0;
            bool expanded = hasChildren && !node.IsCollapsed;

            writer.Write($"<div class=\"gdf-mind-map-branch gdf-mind-map-branch-{WebUtility.HtmlEncode(side)}\" data-gdf-mind-map-branch=\"true\" data-gdf-mind-map-side=\"{WebUtility.HtmlEncode(side)}\" aria-expanded=\"{expanded.ToString().ToLowerInvariant()}\" style=\"--gdf-mind-map-depth:{depth};\">");
            WriteTopic(writer, encoder, node, nodeId, depth, false, hasChildren, childrenId, expanded);

            if (hasChildren)
            {
                string hidden = expanded ? string.Empty : " hidden";
                writer.Write($"<div id=\"{WebUtility.HtmlEncode(childrenId)}\" class=\"gdf-mind-map-children\" data-gdf-mind-map-children=\"true\"{hidden}>");

                for (int index = 0; index < node.Children.Count; index++)
                {
                    WriteBranch(writer, encoder, node.Children[index], $"{nodeId}_{index}", depth + 1, side);
                }

                writer.Write("</div>");
            }

            writer.Write("</div>");
        }

        private void WriteBranchColumn(
            TextWriter writer,
            HtmlEncoder encoder,
            IReadOnlyList<GDFMindMapNode> branches,
            string side,
            string mindMapId
        )
        {
            writer.Write($"<div class=\"gdf-mind-map-branches gdf-mind-map-branches-{WebUtility.HtmlEncode(side)}\">");

            for (int index = 0; index < branches.Count; index++)
            {
                WriteBranch(writer, encoder, branches[index], $"{mindMapId}_{side}_{index}", 1, side);
            }

            writer.Write("</div>");
        }

        private void WriteHeader(TextWriter writer)
        {
            if (!_showHeader)
            {
                return;
            }

            string title = string.IsNullOrWhiteSpace(_title) ? "Mind map" : _title;

            writer.Write("<div class=\"gdf-mind-map-header\">");
            writer.Write("<span class=\"gdf-mind-map-header-icon bi bi-diagram-3\" aria-hidden=\"true\"></span>");
            writer.Write("<span class=\"gdf-mind-map-title\">");
            writer.Write(WebUtility.HtmlEncode(title));
            writer.Write("</span>");
            writer.Write("</div>");
        }

        private void WriteMap(TextWriter writer, HtmlEncoder encoder, GDFMindMapNode root, string mindMapId)
        {
            IReadOnlyList<GDFMindMapNode> left = ResolveBranches(root, GDFMindMapBranchSide.Left);
            IReadOnlyList<GDFMindMapNode> right = ResolveBranches(root, GDFMindMapBranchSide.Right);

            WriteToolbar(writer);
            writer.Write($"<div class=\"gdf-mind-map-viewport\" data-gdf-mind-map-viewport=\"true\" data-gdf-mind-map-line-mode=\"{ResolveLineModeName(_lineMode)}\" data-gdf-mind-map-initial-scale=\"{_initialScale.ToString(System.Globalization.CultureInfo.InvariantCulture)}\" data-gdf-mind-map-auto-fit=\"{_autoFit.ToString().ToLowerInvariant()}\">");
            writer.Write("<canvas class=\"gdf-mind-map-lines\" data-gdf-mind-map-lines=\"true\" aria-hidden=\"true\"></canvas>");
            writer.Write("<div class=\"gdf-mind-map-canvas\" data-gdf-mind-map-stage=\"true\">");
            WriteBranchColumn(writer, encoder, left, "left", mindMapId);
            writer.Write("<div class=\"gdf-mind-map-center\" aria-label=\"Central topic\">");
            WriteTopic(writer, encoder, root, $"{mindMapId}_root", 0, true, false, null, true);
            writer.Write("</div>");
            WriteBranchColumn(writer, encoder, right, "right", mindMapId);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteState(TextWriter writer, string state, string? message, string iconCssClass)
        {
            writer.Write($"<div class=\"gdf-mind-map-state gdf-mind-map-state-{WebUtility.HtmlEncode(state)}\">");
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

            string mindMapId = string.IsNullOrWhiteSpace(GetId())
                ? _htmlHelper.GenerateUniqueId("gdf_mind_map_")
                : GetId();

            SetId(mindMapId);
            SetData("mind-map-state", ResolveState());

            if (_compact)
            {
                this.AddClass("gdf-mind-map-compact");
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            WriteHeader(writer);

            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                WriteState(writer, "error", _errorMessage, "bi-exclamation-triangle");
            }
            else if (_root == null || IsEmptyRoot(_root))
            {
                WriteState(writer, "empty", _emptyMessage, "bi-diagram-3");
            }
            else
            {
                WriteMap(writer, encoder, _root, mindMapId);
            }

            writer.Write($"</{GetTag()}>");
        }

        private void WriteToolbar(TextWriter writer)
        {
            if (!_showZoomControls)
            {
                return;
            }

            writer.Write("<div class=\"gdf-mind-map-toolbar\" data-gdf-mind-map-toolbar=\"true\" aria-label=\"Mind map zoom controls\">");
            WriteToolbarButton(writer, "zoom-out", "bi-zoom-out", "Zoom out");
            writer.Write("<output class=\"gdf-mind-map-zoom-label\" data-gdf-mind-map-zoom-label=\"true\" aria-live=\"polite\">100%</output>");
            WriteToolbarButton(writer, "zoom-in", "bi-zoom-in", "Zoom in");
            WriteToolbarButton(writer, "fit", "bi-aspect-ratio", "Fit to view");
            WriteToolbarButton(writer, "reset", "bi-arrow-counterclockwise", "Reset view");
            writer.Write("</div>");
        }

        private void WriteTopic(
            TextWriter writer,
            HtmlEncoder encoder,
            GDFMindMapNode node,
            string nodeId,
            int depth,
            bool isRoot,
            bool hasChildren,
            string? childrenId,
            bool expanded
        )
        {
            string variant = node.Variant.GetVariantCss();
            string customClass = string.IsNullOrWhiteSpace(node.CssClass) ? string.Empty : $" {WebUtility.HtmlEncode(node.CssClass)}";
            string iconClass = node.Icon.IsEmpty ? " gdf-mind-map-topic-no-icon" : " gdf-mind-map-topic-has-icon";
            string childrenClass = hasChildren ? " gdf-mind-map-topic-has-children" : string.Empty;

            if (string.IsNullOrWhiteSpace(variant))
            {
                variant = "primary";
            }

            writer.Write($"<article id=\"{WebUtility.HtmlEncode(nodeId)}\" class=\"gdf-mind-map-topic gdf-mind-map-topic-{WebUtility.HtmlEncode(variant)}{iconClass}{childrenClass}{customClass}\" data-gdf-mind-map-topic=\"true\" style=\"--gdf-mind-map-depth:{depth};\">");

            if (hasChildren && !string.IsNullOrWhiteSpace(childrenId))
            {
                writer.Write($"<button type=\"button\" class=\"gdf-mind-map-toggle\" aria-expanded=\"{expanded.ToString().ToLowerInvariant()}\" aria-controls=\"{WebUtility.HtmlEncode(childrenId)}\" data-gdf-mind-map-toggle=\"true\" title=\"Toggle topic children\">");
                writer.Write("<span class=\"bi bi-chevron-down\" aria-hidden=\"true\"></span>");
                writer.Write("<span class=\"visually-hidden\">Toggle topic children</span>");
                writer.Write("</button>");
            }

            if (!node.Icon.IsEmpty)
            {
                writer.Write("<span class=\"gdf-mind-map-topic-icon\" aria-hidden=\"true\">");
                HtmlLayoutExtensions.IconBuilder(_htmlHelper, node.Icon, null, null).WriteTo(writer, HtmlEncoder.Default);
                writer.Write("</span>");
            }

            writer.Write(isRoot ? "<strong class=\"gdf-mind-map-topic-title\">" : "<span class=\"gdf-mind-map-topic-title\">");
            encoder.Encode(writer, string.IsNullOrWhiteSpace(node.Title) ? "(untitled)" : node.Title);
            writer.Write(isRoot ? "</strong>" : "</span>");

            if (!string.IsNullOrWhiteSpace(node.Detail))
            {
                writer.Write("<span class=\"gdf-mind-map-topic-detail\">");
                encoder.Encode(writer, node.Detail);
                writer.Write("</span>");
            }

            writer.Write("</article>");
        }

        #endregion
    }
}