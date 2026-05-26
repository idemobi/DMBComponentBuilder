using DMBPageBuilder;

namespace DMBComponentBuilder
{
    public static class RoadmapEffectExtensions
    {
        public static TBuilder WithOpacityEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetOpacity(value);
            return builder;
        }

        public static TBuilder WithScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetScale(value);
            return builder;
        }

        public static TBuilder WithSlideEffect<TBuilder>(
            this TBuilder builder,
            RoadmapSlideEffect effect = RoadmapSlideEffect.Bottom)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetSlide(effect);
            return builder;
        }

        public static TBuilder WithPlotEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetPlot(value);
            return builder;
        }

        public static TBuilder WithHoverScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetHoverScale(value);
            return builder;
        }

        public static TBuilder WithHoverLiftEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetHoverLift(value);
            return builder;
        }
    }
}