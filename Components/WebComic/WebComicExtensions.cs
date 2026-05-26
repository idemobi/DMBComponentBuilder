using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public static class WebComicExtensions
    {
        public static WebComicViewerBuilder WebComicViewer(this IHtmlHelper html)
        {
            return new WebComicViewerBuilder(html.ViewContext.Writer, html);
        }

        public static WebComicViewerBuilder WebComicViewer(
            this IHtmlHelper html,
            string imageUrl,
            string? title = null,
            string? caption = null)
        {
            return new WebComicViewerBuilder(html.ViewContext.Writer, html)
                .SetImageUrl(imageUrl)
                .SetTitle(title)
                .SetCaption(caption);
        }

        public static WebComicViewerBuilder WebComicFolder(
            this IHtmlHelper html,
            string folderUrl,
            string? title = null,
            string? caption = null)
        {
            return new WebComicViewerBuilder(html.ViewContext.Writer, html)
                .SetComicFolder(folderUrl)
                .SetTitle(title)
                .SetCaption(caption);
        }
    }
}
