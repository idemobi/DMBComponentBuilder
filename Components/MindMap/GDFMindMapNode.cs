#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj GDFMindMapNode.cs create at 2026/05/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Describes one topic displayed by <see cref="GDFMindMapBuilder"/>.
    /// </summary>
    /// <remarks>
    /// Use <see cref="Topic"/> and <see cref="AddChild"/> to create readable mind map trees in Razor examples.
    /// </remarks>
    public sealed class GDFMindMapNode
    {
        private readonly List<GDFMindMapNode> _children = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="GDFMindMapNode"/> class.
        /// </summary>
        /// <param name="title">The topic title.</param>
        /// <param name="children">The optional child topics.</param>
        public GDFMindMapNode(string? title, IEnumerable<GDFMindMapNode>? children = null)
        {
            Title = title ?? string.Empty;

            if (children != null)
            {
                _children.AddRange(children);
            }
        }

        /// <summary>
        /// Gets or sets the topic title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the optional topic detail.
        /// </summary>
        public string? Detail { get; set; }

        /// <summary>
        /// Gets or sets the optional HTML id seed used for the rendered topic.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Gets or sets the optional custom CSS class applied to the rendered topic.
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// Gets or sets the optional Bootstrap icon displayed before the topic title.
        /// </summary>
        public IconStruct Icon { get; set; }

        /// <summary>
        /// Gets or sets the Bootstrap variant used for the topic accent.
        /// </summary>
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;

        /// <summary>
        /// Gets or sets the preferred visual side for a root branch.
        /// </summary>
        public GDFMindMapBranchSide Side { get; set; } = GDFMindMapBranchSide.Auto;

        /// <summary>
        /// Gets or sets a value indicating whether child topics start collapsed.
        /// </summary>
        public bool IsCollapsed { get; set; }

        /// <summary>
        /// Gets the child topics displayed below this node.
        /// </summary>
        public IReadOnlyList<GDFMindMapNode> Children => _children;

        /// <summary>
        /// Creates a mind map topic.
        /// </summary>
        /// <param name="title">The topic title.</param>
        /// <param name="children">The optional child topics.</param>
        /// <returns>A configured mind map topic.</returns>
        public static GDFMindMapNode Topic(string? title, IEnumerable<GDFMindMapNode>? children = null)
        {
            return new GDFMindMapNode(title, children);
        }

        /// <summary>
        /// Adds one child topic to the current node.
        /// </summary>
        /// <param name="node">The child topic to add.</param>
        /// <returns>The current node instance.</returns>
        public GDFMindMapNode AddChild(GDFMindMapNode node)
        {
            ArgumentNullException.ThrowIfNull(node);
            _children.Add(node);
            return this;
        }

        /// <summary>
        /// Sets the optional detail displayed below the topic title.
        /// </summary>
        /// <param name="detail">The detail to display.</param>
        /// <returns>The current node instance.</returns>
        public GDFMindMapNode SetDetail(string? detail)
        {
            Detail = detail;
            return this;
        }

        /// <summary>
        /// Sets the optional Bootstrap icon displayed before the topic title.
        /// </summary>
        /// <param name="icon">The icon to display.</param>
        /// <returns>The current node instance.</returns>
        public GDFMindMapNode SetIcon(IconStruct icon)
        {
            Icon = icon;
            return this;
        }

        /// <summary>
        /// Sets the Bootstrap variant used for the topic accent.
        /// </summary>
        /// <param name="variant">The variant to apply.</param>
        /// <returns>The current node instance.</returns>
        public GDFMindMapNode SetVariant(VariantStyle variant)
        {
            Variant = variant;
            return this;
        }

        /// <summary>
        /// Sets the preferred visual side for a root branch.
        /// </summary>
        /// <param name="side">The preferred branch side.</param>
        /// <returns>The current node instance.</returns>
        public GDFMindMapNode SetSide(GDFMindMapBranchSide side)
        {
            Side = side;
            return this;
        }

        /// <summary>
        /// Sets whether child topics start collapsed.
        /// </summary>
        /// <param name="value">A value indicating whether the topic should start collapsed.</param>
        /// <returns>The current node instance.</returns>
        public GDFMindMapNode SetCollapsed(bool value = true)
        {
            IsCollapsed = value;
            return this;
        }

        /// <summary>
        /// Creates a copy of the current topic and its descendants.
        /// </summary>
        /// <returns>A cloned topic tree.</returns>
        public GDFMindMapNode Clone()
        {
            GDFMindMapNode clone = new(Title, _children.Select(child => child.Clone()))
            {
                Detail = Detail,
                Id = Id,
                CssClass = CssClass,
                Icon = Icon,
                Variant = Variant,
                Side = Side,
                IsCollapsed = IsCollapsed
            };

            return clone;
        }
    }
}
