using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public static class SocialShareExtensions
    {
        public static SocialShareBuilder SocialShare(this IHtmlHelper html, SocialShareDefinition definition)
        {
            return new SocialShareBuilder(html, definition);
        }

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
