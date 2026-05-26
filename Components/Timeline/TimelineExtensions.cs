using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public static class TimelineExtensions
    {
        public static TBuilder WithOpacityEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetOpacity(value);

            return builder;
        }

        public static TBuilder WithScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetScale(value);

            return builder;
        }

        public static TBuilder WithSlideEffect<TBuilder>(
            this TBuilder builder,
            TimelineSlideEffect effect = TimelineSlideEffect.Alternate)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetSlide(effect);

            return builder;
        }

        public static TBuilder WithPlotEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetPlot(value);

            return builder;
        }

        public static TBuilder WithHoverScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetHoverScale(value);

            return builder;
        }

        public static TBuilder WithHoverLiftEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetHoverLift(value);

            return builder;
        }
        
        public static TimelineBuilder Timeline(this IHtmlHelper html)
        {
            return new TimelineBuilder(html.ViewContext.Writer, html);
        }

        public static TimelineBlockBuilder TimelineBlock(this IHtmlHelper html)
        {
            return new TimelineBlockBuilder(html.ViewContext.Writer, html);
        }

        public static TimelineBlockBuilder TimelineCard(
            this IHtmlHelper html,
            string? dateTitle,
            string? dateSubtitle,
            string? title,
            TitleLevel titleLevel,
            string? subtitle,
            IconStruct icon,
            VariantStyle variant = VariantStyle.Primary,
            bool chevronInside = false)
        {
            return new TimelineBlockBuilder(html.ViewContext.Writer, html)
                .WithDate(dateTitle, dateSubtitle)
                .WithTitle(title, titleLevel)
                .WithSubtitle(subtitle)
                .WithIcon(icon)
                .SetVariant(variant);
        }
    }
}