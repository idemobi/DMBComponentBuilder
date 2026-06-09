#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBPageBuilder;
using Microsoft.AspNetCore.Html;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Represents a single frequently asked question entry.
    /// </summary>
    public sealed class FaqItem
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets action content used by faq component rendering.
        /// </summary>
        public IHtmlContent? ActionContent { get; set; }

        /// <summary>
        ///     Gets or sets answer used by faq component rendering.
        /// </summary>
        public string Answer { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the icons rendered after the FAQ question text.
        /// </summary>
        public List<IconStruct> EndIcons { get; } = new();

        /// <summary>
        ///     Gets or sets hidden notice used by faq component rendering.
        /// </summary>
        public IHtmlContent? HiddenNotice { get; set; }

        /// <summary>
        ///     Gets or sets is visible used by faq component rendering.
        /// </summary>
        public bool IsVisible { get; set; } = true;

        /// <summary>
        ///     Gets or sets question used by faq component rendering.
        /// </summary>
        public string Question { get; set; } = string.Empty;

        /// <summary>
        ///     Gets the icons rendered before the FAQ question text.
        /// </summary>
        public List<IconStruct> StartIcons { get; } = new();

        #endregion
    }
}