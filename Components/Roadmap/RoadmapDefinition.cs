using DMBPageBuilder;
using Microsoft.AspNetCore.Html;

namespace DMBComponentBuilder
{
    internal sealed class RoadmapDefinition
    {
        public string Id { get; set; } = string.Empty;

        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? DateTitle { get; set; }
        public string? DateSubtitle { get; set; }
        public IHtmlContent Content { get; set; }

        public IconStruct Icon { get; set; }

        public VariantStyle Variant { get; set; } = VariantStyle.Primary;

        public RoadmapState State { get; set; } = RoadmapState.Future;
    }

}
