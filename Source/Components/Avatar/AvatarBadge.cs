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
    ///     Represents a short badge attached to an <see cref="AvatarBuilder" />.
    /// </summary>
    /// <remarks>
    ///     Badges are designed for compact contextual markers such as administrator, moderator, verified, or status labels.
    /// </remarks>
    [Documented]
    public sealed class AvatarBadge
    {
        /// <summary>
        ///     Gets or sets the badge text.
        /// </summary>
        [Documented]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Bootstrap variant used for the badge.
        /// </summary>
        [Documented]
        public VariantStyle Variant { get; set; } = VariantStyle.Danger;

        /// <summary>
        ///     Creates a shallow copy of the badge.
        /// </summary>
        /// <returns>A new <see cref="AvatarBadge" /> with the same values.</returns>
        [Documented]
        public AvatarBadge Clone()
        {
            return new AvatarBadge
            {
                Text = Text,
                Variant = Variant
            };
        }
    }
}
