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
    internal sealed class RoadmapDefinition
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets the optional body content rendered inside the roadmap card.
        /// </summary>
        public IHtmlContent? Content { get; set; }

        /// <summary>
        ///     Gets or sets date subtitle used by roadmap component rendering.
        /// </summary>
        public string? DateSubtitle { get; set; }

        /// <summary>
        ///     Gets or sets date title used by roadmap component rendering.
        /// </summary>
        public string? DateTitle { get; set; }

        /// <summary>
        ///     Gets or sets icon used by roadmap component rendering.
        /// </summary>
        public IconStruct Icon { get; set; }

        /// <summary>
        ///     Gets or sets id used by roadmap component rendering.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets state used by roadmap component rendering.
        /// </summary>
        public RoadmapState State { get; set; } = RoadmapState.Future;

        /// <summary>
        ///     Gets or sets subtitle used by roadmap component rendering.
        /// </summary>
        public string? Subtitle { get; set; }

        /// <summary>
        ///     Gets or sets title used by roadmap component rendering.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Gets or sets variant used by roadmap component rendering.
        /// </summary>
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;

        #endregion
    }
}