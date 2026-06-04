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
    ///     Provides Razor helper methods for <see cref="SeparatorBuilder" />.
    /// </summary>
    public static class SeparatorExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates or renders the separator component through the page separator text helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="text">The text value.</param>
        /// <returns>The configured builder instance.</returns>
        public static SeparatorBuilder PageSeparatorText(this IHtmlHelper html, string? text)
        {
            return html.SeparatorText(text)
                .UsePageSpacing();
        }

        /// <summary>
        ///     Creates or renders the separator component through the separator builder helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static SeparatorBuilder SeparatorBuilder(this IHtmlHelper html)
        {
            return new SeparatorBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates or renders the separator component through the separator icon helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public static SeparatorBuilder SeparatorIcon(this IHtmlHelper html, IconStruct icon)
        {
            return html.SeparatorBuilder()
                .SetIcon(icon);
        }

        /// <summary>
        ///     Creates or renders the separator component through the separator icon helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public static SeparatorBuilder SeparatorIcon(this IHtmlHelper html, string? icon)
        {
            return html.SeparatorBuilder()
                .SetIcon(icon);
        }

        /// <summary>
        ///     Creates or renders the separator component through the separator text helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="text">The text value.</param>
        /// <returns>The configured builder instance.</returns>
        public static SeparatorBuilder SeparatorText(this IHtmlHelper html, string? text)
        {
            return html.SeparatorBuilder()
                .SetText(text);
        }

        #endregion
    }
}