using System.Globalization;
using System.Net;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Builds and renders the web comic visual component for Razor views.
    /// </summary>
    public sealed class WebComicViewerBuilder :
        HtmlBuilderBase<WebComicViewerBuilder>,
        ICanUseCustomClasses
    {
        private const string WebComicCssPath = "/css/components/WebComicViewer.css";
        private const string WebComicJsPath = "/js/components/WebComicViewer.js";

        private string? _imageUrl;
        private string? _comicFolderUrl;
        private string? _title;
        private string? _caption;
        private string? _author = "Pou and Kortex";
        private string? _handle = "@MadBoat";
        private string? _alt;
        private bool _showReader = true;
        private bool _showSocialPreview = true;
        private bool _showDiagnostics = true;
        private int _expectedMinWidth = 1080;
        private int _expectedMinHeight = 1080;
        private WebComicDisplayMode _displayMode = WebComicDisplayMode.FitWidth;
        private WebComicSocialPlatform _platforms = WebComicSocialPlatform.All;
        private int _storySliceCount = 3;
        /// <summary>
        /// Initializes a new instance of the <see cref="WebComicViewerBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public WebComicViewerBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "section";
            this.AddClass("web-comic-viewer");
            SetData("web-comic-viewer", "true");
        }
        /// <summary>
        /// Configures the image url for the web comic component.
        /// </summary>
        /// <param name="imageUrl">The URL of the comic image to render.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetImageUrl(string? imageUrl)
        {
            _imageUrl = imageUrl;
            return this;
        }
        /// <summary>
        /// Configures the comic folder for the web comic component.
        /// </summary>
        /// <param name="folderUrl">The base folder URL that contains the expected comic assets.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetComicFolder(string? folderUrl)
        {
            _comicFolderUrl = NormalizeFolder(folderUrl);
            _imageUrl = GetComicAssetUrl("Strip_Final.png");
            return this;
        }
        /// <summary>
        /// Configures the title for the web comic component.
        /// </summary>
        /// <param name="title">The title value.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetTitle(string? title)
        {
            _title = title;
            return this;
        }
        /// <summary>
        /// Configures the caption for the web comic component.
        /// </summary>
        /// <param name="caption">The caption value.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetCaption(string? caption)
        {
            _caption = caption;
            return this;
        }
        /// <summary>
        /// Configures the author for the web comic component.
        /// </summary>
        /// <param name="author">The author value.</param>
        /// <param name="handle">The handle value.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetAuthor(string? author, string? handle = null)
        {
            _author = author;

            if (handle != null)
            {
                _handle = handle;
            }

            return this;
        }
        /// <summary>
        /// Configures the alt for the web comic component.
        /// </summary>
        /// <param name="alt">The alt value.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetAlt(string? alt)
        {
            _alt = alt;
            return this;
        }
        /// <summary>
        /// Configures the display mode for the web comic component.
        /// </summary>
        /// <param name="displayMode">The reader display mode applied to the comic image.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetDisplayMode(WebComicDisplayMode displayMode)
        {
            _displayMode = displayMode;
            return this;
        }
        /// <summary>
        /// Configures the social platforms for the web comic component.
        /// </summary>
        /// <param name="platforms">The social platforms included in generated share previews.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetSocialPlatforms(WebComicSocialPlatform platforms)
        {
            _platforms = platforms;
            return this;
        }
        /// <summary>
        /// Configures the story slice count for the web comic component.
        /// </summary>
        /// <param name="count">The number of story slices to render.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetStorySliceCount(int count)
        {
            _storySliceCount = Math.Max(0, count);
            return this;
        }
        /// <summary>
        /// Configures whether the reader section is rendered by the web comic component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder ShowReader(bool value = true)
        {
            _showReader = value;
            return this;
        }
        /// <summary>
        /// Configures whether the social preview section is rendered by the web comic component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder ShowSocialPreview(bool value = true)
        {
            _showSocialPreview = value;
            return this;
        }
        /// <summary>
        /// Configures whether the diagnostics section is rendered by the web comic component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder ShowDiagnostics(bool value = true)
        {
            _showDiagnostics = value;
            return this;
        }
        /// <summary>
        /// Configures the expected minimum size for the web comic component.
        /// </summary>
        /// <param name="width">The width value.</param>
        /// <param name="height">The height value.</param>
        /// <returns>The configured builder instance.</returns>
        public WebComicViewerBuilder SetExpectedMinimumSize(int width, int height)
        {
            _expectedMinWidth = Math.Max(1, width);
            _expectedMinHeight = Math.Max(1, height);
            return this;
        }
        /// <inheritdoc />
        protected override WebComicViewerBuilder CreateInstance()
        {
            return new WebComicViewerBuilder(_textWriter, _htmlHelper);
        }
        /// <inheritdoc />
        protected override void InternalClone(WebComicViewerBuilder source)
        {
            base.InternalClone(source);

            _imageUrl = source._imageUrl;
            _comicFolderUrl = source._comicFolderUrl;
            _title = source._title;
            _caption = source._caption;
            _author = source._author;
            _handle = source._handle;
            _alt = source._alt;
            _showReader = source._showReader;
            _showSocialPreview = source._showSocialPreview;
            _showDiagnostics = source._showDiagnostics;
            _expectedMinWidth = source._expectedMinWidth;
            _expectedMinHeight = source._expectedMinHeight;
            _displayMode = source._displayMode;
            _platforms = source._platforms;
            _storySliceCount = source._storySliceCount;
        }
        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            if (string.IsNullOrWhiteSpace(GetId()))
            {
                SetEnsureId("web-comic-viewer");
            }

            SetData("web-comic-min-width", _expectedMinWidth.ToString(CultureInfo.InvariantCulture));
            SetData("web-comic-min-height", _expectedMinHeight.ToString(CultureInfo.InvariantCulture));

            string modeClass = $"web-comic-mode-{_displayMode.ToString().ToLowerInvariant()}";
            this.AddClass(modeClass);

            writer.Write($"<{GetTag()}{BuildAttributes()}>");

            string? readerImageUrl = GetReaderImageUrl();

            if (string.IsNullOrWhiteSpace(readerImageUrl))
            {
                writer.Write("<div class=\"web-comic-empty\">No comic image configured.</div>");
                writer.Write($"</{GetTag()}>");
                return;
            }

            WriteHeader(writer);

            if (_showReader)
            {
                WriteReader(writer);
            }

            if (_showSocialPreview)
            {
                WriteSocialPreviews(writer);
            }

            if (_showDiagnostics)
            {
                WriteDiagnostics(writer);
            }

            writer.Write($"</{GetTag()}>");
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(WebComicCssPath);
            page.SetScriptFile(WebComicJsPath);
        }

        private void WriteHeader(TextWriter writer)
        {
            writer.Write("<div class=\"web-comic-toolbar\">");
            writer.Write("<div class=\"web-comic-heading\">");

            if (!string.IsNullOrWhiteSpace(_title))
            {
                writer.Write($"<h2>{WebUtility.HtmlEncode(_title)}</h2>");
            }

            if (!string.IsNullOrWhiteSpace(_caption))
            {
                writer.Write($"<p>{WebUtility.HtmlEncode(_caption)}</p>");
            }

            writer.Write("</div>");
            writer.Write("<div class=\"web-comic-tools\" role=\"group\" aria-label=\"Comic display tools\">");
            WriteTool(writer, "web-comic-fit-width", "bi-arrows-angle-expand", "Fit width");
            WriteTool(writer, "web-comic-fit-height", "bi-arrows-vertical", "Fit height");
            WriteTool(writer, "web-comic-full-image", "bi-aspect-ratio", "Full image");
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private static void WriteTool(TextWriter writer, string cssClass, string icon, string label)
        {
            writer.Write($"<button type=\"button\" class=\"web-comic-tool {cssClass}\" title=\"{WebUtility.HtmlEncode(label)}\" aria-label=\"{WebUtility.HtmlEncode(label)}\">");
            writer.Write($"<i class=\"bi {WebUtility.HtmlEncode(icon)}\" aria-hidden=\"true\"></i>");
            writer.Write("</button>");
        }

        private void WriteReader(TextWriter writer)
        {
            writer.Write("<div class=\"web-comic-reader\" data-web-comic-reader=\"true\">");
            writer.Write("<div class=\"web-comic-stage\">");
            WriteImage(writer, "web-comic-image", "reader", GetReaderImageUrl(), true);
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WriteSocialPreviews(TextWriter writer)
        {
            writer.Write("<div class=\"web-comic-social-grid\" aria-label=\"Social network previews\">");

            if (_platforms.HasFlag(WebComicSocialPlatform.X))
            {
                WriteXPreview(writer);
            }

            if (_platforms.HasFlag(WebComicSocialPlatform.Facebook))
            {
                WriteFacebookPreview(writer);
            }

            if (_platforms.HasFlag(WebComicSocialPlatform.Instagram))
            {
                WriteInstagramPreview(writer);
            }

            if (_platforms.HasFlag(WebComicSocialPlatform.LinkedIn))
            {
                WriteLinkedInPreview(writer);
            }

            if (_platforms.HasFlag(WebComicSocialPlatform.Stories))
            {
                WriteStoryPreview(writer);
            }

            writer.Write("</div>");
        }

        private void WriteXPreview(TextWriter writer)
        {
            WritePhoneStart(writer, "x", "X");
            writer.Write("<div class=\"web-comic-appbar\"><i class=\"bi bi-list\"></i><strong>X</strong><i class=\"bi bi-stars\"></i></div>");
            writer.Write("<div class=\"web-comic-feed\">");
            WritePostHeader(writer);
            WritePostText(writer);
            writer.Write("<div class=\"web-comic-media web-comic-media-x\">");
            WriteImage(writer, "web-comic-social-image", "x", GetPostImageUrl(), true);
            writer.Write("</div>");
            writer.Write("<div class=\"web-comic-actions\"><span><i class=\"bi bi-chat\"></i> 7</span><span><i class=\"bi bi-arrow-repeat\"></i></span><span><i class=\"bi bi-heart\"></i> 14</span><span><i class=\"bi bi-upload\"></i></span></div>");
            writer.Write("</div>");
            WritePhoneEnd(writer);
        }

        private void WriteFacebookPreview(TextWriter writer)
        {
            WritePhoneStart(writer, "facebook", "Facebook");
            writer.Write("<div class=\"web-comic-appbar web-comic-appbar-facebook\"><strong>Watch</strong><i class=\"bi bi-search\"></i></div>");
            writer.Write("<div class=\"web-comic-feed\">");
            WritePostHeader(writer);
            WritePostText(writer);
            writer.Write("<div class=\"web-comic-media web-comic-media-facebook\">");
            WriteImage(writer, "web-comic-social-image", "facebook", GetPostImageUrl(), true);
            writer.Write("</div>");
            writer.Write("<div class=\"web-comic-actions\"><span><i class=\"bi bi-hand-thumbs-up\"></i> Like</span><span><i class=\"bi bi-chat-square\"></i> Comment</span><span><i class=\"bi bi-reply\"></i> Share</span></div>");
            writer.Write("</div>");
            WritePhoneEnd(writer);
        }

        private void WriteInstagramPreview(TextWriter writer)
        {
            WritePhoneStart(writer, "instagram", "Instagram");
            writer.Write("<div class=\"web-comic-appbar\"><i class=\"bi bi-camera\"></i><strong>Instagram</strong><i class=\"bi bi-send\"></i></div>");
            writer.Write("<div class=\"web-comic-feed\">");
            WritePostHeader(writer);
            writer.Write("<div class=\"web-comic-media web-comic-media-instagram\">");
            WriteImage(writer, "web-comic-social-image", "instagram", GetSquarePostImageUrl(), true);
            writer.Write("</div>");
            writer.Write("<div class=\"web-comic-actions\"><span><i class=\"bi bi-heart\"></i></span><span><i class=\"bi bi-chat\"></i></span><span><i class=\"bi bi-send\"></i></span><span><i class=\"bi bi-bookmark\"></i></span></div>");
            WritePostText(writer);
            writer.Write("</div>");
            WritePhoneEnd(writer);
        }

        private void WriteLinkedInPreview(TextWriter writer)
        {
            WritePhoneStart(writer, "linkedin", "LinkedIn");
            writer.Write("<div class=\"web-comic-appbar web-comic-appbar-linkedin\"><strong>LinkedIn</strong><i class=\"bi bi-bell\"></i></div>");
            writer.Write("<div class=\"web-comic-feed\">");
            WritePostHeader(writer);
            WritePostText(writer);
            writer.Write("<div class=\"web-comic-media web-comic-media-linkedin\">");
            WriteImage(writer, "web-comic-social-image", "linkedin", GetPostImageUrl(), true);
            writer.Write("</div>");
            writer.Write("<div class=\"web-comic-actions\"><span><i class=\"bi bi-hand-thumbs-up\"></i> Like</span><span><i class=\"bi bi-chat-dots\"></i> Comment</span><span><i class=\"bi bi-share\"></i> Share</span></div>");
            writer.Write("</div>");
            WritePhoneEnd(writer);
        }

        private void WriteStoryPreview(TextWriter writer)
        {
            WritePhoneStart(writer, "stories", "Stories");
            writer.Write("<div class=\"web-comic-story-shell\">");
            writer.Write("<div class=\"web-comic-story-progress\" aria-hidden=\"true\">");

            for (int i = 0; i < _storySliceCount; i++)
            {
                writer.Write("<span></span>");
            }

            writer.Write("</div>");
            writer.Write("<div class=\"web-comic-story-stack\">");

            string? titleUrl = GetComicAssetUrl("Title.png");
            if (!string.IsNullOrWhiteSpace(titleUrl))
            {
                WriteImage(writer, "web-comic-story-title", "story-title", titleUrl, true);
            }

            for (int i = 1; i <= _storySliceCount; i++)
            {
                WriteImage(writer, "web-comic-story-image", $"story-{i}", GetComicAssetUrl($"Slice{i}.png"), true);
            }

            writer.Write("</div>");
            writer.Write("</div>");
            WritePhoneEnd(writer);
        }

        private void WritePhoneStart(TextWriter writer, string platform, string label)
        {
            writer.Write($"<article class=\"web-comic-phone web-comic-phone-{WebUtility.HtmlEncode(platform)}\" data-web-comic-platform=\"{WebUtility.HtmlEncode(platform)}\">");
            writer.Write($"<h3>{WebUtility.HtmlEncode(label)}</h3>");
            writer.Write("<div class=\"web-comic-device\">");
            writer.Write("<div class=\"web-comic-camera\" aria-hidden=\"true\"></div>");
            writer.Write("<div class=\"web-comic-status\"><strong>11:29</strong><span><i class=\"bi bi-reception-4\"></i><i class=\"bi bi-wifi\"></i><i class=\"bi bi-battery-full\"></i></span></div>");
            writer.Write("<div class=\"web-comic-screen\">");
        }

        private static void WritePhoneEnd(TextWriter writer)
        {
            writer.Write("</div>");
            writer.Write("</div>");
            writer.Write("</article>");
        }

        private void WritePostHeader(TextWriter writer)
        {
            string author = string.IsNullOrWhiteSpace(_author) ? "Comic account" : _author;
            string handle = string.IsNullOrWhiteSpace(_handle) ? "@comic" : _handle;

            writer.Write("<div class=\"web-comic-post-header\">");
            writer.Write("<div class=\"web-comic-avatar\"><i class=\"bi bi-person-fill\" aria-hidden=\"true\"></i></div>");
            writer.Write("<div>");
            writer.Write($"<strong>{WebUtility.HtmlEncode(author)}</strong>");
            writer.Write($"<span>{WebUtility.HtmlEncode(handle)} · 1h</span>");
            writer.Write("</div>");
            writer.Write("</div>");
        }

        private void WritePostText(TextWriter writer)
        {
            if (string.IsNullOrWhiteSpace(_caption))
            {
                return;
            }

            writer.Write($"<p class=\"web-comic-post-text\">{WebUtility.HtmlEncode(_caption)}</p>");
        }

        private void WriteDiagnostics(TextWriter writer)
        {
            writer.Write("<aside class=\"web-comic-diagnostics\" data-web-comic-diagnostics=\"true\" aria-live=\"polite\">");
            writer.Write("<h3>Integration checks</h3>");
            writer.Write("<dl>");
            WriteDiagnostic(writer, "Image", "Waiting for image load", "image");
            WriteDiagnostic(writer, "Dimensions", $"Minimum {_expectedMinWidth} x {_expectedMinHeight}px", "dimensions");
            WriteDiagnostic(writer, "Ratio", "Checking social crops", "ratio");
            WriteDiagnostic(writer, "Reader", "Checking display fit", "reader");
            writer.Write("</dl>");
            writer.Write("</aside>");
        }

        private static void WriteDiagnostic(TextWriter writer, string term, string value, string key)
        {
            writer.Write($"<div class=\"web-comic-check\" data-web-comic-check=\"{WebUtility.HtmlEncode(key)}\">");
            writer.Write($"<dt>{WebUtility.HtmlEncode(term)}</dt>");
            writer.Write($"<dd>{WebUtility.HtmlEncode(value)}</dd>");
            writer.Write("</div>");
        }

        private void WriteImage(TextWriter writer, string cssClass, string context, string? imageUrl, bool required)
        {
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return;
            }

            string alt = _alt ?? _title ?? "Web comic";
            string requiredAttribute = required ? " data-web-comic-required=\"true\"" : string.Empty;
            writer.Write($"<img class=\"{WebUtility.HtmlEncode(cssClass)}\" src=\"{WebUtility.HtmlEncode(imageUrl)}\" alt=\"{WebUtility.HtmlEncode(alt)}\" loading=\"lazy\" data-web-comic-image=\"{WebUtility.HtmlEncode(context)}\"{requiredAttribute} />");
        }

        private string? GetReaderImageUrl()
        {
            return GetComicAssetUrl("Strip_Final.png") ?? _imageUrl;
        }

        private string? GetPostImageUrl()
        {
            return GetComicAssetUrl("Strip_Final_1600x1600.png") ?? GetSquarePostImageUrl() ?? GetReaderImageUrl();
        }

        private string? GetSquarePostImageUrl()
        {
            return GetComicAssetUrl("Strip_Final_1080x1080.png") ?? GetComicAssetUrl("Strip_Final_1600x1600.png") ?? GetReaderImageUrl();
        }

        private string? GetComicAssetUrl(string fileName)
        {
            if (string.IsNullOrWhiteSpace(_comicFolderUrl))
            {
                return null;
            }

            return $"{_comicFolderUrl}/{fileName}";
        }

        private static string? NormalizeFolder(string? folderUrl)
        {
            if (string.IsNullOrWhiteSpace(folderUrl))
            {
                return null;
            }

            return folderUrl.Trim().TrimEnd('/');
        }
    }
}
