#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj CodeBlockBuilder.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Net;
using System.Text.Encodings.Web;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Renders a syntax-highlighted code block with optional title, line numbers, and copy action.
    /// </summary>
    /// <remarks>
    /// The component renders accessible server-side HTML and registers local code block assets through
    /// <see cref="PageInformation"/> when syntax highlighting is enabled.
    /// </remarks>
    public sealed class CodeBlockBuilder :
        HtmlBuilderBase<CodeBlockBuilder>,
        ICanUseCustomClasses,
        IDisposable
    {
        private const string CodeBlockCssPath = "/css/components/CodeBlock.css";
        private const string CodeBlockJsPath = "/js/components/CodeBlock.js";

        private string _code = string.Empty;
        private StringWriter? _captureWriter;
        private string? _downloadFileName;
        private string? _title;
        private string? _emptyMessage = "No code to display.";
        private string? _errorMessage;
        private TextWriter? _originalWriter;
        private CodeLanguage _language = CodeLanguage.PlainText;
        private CodeBlockTheme _theme = CodeBlockTheme.Default;
        private bool _disposed;
        private bool _renderCapturedRawHtml;
        private bool _showLineNumbers;
        private bool _showCopyButton;
        private bool _showLanguageName;
        private bool _highlight = true;
        private bool _compact;
        private bool _started;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeBlockBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer used by the current rendering context.</param>
        /// <param name="html">The HTML helper associated with the current Razor view.</param>
        public CodeBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClass("dmb-code-block");
            SetData("code-block", "true");
            SetCssComposer(new CodeBlockComposer());
        }

        /// <summary>
        /// Sets the code displayed by the component.
        /// </summary>
        /// <param name="code">The code to render.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetCode(string? code)
        {
            _code = code ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets the syntax language used for highlighting.
        /// </summary>
        /// <param name="language">The language to apply.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetLanguage(CodeLanguage language)
        {
            _language = language;
            return this;
        }

        /// <summary>
        /// Sets the title displayed in the code block header.
        /// </summary>
        /// <param name="title">The optional title to display.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetTitle(string? title)
        {
            _title = title;
            return this;
        }

        /// <summary>
        /// Sets the theme used by the code block surface.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetTheme(CodeBlockTheme theme)
        {
            _theme = theme;
            CodeBlockComposer composer = GetOrCreateCssComposer(() => new CodeBlockComposer());
            composer.SetTheme(theme);
            return this;
        }

        /// <summary>
        /// Shows or hides line numbers.
        /// </summary>
        /// <param name="value">A value indicating whether line numbers should be displayed.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetShowLineNumbers(bool value = true)
        {
            _showLineNumbers = value;
            return this;
        }

        /// <summary>
        /// Shows or hides the copy action.
        /// </summary>
        /// <param name="value">A value indicating whether the copy action should be displayed.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetCopyButton(bool value = true)
        {
            _showCopyButton = value;
            return this;
        }
        /// <summary>
        /// Configures the download file name for the code block component.
        /// </summary>
        /// <param name="fileName">The file name value.</param>
        /// <returns>The configured builder instance.</returns>
        public CodeBlockBuilder SetDownloadFileName(string? fileName)
        {
            _downloadFileName = fileName;
            return this;
        }

        /// <summary>
        /// Shows or hides the language name next to the language icon.
        /// </summary>
        /// <param name="value">A value indicating whether the language name should be displayed.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetShowLanguageName(bool value = true)
        {
            _showLanguageName = value;
            return this;
        }

        /// <summary>
        /// Enables or disables client-side syntax highlighting.
        /// </summary>
        /// <param name="value">A value indicating whether syntax highlighting should be enabled.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetHighlight(bool value = true)
        {
            _highlight = value;
            CodeBlockComposer composer = GetOrCreateCssComposer(() => new CodeBlockComposer());
            composer.SetHighlightDisabled(!value);
            return this;
        }

        /// <summary>
        /// Enables or disables the compact layout.
        /// </summary>
        /// <param name="value">A value indicating whether compact spacing should be used.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetCompact(bool value = true)
        {
            _compact = value;
            CodeBlockComposer composer = GetOrCreateCssComposer(() => new CodeBlockComposer());
            composer.SetCompact(value);
            return this;
        }

        /// <summary>
        /// Sets the message displayed when the code is empty.
        /// </summary>
        /// <param name="message">The empty-state message to display.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetEmptyMessage(string? message)
        {
            _emptyMessage = message;
            return this;
        }

        /// <summary>
        /// Sets an error message displayed instead of the code content.
        /// </summary>
        /// <param name="message">The optional error message.</param>
        /// <returns>The current builder instance.</returns>
        public CodeBlockBuilder SetErrorMessage(string? message)
        {
            _errorMessage = message;
            return this;
        }
        /// <summary>
        /// Starts the code block rendering or capture scope.
        /// </summary>
        /// <param name="renderCapturedRawHtml">The render captured raw html value.</param>
        /// <returns>The configured builder instance.</returns>
        public CodeBlockBuilder Begin(bool renderCapturedRawHtml = true)
        {
            if (_started)
            {
                return this;
            }

            _started = true;
            _renderCapturedRawHtml = renderCapturedRawHtml;
            _originalWriter = _htmlHelper.ViewContext.Writer;
            _captureWriter = new StringWriter();
            _htmlHelper.ViewContext.Writer = _captureWriter;

            return this;
        }

        /// <summary>
        /// Renders the code block to an HTML content instance.
        /// </summary>
        /// <returns>The generated HTML content.</returns>
        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        /// <inheritdoc/>
        protected override CodeBlockBuilder CreateInstance()
        {
            return new CodeBlockBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc/>
        protected override void InternalClone(CodeBlockBuilder source)
        {
            base.InternalClone(source);
            _code = source._code;
            _captureWriter = null;
            _downloadFileName = source._downloadFileName;
            _title = source._title;
            _emptyMessage = source._emptyMessage;
            _errorMessage = source._errorMessage;
            _originalWriter = null;
            _language = source._language;
            _theme = source._theme;
            _disposed = false;
            _renderCapturedRawHtml = source._renderCapturedRawHtml;
            _showLineNumbers = source._showLineNumbers;
            _showCopyButton = source._showCopyButton;
            _showLanguageName = source._showLanguageName;
            _highlight = source._highlight;
            _compact = source._compact;
            _started = false;
        }

        /// <inheritdoc/>
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();
            string language = _highlight ? _language.GetCodeBlockLanguage() : "none";
            string languageClass = $"code-language-{language}";
            string codeId = string.IsNullOrWhiteSpace(GetId())
                ? _htmlHelper.GenerateUniqueId("dmb_code_block_")
                : GetId();

            SetId(codeId);
            SetData("code-block-language", language);
            SetData("code-block-highlight", _highlight);
            SetData("code-block-line-numbers", _showLineNumbers);
            SetData("code-block-theme", _theme.ToString().ToLowerInvariant());

            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                SetData("code-block-state", "error");
            }
            else if (string.IsNullOrWhiteSpace(_code))
            {
                SetData("code-block-state", "empty");
            }
            else
            {
                SetData("code-block-state", "normal");
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            WriteHeader(writer);

            if (!string.IsNullOrWhiteSpace(_errorMessage))
            {
                WriteState(writer, "error", _errorMessage);
            }
            else if (string.IsNullOrWhiteSpace(_code))
            {
                WriteState(writer, "empty", _emptyMessage);
            }
            else
            {
                WriteCode(writer, encoder, languageClass);
            }

            writer.Write($"</{GetTag()}>");
        }
        /// <summary>
        /// Completes the active code block rendering or capture scope.
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

            _code = _captureWriter?.ToString() ?? string.Empty;
            WriteTo(_originalWriter ?? _textWriter, HtmlEncoder.Default);
            _started = false;
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(CodeBlockCssPath, 10);
            page.SetScriptFile(CodeBlockJsPath, PageScriptLocation.EndOfBody, order: 10);
        }

        private void WriteHeader(TextWriter writer)
        {
            if (string.IsNullOrWhiteSpace(_title) && !_showCopyButton && string.IsNullOrWhiteSpace(_downloadFileName))
            {
                return;
            }

            writer.Write("<div class=\"dmb-code-block-header\">");
            writer.Write("<div class=\"dmb-code-block-meta\">");
            writer.Write($"<span class=\"dmb-code-block-language{(_showLanguageName ? string.Empty : " dmb-code-block-language-icon-only")}\" title=\"{WebUtility.HtmlEncode(_language.GetDisplayName())}\">");
            writer.Write($"<span class=\"bi {WebUtility.HtmlEncode(_language.GetBootstrapIconClass())}\" aria-hidden=\"true\"></span>");
            if (_showLanguageName)
            {
                writer.Write("<span>");
                writer.Write(WebUtility.HtmlEncode(_language.GetDisplayName()));
                writer.Write("</span>");
            }
            writer.Write("</span>");

            if (!string.IsNullOrWhiteSpace(_title))
            {
                writer.Write("<span class=\"dmb-code-block-title\">");
                writer.Write(WebUtility.HtmlEncode(_title));
                writer.Write("</span>");
            }

            writer.Write("</div>");

            bool hasActions = _showCopyButton || !string.IsNullOrWhiteSpace(_downloadFileName);
            if (hasActions)
            {
                writer.Write("<div class=\"dmb-code-block-actions\">");
            }

            if (_showCopyButton)
            {
                writer.Write("<button type=\"button\" class=\"btn btn-sm btn-outline-secondary dmb-code-block-action dmb-code-block-copy\" data-code-block-copy=\"true\" title=\"Copy code\">");
                writer.Write("<span class=\"bi bi-clipboard\" aria-hidden=\"true\"></span>");
                writer.Write("<span class=\"visually-hidden\">Copy code</span>");
                writer.Write("</button>");
            }

            if (!string.IsNullOrWhiteSpace(_downloadFileName))
            {
                string fileName = JavaScriptEncoder.Default.Encode(_downloadFileName);
                writer.Write($"<button type=\"button\" class=\"btn btn-sm btn-outline-secondary dmb-code-block-action dmb-code-block-download\" title=\"Download code\" onclick=\"const code=this.closest('[data-code-block]').querySelector('code')?.textContent||'';const file=new Blob([code],{{type:'text/plain'}});const link=document.createElement('a');link.href=URL.createObjectURL(file);link.download='{fileName}';document.body.appendChild(link);link.click();document.body.removeChild(link);URL.revokeObjectURL(link.href);\">");
                writer.Write("<span class=\"bi bi-download\" aria-hidden=\"true\"></span>");
                writer.Write("<span class=\"visually-hidden\">Download code</span>");
                writer.Write("</button>");
            }

            if (hasActions)
            {
                writer.Write("</div>");
            }

            writer.Write("</div>");
        }

        private void WriteState(TextWriter writer, string state, string? message)
        {
            writer.Write($"<div class=\"dmb-code-block-state dmb-code-block-state-{WebUtility.HtmlEncode(state)}\">");
            writer.Write(WebUtility.HtmlEncode(message ?? string.Empty));
            writer.Write("</div>");
        }

        private void WriteCode(TextWriter writer, HtmlEncoder encoder, string languageClass)
        {
            string lineNumbersClass = _showLineNumbers ? " dmb-code-block-with-lines" : string.Empty;
            string compactClass = _compact ? " dmb-code-block-pre-compact" : string.Empty;

            writer.Write($"<pre class=\"dmb-code-block-pre {languageClass}{lineNumbersClass}{compactClass}\"><code class=\"dmb-code-block-code {languageClass}\">");
            if (_renderCapturedRawHtml)
            {
                writer.Write(_code);
            }
            else
            {
                encoder.Encode(writer, _code);
            }
            writer.Write("</code></pre>");
        }
    }
}
