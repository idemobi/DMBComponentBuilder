using System.Net;
using System.Text.Encodings.Web;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Renders a Bootstrap compatible social share control.
    /// </summary>
    public sealed class SocialShareBuilder : IHtmlContent
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly SocialShareDefinition _definition;
        /// <summary>
        /// Initializes a new instance of the <see cref="SocialShareBuilder"/> class.
        /// </summary>
        /// <param name="htmlHelper">The html helper value.</param>
        /// <param name="definition">The definition value.</param>
        public SocialShareBuilder(IHtmlHelper htmlHelper, SocialShareDefinition definition)
        {
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            _definition = definition ?? throw new ArgumentNullException(nameof(definition));
        }
        /// <summary>
        /// Writes the configured social share component to the target HTML writer.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="encoder">The HTML encoder used for writer output.</param>
        public void WriteTo(TextWriter writer, HtmlEncoder encoder)
        {
            if (string.IsNullOrWhiteSpace(_definition.Url))
            {
                return;
            }

            switch (_definition.Style)
            {
                case SocialShareStyle.Carded:
                    WriteCarded(writer);
                    break;
                case SocialShareStyle.MenuDropdown:
                    WriteMenuDropdown(writer);
                    break;
                case SocialShareStyle.Toolbar:
                    WriteToolbar(writer);
                    break;
                case SocialShareStyle.Inline:
                default:
                    WriteInline(writer);
                    break;
            }
        }

        private void WriteInline(TextWriter writer)
        {
            writer.Write($"<div class=\"social-share social-share-inline d-flex flex-wrap align-items-center gap-2{GetCssClass()}\" data-social-share-kind=\"{Encode(_definition.Kind.ToString())}\">");
            writer.Write($"<span class=\"social-share-title small text-body-secondary\">{Encode(GetTitle())}</span>");
            WriteButtons(writer, "btn btn-sm btn-outline-secondary");
            writer.Write("</div>");
        }

        private void WriteCarded(TextWriter writer)
        {
            writer.Write($"<section class=\"social-share social-share-card card{GetCssClass()}\" data-social-share-kind=\"{Encode(_definition.Kind.ToString())}\">");
            writer.Write("<div class=\"card-header d-flex align-items-center gap-2\">");
            writer.Write("<span class=\"bi bi-share\" aria-hidden=\"true\"></span>");
            writer.Write($"<strong>{Encode(GetTitle())}</strong>");
            writer.Write("</div>");
            writer.Write("<div class=\"card-body d-flex flex-wrap gap-2\">");
            WriteButtons(writer, "btn btn-sm btn-outline-secondary");
            writer.Write("</div>");
            writer.Write("</section>");
        }

        private void WriteMenuDropdown(TextWriter writer)
        {
            string id = _htmlHelper.GenerateUniqueId("social_share_");
            writer.Write($"<div class=\"social-share social-share-dropdown dropdown{GetCssClass()}\" data-social-share-kind=\"{Encode(_definition.Kind.ToString())}\">");
            writer.Write($"<button class=\"btn btn-sm btn-outline-secondary dropdown-toggle\" type=\"button\" id=\"{Encode(id)}\" data-bs-toggle=\"dropdown\" aria-expanded=\"false\">");
            writer.Write("<span class=\"bi bi-share\" aria-hidden=\"true\"></span> ");
            writer.Write(Encode(GetTitle()));
            writer.Write("</button>");
            writer.Write($"<ul class=\"dropdown-menu\" aria-labelledby=\"{Encode(id)}\">");

            foreach (SocialSharePlatform platform in GetPlatforms())
            {
                writer.Write("<li>");
                WriteLink(writer, platform, "dropdown-item", true);
                writer.Write("</li>");
            }

            writer.Write("</ul>");
            writer.Write("</div>");
        }

        private void WriteToolbar(TextWriter writer)
        {
            writer.Write($"<div class=\"social-share social-share-toolbar btn-toolbar gap-2{GetCssClass()}\" role=\"toolbar\" aria-label=\"{Encode(GetTitle())}\" data-social-share-kind=\"{Encode(_definition.Kind.ToString())}\">");
            writer.Write("<div class=\"btn-group btn-group-sm\" role=\"group\">");
            WriteButtons(writer, "btn btn-outline-secondary");
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteButtons(TextWriter writer, string cssClass)
        {
            foreach (SocialSharePlatform platform in GetPlatforms())
            {
                WriteLink(writer, platform, cssClass, false);
            }
        }

        private void WriteLink(TextWriter writer, SocialSharePlatform platform, string cssClass, bool showLabel)
        {
            string label = GetPlatformLabel(platform);
            string url = BuildShareUrl(platform);
            string target = url.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase) ? string.Empty : " target=\"_blank\" rel=\"noopener noreferrer\"";

            writer.Write($"<a class=\"{Encode(cssClass)}\" href=\"{Encode(url)}\" aria-label=\"{Encode(label)}\"{target}>");
            writer.Write($"<span class=\"bi {Encode(GetPlatformIcon(platform))}\" aria-hidden=\"true\"></span>");

            if (showLabel)
            {
                writer.Write($" <span>{Encode(label)}</span>");
            }
            else
            {
                writer.Write($"<span class=\"visually-hidden\">{Encode(label)}</span>");
            }

            writer.Write("</a>");
        }

        private IEnumerable<SocialSharePlatform> GetPlatforms()
        {
            return _definition.Platforms
                .Distinct()
                .Where(platform => IsPlatformAvailableForKind(platform, _definition.Kind));
        }

        private static bool IsPlatformAvailableForKind(SocialSharePlatform platform, SocialShareKind kind)
        {
            return platform switch
            {
                SocialSharePlatform.Instagram => kind == SocialShareKind.Image,
                SocialSharePlatform.Pinterest => kind == SocialShareKind.Image,
                SocialSharePlatform.Twitch => kind == SocialShareKind.Video,
                _ => true
            };
        }

        private string BuildShareUrl(SocialSharePlatform platform)
        {
            string url = WebUtility.UrlEncode(_definition.Url);
            string title = WebUtility.UrlEncode(GetTitle());

            return platform switch
            {
                SocialSharePlatform.Email => $"mailto:?subject={title}&body={url}",
                SocialSharePlatform.Facebook => $"https://www.facebook.com/share.php?u={url}",
                SocialSharePlatform.GooglePlus => $"https://plus.google.com/share?url={url}",
                SocialSharePlatform.WhatsApp => $"https://wa.me/?text={url}",
                SocialSharePlatform.Weibo => $"http://service.weibo.com/share/share.php?url={url}",
                SocialSharePlatform.Renren => $"http://share.renren.com/share/buttonshare.do?link={url}",
                SocialSharePlatform.Baidu => $"http://cang.baidu.com/do/add?it={title}&iu={url}",
                SocialSharePlatform.Reddit => $"https://reddit.com/submit?url={url}&title={title}",
                SocialSharePlatform.Twitter => $"https://twitter.com/share?url={url}&text={title}",
                SocialSharePlatform.Pinterest => $"https://pinterest.com/pin/create/button/?url={url}",
                SocialSharePlatform.LinkedIn => $"https://www.linkedin.com/shareArticle?mini=true&url={url}&title={title}",
                _ => _definition.Url
            };
        }

        private string GetTitle()
        {
            if (!string.IsNullOrWhiteSpace(_definition.Title))
            {
                return _definition.Title;
            }

            return _definition.Kind switch
            {
                SocialShareKind.Image => "Share this image",
                SocialShareKind.Sound => "Share this sound",
                SocialShareKind.Video => "Share this video",
                _ => "Share this page"
            };
        }

        private string GetPlatformLabel(SocialSharePlatform platform)
        {
            if (_definition.Labels.TryGetValue(platform, out string? label) && !string.IsNullOrWhiteSpace(label))
            {
                return label;
            }

            return platform switch
            {
                SocialSharePlatform.Email => "Email",
                SocialSharePlatform.GooglePlus => "Google+",
                SocialSharePlatform.WhatsApp => "WhatsApp",
                SocialSharePlatform.LinkedIn => "LinkedIn",
                _ => platform.ToString()
            };
        }

        private static string GetPlatformIcon(SocialSharePlatform platform)
        {
            return platform switch
            {
                SocialSharePlatform.Email => "bi-envelope",
                SocialSharePlatform.Facebook => "bi-facebook",
                SocialSharePlatform.GooglePlus => "bi-google",
                SocialSharePlatform.WhatsApp => "bi-whatsapp",
                SocialSharePlatform.Instagram => "bi-instagram",
                SocialSharePlatform.Weibo => "bi-sina-weibo",
                SocialSharePlatform.Reddit => "bi-reddit",
                SocialSharePlatform.Twitter => "bi-twitter-x",
                SocialSharePlatform.Pinterest => "bi-pinterest",
                SocialSharePlatform.LinkedIn => "bi-linkedin",
                SocialSharePlatform.Discord => "bi-discord",
                SocialSharePlatform.Twitch => "bi-twitch",
                _ => "bi-share"
            };
        }

        private string GetCssClass()
        {
            return string.IsNullOrWhiteSpace(_definition.CssClass)
                ? string.Empty
                : $" {Encode(_definition.CssClass)}";
        }

        private static string Encode(string value)
        {
            return WebUtility.HtmlEncode(value);
        }
    }
}
