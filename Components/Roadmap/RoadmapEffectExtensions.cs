#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Provides Razor helper and fluent extension methods for roadmap components.
    /// </summary>
    public static class RoadmapEffectExtensions
    {
        #region Static methods

        /// <summary>
        ///     Enables or disables the hover lift class on a roadmap builder.
        /// </summary>
        /// <typeparam name="TBuilder">The roadmap-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the hover lift effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithHoverLiftEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetHoverLift(value);
            return builder;
        }

        /// <summary>
        ///     Enables or disables the hover scale class on a roadmap builder.
        /// </summary>
        /// <typeparam name="TBuilder">The roadmap-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the hover scale effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithHoverScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetHoverScale(value);
            return builder;
        }

        /// <summary>
        ///     Enables or disables the opacity animation class on a roadmap builder.
        /// </summary>
        /// <typeparam name="TBuilder">The roadmap-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the opacity effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithOpacityEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetOpacity(value);
            return builder;
        }

        /// <summary>
        ///     Enables or disables the plot animation class on a roadmap builder.
        /// </summary>
        /// <typeparam name="TBuilder">The roadmap-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the plot effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithPlotEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetPlot(value);
            return builder;
        }

        /// <summary>
        ///     Enables or disables the scale animation class on a roadmap builder.
        /// </summary>
        /// <typeparam name="TBuilder">The roadmap-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">True to enable the scale effect; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithScaleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetScale(value);
            return builder;
        }

        /// <summary>
        ///     Configures the slide animation class applied to roadmap entries.
        /// </summary>
        /// <typeparam name="TBuilder">The roadmap-capable builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="effect">The slide effect applied to the roadmap entries.</param>
        /// <returns>The configured builder instance.</returns>
        public static TBuilder WithSlideEffect<TBuilder>(
            this TBuilder builder,
            RoadmapSlideEffect effect = RoadmapSlideEffect.Bottom
        )
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRoadmapEffects
        {
            RoadmapEffectComposer composer = builder.GetOrCreateCssComposer(() => new RoadmapEffectComposer());
            composer.SetSlide(effect);
            return builder;
        }

        #endregion
    }
}