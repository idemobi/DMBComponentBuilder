#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Builds and renders a participant-based conversation thread.
    /// </summary>
    /// <remarks>
    ///     The component is suitable for support tickets, forum discussions, moderation threads, and other conversations
    ///     containing two or more participants. Messages owned by the current participant are aligned to the right;
    ///     every other participant is aligned to the left while keeping their own author, role, icon, and variant.
    /// </remarks>
    [Documented]
    public sealed class ConversationBuilder :
        HtmlBuilderBase<ConversationBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin
    {
        #region Instance fields and properties

        private readonly List<ConversationMessage> _messages = new();
        private VariantStyle _currentParticipantVariant = VariantStyle.Primary;
        private string _currentParticipantKey = string.Empty;
        private string _emptyMessage = "No messages yet.";
        private string _errorMessage = string.Empty;
        private bool _sortByCreation = true;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConversationBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        [Documented]
        public ConversationBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("conversation-builder", "d-flex", "flex-column", "gap-3");
            SetData("conversation", "true");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds a message to the conversation.
        /// </summary>
        /// <param name="message">The message to add.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder AddMessage(ConversationMessage? message)
        {
            if (message != null)
            {
                _messages.Add(message);
            }

            return this;
        }

        /// <summary>
        ///     Adds messages to the conversation.
        /// </summary>
        /// <param name="messages">The messages to add.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder AddMessages(IEnumerable<ConversationMessage>? messages)
        {
            if (messages == null)
            {
                return this;
            }

            foreach (ConversationMessage message in messages)
            {
                AddMessage(message);
            }

            return this;
        }

        /// <inheritdoc />
        protected override ConversationBuilder CreateInstance()
        {
            return new ConversationBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc />
        protected override void InternalClone(ConversationBuilder source)
        {
            base.InternalClone(source);

            _messages.Clear();
            _messages.AddRange(source._messages.Select(message => message.Clone()));
            _currentParticipantVariant = source._currentParticipantVariant;
            _currentParticipantKey = source._currentParticipantKey;
            _emptyMessage = source._emptyMessage;
            _errorMessage = source._errorMessage;
            _sortByCreation = source._sortByCreation;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        /// <summary>
        ///     Sets the participant key used to align current-user messages.
        /// </summary>
        /// <param name="participantKey">The current participant key.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder SetCurrentParticipantKey(string? participantKey)
        {
            _currentParticipantKey = participantKey ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the bubble variant used for current-participant messages when a message does not define its own bubble variant.
        /// </summary>
        /// <param name="variant">The current participant bubble variant.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder SetCurrentParticipantVariant(VariantStyle variant)
        {
            _currentParticipantVariant = variant;
            return this;
        }

        /// <summary>
        ///     Sets the message rendered when the conversation has no messages.
        /// </summary>
        /// <param name="message">The empty-state message.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder SetEmptyMessage(string? message)
        {
            _emptyMessage = message ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the message rendered when the conversation cannot be displayed.
        /// </summary>
        /// <param name="message">The error message. An empty value disables the error state.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder SetErrorMessage(string? message)
        {
            _errorMessage = message ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Replaces the current message collection.
        /// </summary>
        /// <param name="messages">The messages to render.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder SetMessages(IEnumerable<ConversationMessage>? messages)
        {
            _messages.Clear();
            return AddMessages(messages);
        }

        /// <summary>
        ///     Sets whether messages with creation dates should be sorted before rendering.
        /// </summary>
        /// <param name="sortByCreation">True to sort messages by creation date; false to keep insertion order.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public ConversationBuilder SetSortByCreation(bool sortByCreation)
        {
            _sortByCreation = sortByCreation;
            return this;
        }

        private string BuildBubbleClasses(ConversationMessage message, bool isCurrentParticipant)
        {
            VariantStyle? bubbleVariant = message.BubbleVariant;

            if (bubbleVariant.HasValue || isCurrentParticipant)
            {
                string variant = ResolveVariantCss(bubbleVariant ?? _currentParticipantVariant, "primary");
                return $"rounded-3 p-3 bg-{variant} text-white shadow-sm";
            }

            return "rounded-3 p-3 bg-body-tertiary border shadow-sm";
        }

        private static string ResolveVariantCss(VariantStyle variant, string fallback)
        {
            string variantCss = variant.GetVariantCss();
            return string.IsNullOrWhiteSpace(variantCss) ? fallback : variantCss;
        }

        private IEnumerable<ConversationMessage> ResolveMessages()
        {
            if (!_sortByCreation)
            {
                return _messages;
            }

            return _messages
                .OrderBy(message => message.CreatedAt ?? DateTimeOffset.MaxValue)
                .ThenBy(message => _messages.IndexOf(message));
        }

        private void WriteAvatar(TextWriter writer, HtmlEncoder encoder, ConversationMessage message)
        {
            AvatarBuilder avatar = _htmlHelper.AvatarBuilder()
                .SetDisplayName(message.AuthorName)
                .SetIcon(message.Icon)
                .SetVariant(message.Variant)
                .SetCustomColors(message.AvatarBackgroundColor, message.AvatarForegroundColor)
                .SetSize(AvatarSize.Medium);

            if (!string.IsNullOrWhiteSpace(message.AvatarBadgeText))
            {
                avatar.AddBadge(message.AvatarBadgeText, message.AvatarBadgeVariant);
            }

            avatar.WriteTo(writer, encoder);
        }

        private void WriteError(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"alert alert-danger mb-0\" role=\"alert\">");
            encoder.Encode(writer, _errorMessage);
            writer.Write("</div>");
        }

        private void WriteMessage(TextWriter writer, HtmlEncoder encoder, ConversationMessage message)
        {
            bool isCurrentParticipant = message.IsOwnedBy(_currentParticipantKey);
            string rowClasses = isCurrentParticipant
                ? "d-flex flex-row-reverse gap-2 justify-content-start align-items-start"
                : "d-flex gap-2 justify-content-start align-items-start";
            string bodyClasses = isCurrentParticipant
                ? "d-flex flex-column align-items-end"
                : "d-flex flex-column align-items-start";
            string metaClasses = isCurrentParticipant
                ? "small text-muted d-flex flex-wrap gap-2 justify-content-end text-end"
                : "small text-muted d-flex flex-wrap gap-2 justify-content-start";

            writer.Write($"<div class=\"{rowClasses}\">");
            WriteAvatar(writer, encoder, message);
            writer.Write($"<div class=\"{bodyClasses}\" style=\"max-width:min(48rem, calc(100% - 4.75rem));min-width:0;\">");
            WriteMessageMeta(writer, encoder, message, metaClasses);
            writer.Write($"<div class=\"{BuildBubbleClasses(message, isCurrentParticipant)}\" style=\"white-space:pre-wrap;\">");

            if (message.HtmlContent != null)
            {
                message.HtmlContent.WriteTo(writer, encoder);
            }
            else
            {
                encoder.Encode(writer, message.Text);
            }

            writer.Write("</div>");
            message.AccessoryContent?.WriteTo(writer, encoder);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private static void WriteMessageMeta(TextWriter writer, HtmlEncoder encoder, ConversationMessage message, string metaClasses)
        {
            string dateText = message.ResolveDateText();
            bool hasBadges = message.Badges.Any(badge => !string.IsNullOrWhiteSpace(badge.Text));

            if (string.IsNullOrWhiteSpace(message.AuthorName) &&
                string.IsNullOrWhiteSpace(message.AuthorSubtitle) &&
                string.IsNullOrWhiteSpace(dateText) &&
                hasBadges == false)
            {
                return;
            }

            writer.Write($"<div class=\"{metaClasses}\">");

            if (!string.IsNullOrWhiteSpace(message.AuthorName))
            {
                writer.Write("<span class=\"fw-semibold\">");
                encoder.Encode(writer, message.AuthorName);
                writer.Write("</span>");
            }

            if (!string.IsNullOrWhiteSpace(message.AuthorSubtitle))
            {
                writer.Write("<span>");
                encoder.Encode(writer, message.AuthorSubtitle);
                writer.Write("</span>");
            }

            if (!string.IsNullOrWhiteSpace(dateText))
            {
                writer.Write("<span>");
                encoder.Encode(writer, dateText);
                writer.Write("</span>");
            }

            foreach (ConversationMessageBadge badge in message.Badges)
            {
                if (string.IsNullOrWhiteSpace(badge.Text))
                {
                    continue;
                }

                string variant = ResolveVariantCss(badge.Variant, "secondary");
                writer.Write($"<span class=\"badge rounded-pill text-bg-{variant}\">");
                encoder.Encode(writer, badge.Text);
                writer.Write("</span>");
            }

            writer.Write("</div>");
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write($"<{GetTag()}{BuildAttributes()}>");

            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                WriteError(writer, encoder);
            }
            else if (_messages.Count == 0)
            {
                writer.Write("<div class=\"text-center text-muted py-4\">");
                encoder.Encode(writer, _emptyMessage);
                writer.Write("</div>");
            }
            else
            {
                foreach (ConversationMessage message in ResolveMessages())
                {
                    WriteMessage(writer, encoder, message);
                }
            }

            writer.Write($"</{GetTag()}>");
        }

        #endregion
    }
}
