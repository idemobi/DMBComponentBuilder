using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper and fluent extension methods for timeline components.
    /// </summary>
    public static class TimelineExtensions
    {
        /// <summary>
        /// Enables or disables the opacity animation class on a timeline builder.
        /// </summary>
        /// <typeparam name="TBuilder">The timeline-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the opacity effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithOpacityEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetOpacity(value);

            return builder;
        }

        /// <summary>
        /// Enables or disables the scale animation class on a timeline builder.
        /// </summary>
        /// <typeparam name="TBuilder">The timeline-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the scale effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetScale(value);

            return builder;
        }

        /// <summary>
        /// Configures the slide animation class applied to timeline entries.
        /// </summary>
        /// <typeparam name="TBuilder">The timeline-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="effect">The slide effect applied to the timeline entries.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithSlideEffect<TBuilder>(
            this TBuilder builder,
            TimelineSlideEffect effect = TimelineSlideEffect.Alternate)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetSlide(effect);

            return builder;
        }

        /// <summary>
        /// Enables or disables the plot animation class on a timeline builder.
        /// </summary>
        /// <typeparam name="TBuilder">The timeline-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the plot effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithPlotEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetPlot(value);

            return builder;
        }

        /// <summary>
        /// Enables or disables the hover scale class on a timeline builder.
        /// </summary>
        /// <typeparam name="TBuilder">The timeline-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the hover scale effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithHoverScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetHoverScale(value);

            return builder;
        }

        /// <summary>
        /// Enables or disables the hover lift class on a timeline builder.
        /// </summary>
        /// <typeparam name="TBuilder">The timeline-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the hover lift effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithHoverLiftEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseTimelineEffects
        {
            TimelineEffectComposer composer =   builder.GetOrCreateCssComposer(() => new TimelineEffectComposer());
            composer.SetHoverLift(value);

            return builder;
        }
        /// <summary>
        /// Creates or renders the timeline component through the timeline helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static TimelineBuilder Timeline(this IHtmlHelper html)
        {
            return new TimelineBuilder(html.ViewContext.Writer, html);
        }
        /// <summary>
        /// Creates or renders the timeline component through the timeline block helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static TimelineBlockBuilder TimelineBlock(this IHtmlHelper html)
        {
            return new TimelineBlockBuilder(html.ViewContext.Writer, html);
        }
        /// <summary>
        /// Creates or renders the timeline component through the timeline card helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="dateTitle">The date title value.</param>
        /// <param name="dateSubtitle">The date subtitle value.</param>
        /// <param name="title">The title value.</param>
        /// <param name="titleLevel">The title level value.</param>
        /// <param name="subtitle">The subtitle value.</param>
        /// <param name="icon">The icon value.</param>
        /// <param name="variant">The variant value.</param>
        /// <param name="chevronInside">The chevron inside value.</param>
        /// <returns>The configured builder instance.</returns>
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
