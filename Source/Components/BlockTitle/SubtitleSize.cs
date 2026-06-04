#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Controls the visual size of a subtitle line inside a <see cref="BlockTitleBuilder" />.
    /// </summary>
    public enum SubtitleSize
    {
        /// <summary>Standard muted paragraph text.</summary>
        Normal = 0,

        /// <summary>Smaller muted text, suitable for secondary annotations.</summary>
        Small = 1,

        /// <summary>Larger lead text, suitable for a prominent tagline.</summary>
        Lead = 2
    }
}