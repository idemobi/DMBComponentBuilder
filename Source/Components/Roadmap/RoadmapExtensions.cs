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
    ///     Provides Razor helper and fluent extension methods for roadmap components.
    /// </summary>
    public static class RoadmapExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates or renders the roadmap component through the roadmap helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static RoadmapBuilder Roadmap(this IHtmlHelper html)
        {
            return new RoadmapBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates or renders the roadmap component through the roadmap block helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static RoadmapBlockBuilder RoadmapBlock(this IHtmlHelper html)
        {
            return new RoadmapBlockBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates or renders the roadmap component through the roadmap item helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="title">The title value.</param>
        /// <param name="subtitle">The subtitle value.</param>
        /// <param name="icon">The icon value.</param>
        /// <param name="variant">The variant value.</param>
        /// <param name="state">The state value.</param>
        /// <param name="dateTitle">The date title value.</param>
        /// <param name="dateSubtitle">The date subtitle value.</param>
        /// <returns>The configured builder instance.</returns>
        public static RoadmapBlockBuilder RoadmapItem(
            this IHtmlHelper html,
            string? title = null,
            string? subtitle = null,
            IconStruct icon = default,
            VariantStyle variant = VariantStyle.Primary,
            RoadmapState state = RoadmapState.Future,
            string? dateTitle = null,
            string? dateSubtitle = null
        )
        {
            return new RoadmapBlockBuilder(html.ViewContext.Writer, html)
                .WithTitle(title)
                .WithSubtitle(subtitle)
                .WithIcon(icon)
                .SetVariant(variant)
                .SetState(state)
                .WithDate(dateTitle, dateSubtitle);
        }

        #endregion
    }
}