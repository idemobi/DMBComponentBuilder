using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public static class StepExtensions
    {
        public static StepAreaBuilder StepArea(this IHtmlHelper html)
        {
            return new StepAreaBuilder(html.ViewContext.Writer, html);
        }

        public static StepAreaBuilder<TModel> StepAreaFor<TModel>(this IHtmlHelper<TModel> html)
        {
            return new StepAreaBuilder<TModel>(html);
        }

        public static StepBlockBuilder StepBlock(this IHtmlHelper html)
        {
            return new StepBlockBuilder(html.ViewContext.Writer, html);
        }

        public static StepBlockBuilder Step(
            this IHtmlHelper html,
            string? title = null,
            string? subtitle = null,
            int? step = null,
            IconStruct icon = default,
            VariantStyle variant = VariantStyle.Primary,
            StepBlockState state = StepBlockState.Future)
        {
            StepBlockBuilder builder = new StepBlockBuilder(html.ViewContext.Writer, html)
                .WithTitle(title)
                .WithSubtitle(subtitle)
                .WithIcon(icon)
                .SetVariant(variant)
                .SetState(state);

            if (step.HasValue)
            {
                builder.WithStep(step.Value);
            }

            return builder;
        }
    }
}
