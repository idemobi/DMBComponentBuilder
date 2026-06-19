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
    ///     Represents a compact badge rendered in the metadata row of a <see cref="ConversationMessage" />.
    /// </summary>
    [Documented]
    public sealed class ConversationMessageBadge
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ConversationMessageBadge" /> class.
        /// </summary>
        [Documented]
        public ConversationMessageBadge()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConversationMessageBadge" /> class.
        /// </summary>
        /// <param name="text">The badge text.</param>
        /// <param name="variant">The Bootstrap variant used to render the badge.</param>
        [Documented]
        public ConversationMessageBadge(string? text, VariantStyle variant = VariantStyle.Secondary)
        {
            Text = text ?? string.Empty;
            Variant = variant;
        }

        /// <summary>
        ///     Gets or sets the badge text.
        /// </summary>
        [Documented]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Bootstrap variant used to render the badge.
        /// </summary>
        [Documented]
        public VariantStyle Variant { get; set; } = VariantStyle.Secondary;

        /// <summary>
        ///     Creates a copy of this badge model.
        /// </summary>
        /// <returns>A new <see cref="ConversationMessageBadge" /> carrying the same values.</returns>
        [Documented]
        public ConversationMessageBadge Clone()
        {
            return new ConversationMessageBadge
            {
                Text = Text,
                Variant = Variant
            };
        }
    }
}
