#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Represents a single item in a <see cref="TodoBlockBuilder" />.
    /// </summary>
    internal sealed class TodoBlockItem
    {
        #region Instance fields and properties

        /// <summary>Whether this item is checked. Only relevant when <see cref="TodoBlockBuilder" /> has checkable mode enabled.</summary>
        internal bool Checked { get; set; }

        /// <summary>The Bootstrap icon class or custom icon for this item.</summary>
        internal IconStruct Icon { get; set; } = IconStruct.Empty;

        /// <summary>The Bootstrap variant used to color the icon.</summary>
        internal VariantStyle IconVariant { get; set; } = VariantStyle.Warning;

        /// <summary>The item subtitle displayed as secondary text.</summary>
        internal string Subtitle { get; set; } = string.Empty;

        /// <summary>The item title displayed in bold.</summary>
        internal string Title { get; set; } = string.Empty;

        #endregion
    }
}