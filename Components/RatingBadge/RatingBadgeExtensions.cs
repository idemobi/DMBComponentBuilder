#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj RatingBadgeExtensions.cs create at 2026/05/05
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper and fluent extension methods for configuring rating badges.
    /// </summary>
    public static class RatingBadgeExtensions
    {
        /// <summary>
        /// Sets the Bootstrap variant used by the rating badge.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="variant">The Bootstrap variant to apply.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder SetVariant<TBuilder>(this TBuilder builder, VariantStyle variant)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeStyle
        {
            RatingBadgeComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeComposer());
            composer.SetVariant(variant);

            return builder;
        }

        /// <summary>
        /// Enables or disables the cartouche presentation for the rating badge.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">A value indicating whether the cartouche should be applied.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder SetCartouche<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeStyle
        {
            RatingBadgeComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeComposer());
            composer.SetCartouche(value);

            return builder;
        }

        /// <summary>
        /// Sets the size preset for the rating badge.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="size">The rating badge size preset.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder SetSize<TBuilder>(this TBuilder builder, RatingBadgeSize size)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeStyle
        {
            RatingBadgeComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeComposer());
            composer.SetSize(size);

            return builder;
        }

        /// <summary>
        /// Applies a staggered pop-in animation to the rating stars.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">A value indicating whether the effect should be applied.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder WithPopEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeEffects
        {
            RatingBadgeEffectComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeEffectComposer());
            composer.SetPop(value);

            return builder;
        }

        /// <summary>
        /// Applies a sparkle sweep animation across the rating stars.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">A value indicating whether the effect should be applied.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder WithSparkleEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeEffects
        {
            RatingBadgeEffectComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeEffectComposer());
            composer.SetSparkle(value);

            return builder;
        }

        /// <summary>
        /// Applies a subtle pulse animation to filled rating stars.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">A value indicating whether the effect should be applied.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder WithPulseEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeEffects
        {
            RatingBadgeEffectComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeEffectComposer());
            composer.SetPulse(value);

            return builder;
        }

        /// <summary>
        /// Applies a slight lift to the rating badge on hover.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">A value indicating whether the effect should be applied.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder WithHoverLiftEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeEffects
        {
            RatingBadgeEffectComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeEffectComposer());
            composer.SetHoverLift(value);

            return builder;
        }

        /// <summary>
        /// Applies a glow to filled rating stars on hover.
        /// </summary>
        /// <typeparam name="TBuilder">The concrete builder type.</typeparam>
        /// <param name="builder">The builder to configure.</param>
        /// <param name="value">A value indicating whether the effect should be applied.</param>
        /// <returns>The configured builder.</returns>
        public static TBuilder WithHoverGlowEffect<TBuilder>(this TBuilder builder, bool value = true)
            where TBuilder : HtmlBuilderBase<TBuilder>, ICanUseRatingBadgeEffects
        {
            RatingBadgeEffectComposer composer = builder.GetOrCreateCssComposer(() => new RatingBadgeEffectComposer());
            composer.SetHoverGlow(value);

            return builder;
        }

        /// <summary>
        /// Creates a new <see cref="RatingBadgeBuilder"/> for rendering a star rating.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="value">The rating value to render.</param>
        /// <param name="maxStars">The maximum number of stars to display.</param>
        /// <param name="variant">The Bootstrap variant used to color the rating.</param>
        /// <returns>A configured rating badge builder.</returns>
        public static RatingBadgeBuilder RatingBadge(
            this IHtmlHelper html,
            double value = 0d,
            int maxStars = 5,
            VariantStyle variant = VariantStyle.Warning)
        {
            return new RatingBadgeBuilder(html.ViewContext.Writer, html)
                .SetRating(value)
                .SetMaxStars(maxStars)
                .SetVariant(variant);
        }
    }
}
