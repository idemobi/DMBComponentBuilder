using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public static class RoadmapExtensions
    {
        public static RoadmapBuilder Roadmap(this IHtmlHelper html)
        {
            return new RoadmapBuilder(html.ViewContext.Writer, html);
        }

        public static RoadmapBlockBuilder RoadmapBlock(this IHtmlHelper html)
        {
            return new RoadmapBlockBuilder(html.ViewContext.Writer, html);
        }

        public static RoadmapBlockBuilder RoadmapItem(
            this IHtmlHelper html,
            string? title = null,
            string? subtitle = null,
            IconStruct icon = default,
            VariantStyle variant = VariantStyle.Primary,
            RoadmapState state = RoadmapState.Future,
            string? dateTitle = null,
            string? dateSubtitle = null)
        {
            return new RoadmapBlockBuilder(html.ViewContext.Writer, html)
                .WithTitle(title)
                .WithSubtitle(subtitle)
                .WithIcon(icon)
                .SetVariant(variant)
                .SetState(state)
                .WithDate(dateTitle, dateSubtitle);
        }
    }
}
