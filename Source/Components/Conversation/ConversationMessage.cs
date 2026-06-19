#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Represents one message rendered by a <see cref="ConversationBuilder" />.
    /// </summary>
    /// <remarks>
    ///     The message model is intentionally domain neutral so it can represent support tickets,
    ///     forum threads, moderation exchanges, or any participant-based conversation.
    /// </remarks>
    [Documented]
    public sealed class ConversationMessage
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets additional HTML content rendered below the message bubble.
        /// </summary>
        /// <remarks>
        ///     Use this slot for attachments, moderation controls, reaction summaries, or domain-specific actions.
        /// </remarks>
        [Documented]
        public IHtmlContent? AccessoryContent { get; set; }

        /// <summary>
        ///     Gets or sets the display name of the participant who wrote the message.
        /// </summary>
        [Documented]
        public string AuthorName { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets an optional participant role, team, or contextual subtitle.
        /// </summary>
        [Documented]
        public string AuthorSubtitle { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the short badge text rendered on the top-right corner of the avatar.
        /// </summary>
        /// <remarks>
        ///     Keep this text short because the badge is attached to the avatar and must remain readable on small screens.
        /// </remarks>
        [Documented]
        public string AvatarBadgeText { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the color variant used by the optional avatar badge.
        /// </summary>
        [Documented]
        public VariantStyle AvatarBadgeVariant { get; set; } = VariantStyle.Danger;

        /// <summary>
        ///     Gets or sets the message bubble variant.
        /// </summary>
        /// <remarks>
        ///     When left unset, current-participant messages use the builder current variant and other messages use a neutral bubble.
        /// </remarks>
        [Documented]
        public VariantStyle? BubbleVariant { get; set; }

        /// <summary>
        ///     Gets or sets the message creation date.
        /// </summary>
        [Documented]
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        ///     Gets or sets an explicit date label.
        /// </summary>
        /// <remarks>
        ///     When this value is empty and <see cref="CreatedAt" /> is set, the builder renders the local short date/time value.
        /// </remarks>
        [Documented]
        public string DateText { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets trusted HTML content rendered inside the message bubble.
        /// </summary>
        /// <remarks>
        ///     Prefer <see cref="Text" /> for user-provided content. This property is written as HTML content.
        /// </remarks>
        [Documented]
        public IHtmlContent? HtmlContent { get; set; }

        /// <summary>
        ///     Gets or sets the avatar icon rendered for the participant.
        /// </summary>
        [Documented]
        public IconStruct Icon { get; set; } = IconStruct.Bootstrap("bi-person-fill");

        /// <summary>
        ///     Gets or sets an explicit ownership flag.
        /// </summary>
        /// <remarks>
        ///     When this value is unset, ownership is resolved by comparing <see cref="ParticipantKey" /> with the builder current participant key.
        /// </remarks>
        [Documented]
        public bool? IsCurrentParticipant { get; set; }

        /// <summary>
        ///     Gets or sets the stable participant key.
        /// </summary>
        /// <remarks>
        ///     Use an account id, author slug, or domain-specific participant reference. The key allows conversations to contain more than two people.
        /// </remarks>
        [Documented]
        public string ParticipantKey { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets plain text rendered inside the message bubble.
        /// </summary>
        [Documented]
        public string Text { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the color variant used by the participant avatar.
        /// </summary>
        [Documented]
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Creates a shallow copy of this message model.
        /// </summary>
        /// <returns>A new <see cref="ConversationMessage" /> carrying the same values.</returns>
        [Documented]
        public ConversationMessage Clone()
        {
            return new ConversationMessage
            {
                AccessoryContent = AccessoryContent,
                AuthorName = AuthorName,
                AuthorSubtitle = AuthorSubtitle,
                AvatarBadgeText = AvatarBadgeText,
                AvatarBadgeVariant = AvatarBadgeVariant,
                BubbleVariant = BubbleVariant,
                CreatedAt = CreatedAt,
                DateText = DateText,
                HtmlContent = HtmlContent,
                Icon = Icon,
                IsCurrentParticipant = IsCurrentParticipant,
                ParticipantKey = ParticipantKey,
                Text = Text,
                Variant = Variant
            };
        }

        /// <summary>
        ///     Resolves whether this message belongs to the current participant key.
        /// </summary>
        /// <param name="currentParticipantKey">The current participant key configured on the conversation builder.</param>
        /// <returns><see langword="true" /> when the message is owned by the current participant; otherwise, <see langword="false" />.</returns>
        [Documented]
        public bool IsOwnedBy(string? currentParticipantKey)
        {
            if (IsCurrentParticipant.HasValue)
            {
                return IsCurrentParticipant.Value;
            }

            return !string.IsNullOrWhiteSpace(ParticipantKey) &&
                   string.Equals(ParticipantKey, currentParticipantKey, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///     Resolves the display date text for the message.
        /// </summary>
        /// <returns>The configured date text, a local short date/time value, or an empty string.</returns>
        [Documented]
        public string ResolveDateText()
        {
            if (!string.IsNullOrWhiteSpace(DateText))
            {
                return DateText;
            }

            return CreatedAt.HasValue
                ? CreatedAt.Value.ToLocalTime().ToString("g")
                : string.Empty;
        }

        #endregion
    }
}
