using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBComponentBuilder
{
    internal sealed class TimelineDefinition
    {
        public string Id { get; set; } = string.Empty;
        public string? DateTitle { get; set; }
        public string? DateSubtitle { get; set; }
        public string? Title { get; set; }
        public TitleLevel TitleLevel { get; set; } = TitleLevel.Four;
        public string? Subtitle { get; set; }
        public IconStruct Icon { get; set; }
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;
        public bool IsLeft { get; set; }
        public string ContentHtml { get; set; } = string.Empty;
    }
}