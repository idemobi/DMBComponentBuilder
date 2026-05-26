#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj BlockTitleSubtitle.cs create at 2026/05/19
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Represents a single subtitle line in a <see cref="BlockTitleBuilder"/>.
    /// </summary>
    internal sealed class BlockTitleSubtitle
    {
        /// <summary>The subtitle text content.</summary>
        internal string Text { get; set; } = string.Empty;

        /// <summary>The display size of the subtitle.</summary>
        internal SubtitleSize Size { get; set; } = SubtitleSize.Normal;

        /// <summary>Overrides the global text color for this subtitle line. Null inherits the builder-level variant.</summary>
        internal VariantStyle? TextVariant { get; set; }

        /// <summary>Overrides the global text shadow for this subtitle line. Null inherits the builder-level shadow.</summary>
        internal Shadow? TextShadow { get; set; }
    }
}
