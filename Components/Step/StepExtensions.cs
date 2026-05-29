#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Provides Razor helper and fluent extension methods for step components.
    /// </summary>
    public static class StepExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates or renders the step component through the step helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="title">The title value.</param>
        /// <param name="subtitle">The subtitle value.</param>
        /// <param name="step">The step value.</param>
        /// <param name="icon">The icon value.</param>
        /// <param name="variant">The variant value.</param>
        /// <param name="state">The state value.</param>
        /// <returns>The configured builder instance.</returns>
        public static StepBlockBuilder Step(
            this IHtmlHelper html,
            string? title = null,
            string? subtitle = null,
            int? step = null,
            IconStruct icon = default,
            VariantStyle variant = VariantStyle.Primary,
            StepBlockState state = StepBlockState.Future
        )
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

        /// <summary>
        ///     Creates or renders the step component through the step area helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static StepAreaBuilder StepArea(this IHtmlHelper html)
        {
            return new StepAreaBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a typed step area builder that can resolve field ids from model expressions.
        /// </summary>
        /// <typeparam name="TModel">The Razor page model type.</typeparam>
        /// <param name="html">The typed Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static StepAreaBuilder<TModel> StepAreaFor<TModel>(this IHtmlHelper<TModel> html)
        {
            return new StepAreaBuilder<TModel>(html);
        }

        /// <summary>
        ///     Creates or renders the step component through the step block helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static StepBlockBuilder StepBlock(this IHtmlHelper html)
        {
            return new StepBlockBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}