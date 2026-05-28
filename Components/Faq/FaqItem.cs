#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj FaqItem.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBPageBuilder;
using Microsoft.AspNetCore.Html;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Represents a single frequently asked question entry.
    /// </summary>
    public sealed class FaqItem
    {
        /// <summary>
        /// Gets or sets question used by faq component rendering.
        /// </summary>
        public string Question { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets answer used by faq component rendering.
        /// </summary>
        public string Answer { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets is visible used by faq component rendering.
        /// </summary>
        public bool IsVisible { get; set; } = true;
        /// <summary>
        /// Gets the icons rendered before the FAQ question text.
        /// </summary>
        public List<IconStruct> StartIcons { get; } = new();
        /// <summary>
        /// Gets the icons rendered after the FAQ question text.
        /// </summary>
        public List<IconStruct> EndIcons { get; } = new();
        /// <summary>
        /// Gets or sets action content used by faq component rendering.
        /// </summary>
        public IHtmlContent? ActionContent { get; set; }
        /// <summary>
        /// Gets or sets hidden notice used by faq component rendering.
        /// </summary>
        public IHtmlContent? HiddenNotice { get; set; }
    }
}
