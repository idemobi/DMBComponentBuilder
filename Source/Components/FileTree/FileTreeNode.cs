#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Describes one file or folder displayed by <see cref="FileTreeBuilder" />.
    /// </summary>
    /// <remarks>
    ///     Use <see cref="Folder" /> and <see cref="File" /> to create readable tree models for Razor examples.
    /// </remarks>
    public sealed class FileTreeNode
    {
        #region Static methods

        /// <summary>
        ///     Creates a file node.
        /// </summary>
        /// <param name="name">The file display name.</param>
        /// <returns>A configured file node.</returns>
        public static FileTreeNode File(string? name)
        {
            return new FileTreeNode(name, false);
        }

        /// <summary>
        ///     Creates a folder node.
        /// </summary>
        /// <param name="name">The folder display name.</param>
        /// <param name="children">The optional folder children.</param>
        /// <returns>A configured folder node.</returns>
        public static FileTreeNode Folder(string? name, IEnumerable<FileTreeNode>? children = null)
        {
            return new FileTreeNode(name, true, children);
        }

        #endregion

        #region Instance fields and properties

        private readonly List<FileTreeNode> _children = new();

        /// <summary>
        ///     Gets the child nodes displayed below this folder.
        /// </summary>
        public IReadOnlyList<FileTreeNode> Children => _children;

        /// <summary>
        ///     Gets or sets the optional custom CSS class applied to the node row.
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        ///     Gets or sets the optional accessible description displayed as a muted detail.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     Gets or sets the optional Bootstrap Icons CSS class used instead of the default icon.
        /// </summary>
        public string? IconCssClass { get; set; }

        /// <summary>
        ///     Gets or sets the optional HTML id seed used for this node.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the node is a folder.
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        ///     Gets a value indicating whether the folder name starts with a dot.
        /// </summary>
        public bool IsSpecialDirectory => IsDirectory && Name.StartsWith(".", StringComparison.Ordinal);

        /// <summary>
        ///     Gets or sets the file or folder display name.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileTreeNode" /> class.
        /// </summary>
        /// <param name="name">The file or folder display name.</param>
        /// <param name="isDirectory">A value indicating whether the node is a folder.</param>
        /// <param name="children">The optional folder children.</param>
        public FileTreeNode(string? name, bool isDirectory, IEnumerable<FileTreeNode>? children = null)
        {
            Name = name ?? string.Empty;
            IsDirectory = isDirectory;

            if (children != null)
            {
                _children.AddRange(children);
            }
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds one child to the current folder node.
        /// </summary>
        /// <param name="node">The child node to add.</param>
        /// <returns>The current node instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the current node is not a folder.</exception>
        public FileTreeNode AddChild(FileTreeNode node)
        {
            if (!IsDirectory)
            {
                throw new InvalidOperationException("Only folder nodes can contain children.");
            }

            _children.Add(node);
            return this;
        }

        /// <summary>
        ///     Creates a copy of the current node and its descendants.
        /// </summary>
        /// <returns>A cloned node tree.</returns>
        public FileTreeNode Clone()
        {
            FileTreeNode clone = new(Name, IsDirectory, _children.Select(child => child.Clone()))
            {
                Description = Description,
                IconCssClass = IconCssClass,
                Id = Id,
                CssClass = CssClass
            };

            return clone;
        }

        /// <summary>
        ///     Sets the optional description displayed next to the node name.
        /// </summary>
        /// <param name="description">The description to display.</param>
        /// <returns>The current node instance.</returns>
        public FileTreeNode SetDescription(string? description)
        {
            Description = description;
            return this;
        }

        /// <summary>
        ///     Sets the optional Bootstrap Icons CSS class for this node.
        /// </summary>
        /// <param name="iconCssClass">The Bootstrap Icons CSS class.</param>
        /// <returns>The current node instance.</returns>
        public FileTreeNode SetIconCssClass(string? iconCssClass)
        {
            IconCssClass = iconCssClass;
            return this;
        }

        #endregion
    }
}