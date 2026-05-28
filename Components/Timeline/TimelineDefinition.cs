using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBComponentBuilder
{
    internal sealed class TimelineDefinition
    {
        /// <summary>
        /// Gets or sets id used by timeline component rendering.
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets date title used by timeline component rendering.
        /// </summary>
        public string? DateTitle { get; set; }
        /// <summary>
        /// Gets or sets date subtitle used by timeline component rendering.
        /// </summary>
        public string? DateSubtitle { get; set; }
        /// <summary>
        /// Gets or sets title used by timeline component rendering.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Gets or sets title level used by timeline component rendering.
        /// </summary>
        public TitleLevel TitleLevel { get; set; } = TitleLevel.Four;
        /// <summary>
        /// Gets or sets subtitle used by timeline component rendering.
        /// </summary>
        public string? Subtitle { get; set; }
        /// <summary>
        /// Gets or sets icon used by timeline component rendering.
        /// </summary>
        public IconStruct Icon { get; set; }
        /// <summary>
        /// Gets or sets variant used by timeline component rendering.
        /// </summary>
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;
        /// <summary>
        /// Gets or sets is left used by timeline component rendering.
        /// </summary>
        public bool IsLeft { get; set; }
        /// <summary>
        /// Gets or sets content html used by timeline component rendering.
        /// </summary>
        public string ContentHtml { get; set; } = string.Empty;
    }
}