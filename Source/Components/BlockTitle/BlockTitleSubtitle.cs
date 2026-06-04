#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Represents a single subtitle line in a <see cref="BlockTitleBuilder" />.
    /// </summary>
    internal sealed class BlockTitleSubtitle
    {
        #region Instance fields and properties

        /// <summary>The display size of the subtitle.</summary>
        internal SubtitleSize Size { get; set; } = SubtitleSize.Normal;

        /// <summary>The subtitle text content.</summary>
        internal string Text { get; set; } = string.Empty;

        /// <summary>Overrides the global text shadow for this subtitle line. Null inherits the builder-level shadow.</summary>
        internal Shadow? TextShadow { get; set; }

        /// <summary>Overrides the global text color for this subtitle line. Null inherits the builder-level variant.</summary>
        internal VariantStyle? TextVariant { get; set; }

        #endregion
    }
}