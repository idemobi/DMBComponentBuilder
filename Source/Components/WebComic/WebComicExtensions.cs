#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Provides Razor helper and fluent extension methods for web comic components.
    /// </summary>
    public static class WebComicExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates or renders the web comic component through the web comic folder helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="folderUrl">The base folder URL that contains the expected comic assets.</param>
        /// <param name="title">The title value.</param>
        /// <param name="caption">The caption value.</param>
        /// <returns>The configured builder instance.</returns>
        public static WebComicViewerBuilder WebComicFolder(
            this IHtmlHelper html,
            string folderUrl,
            string? title = null,
            string? caption = null
        )
        {
            return new WebComicViewerBuilder(html.ViewContext.Writer, html)
                .SetComicFolder(folderUrl)
                .SetTitle(title)
                .SetCaption(caption);
        }

        /// <summary>
        ///     Creates or renders the web comic component through the web comic viewer helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static WebComicViewerBuilder WebComicViewer(this IHtmlHelper html)
        {
            return new WebComicViewerBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates or renders the web comic component through the web comic viewer helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="imageUrl">The URL of the comic image to render.</param>
        /// <param name="title">The title value.</param>
        /// <param name="caption">The caption value.</param>
        /// <returns>The configured builder instance.</returns>
        public static WebComicViewerBuilder WebComicViewer(
            this IHtmlHelper html,
            string imageUrl,
            string? title = null,
            string? caption = null
        )
        {
            return new WebComicViewerBuilder(html.ViewContext.Writer, html)
                .SetImageUrl(imageUrl)
                .SetTitle(title)
                .SetCaption(caption);
        }

        #endregion
    }
}