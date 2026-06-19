#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using DMBBootstrapBuilder;

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Defines the visual size of an <see cref="AvatarBuilder" />.
    /// </summary>
    [Documented]
    public enum AvatarSize
    {
        /// <summary>
        ///     Renders a compact avatar for dense lists and table-like surfaces.
        /// </summary>
        [Documented]
        Small,

        /// <summary>
        ///     Renders the default avatar size for conversations and profile summaries.
        /// </summary>
        [Documented]
        Medium,

        /// <summary>
        ///     Renders a larger avatar for profile previews and editorial layouts.
        /// </summary>
        [Documented]
        Large,

        /// <summary>
        ///     Renders an extra-large avatar for prominent profile displays.
        /// </summary>
        [Documented]
        ExtraLarge
    }
}
