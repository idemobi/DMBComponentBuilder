#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj SubtitleSize.cs create at 2026/05/19
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Controls the visual size of a subtitle line inside a <see cref="BlockTitleBuilder"/>.
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
