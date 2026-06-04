#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.IO;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Builds and renders the copy block visual component for Razor views.
    /// </summary>
    public sealed class CopyBlockBuilder :
        HtmlBuilderBase<CopyBlockBuilder>,
        ICanUseCustomClasses,
        IDisposable
    {
        #region Constants

        private const string CopyBlockJsPath = "/js/components/CopyBlock.js";

        #endregion

        #region Instance fields and properties

        private StringWriter? _captureWriter;

        private string _content = string.Empty;
        private bool _disposed;
        private TextWriter? _originalWriter;
        private bool _started;
        private BootstrapFullKindOfStyle _style = BootstrapFullKindOfStyle.OutlinePrimary;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="CopyBlockBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public CopyBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClass("d-flex");
            this.AddClass("justify-content-between");
            this.AddClass("align-items-start");
            SetData("copy-block", "true");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Starts the copy block rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public CopyBlockBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            _started = true;
            _originalWriter = _htmlHelper.ViewContext.Writer;
            _captureWriter = new StringWriter();
            _htmlHelper.ViewContext.Writer = _captureWriter;

            return this;
        }

        /// <inheritdoc />
        protected override CopyBlockBuilder CreateInstance()
        {
            return new CopyBlockBuilder(_textWriter, _htmlHelper);
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetScriptFile(CopyBlockJsPath, PageScriptLocation.EndOfBody, order: 10);
        }

        private string GetStyleCssClass()
        {
            return _style.ToString().Replace("Outline", "outline-").ToLowerInvariant();
        }

        /// <inheritdoc />
        protected override void InternalClone(CopyBlockBuilder source)
        {
            base.InternalClone(source);

            _captureWriter = null;
            _originalWriter = null;
            _content = source._content;
            _style = source._style;
            _started = false;
            _disposed = false;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        /// <summary>
        ///     Configures the content for the copy block component.
        /// </summary>
        /// <param name="content">The content value.</param>
        /// <returns>The configured builder instance.</returns>
        public CopyBlockBuilder SetContent(string? content)
        {
            _content = content ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Configures the style for the copy block component.
        /// </summary>
        /// <param name="style">The style value.</param>
        /// <returns>The configured builder instance.</returns>
        public CopyBlockBuilder SetStyle(BootstrapFullKindOfStyle style)
        {
            _style = style;
            return this;
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            string contentId = string.IsNullOrWhiteSpace(GetId())
                ? _htmlHelper.GenerateUniqueId("dmb_copy_block_")
                : GetId();
            string buttonId = $"{contentId}_copy";
            string styleCss = GetStyleCssClass();

            SetId(contentId);

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write($"<div id=\"{HtmlEncoder.Default.Encode(contentId)}_content\" class=\"flex-grow-1 text-break\">");
            writer.Write(_content);
            writer.Write("</div>");
            writer.Write("<button");
            writer.Write($" class=\"btn btn-sm btn-{HtmlEncoder.Default.Encode(styleCss)}\"");
            writer.Write(" type=\"button\"");
            writer.Write($" id=\"{HtmlEncoder.Default.Encode(buttonId)}\"");
            writer.Write(" data-copy-block-button=\"true\"");
            writer.Write($" data-copy-block-target=\"{HtmlEncoder.Default.Encode(contentId)}_content\"");
            writer.Write($" data-copy-block-style=\"btn btn-sm btn-{HtmlEncoder.Default.Encode(styleCss)}\"");
            writer.Write(">");
            writer.Write("<span class=\"bi-clipboard\" aria-hidden=\"true\"></span>");
            writer.Write("<span class=\"visually-hidden\">Copy</span>");
            writer.Write("</button>");
            writer.Write($"</{GetTag()}>");
        }

        #region From interface IDisposable

        /// <summary>
        ///     Completes the active copy block rendering or capture scope.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            if (!_started)
            {
                return;
            }

            if (_originalWriter != null)
            {
                _htmlHelper.ViewContext.Writer = _originalWriter;
            }

            _content = _captureWriter?.ToString() ?? string.Empty;
            WriteTo(_originalWriter ?? _textWriter, HtmlEncoder.Default);
            _started = false;
        }

        #endregion

        #endregion
    }
}