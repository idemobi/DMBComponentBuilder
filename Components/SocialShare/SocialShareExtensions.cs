using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper and fluent extension methods for social share components.
    /// </summary>
    public static class SocialShareExtensions
    {
        /// <summary>
        /// Creates or renders the social share component through the social share helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="definition">The definition value.</param>
        /// <returns>The configured builder instance.</returns>
        public static SocialShareBuilder SocialShare(this IHtmlHelper html, SocialShareDefinition definition)
        {
            return new SocialShareBuilder(html, definition);
        }
        /// <summary>
        /// Creates or renders the social share component through the social share helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="title">The title value.</param>
        /// <param name="url">The url value.</param>
        /// <param name="kind">The kind value.</param>
        /// <param name="style">The style value.</param>
        /// <returns>The configured builder instance.</returns>
        public static SocialShareBuilder SocialShare(
            this IHtmlHelper html,
            string title,
            string url,
            SocialShareKind kind = SocialShareKind.Html,
            SocialShareStyle style = SocialShareStyle.Toolbar)
        {
            return html.SocialShare(new SocialShareDefinition(title, url, kind, style));
        }
    }
}
