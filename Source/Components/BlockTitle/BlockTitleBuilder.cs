#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Collections.Generic;
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
    ///     Renders a structured block composed of a title and one or more subtitle lines of varying
    ///     sizes. Replaces the repetitive pattern of a <c>TitleBuilder</c> followed by one or several
    ///     <c>&lt;p class="text-muted"&gt;</c> paragraphs.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> page section intros, feature block headings, landing-page copy blocks,
    ///         card preambles.
    ///     </para>
    ///     <para>
    ///         <b>Example:</b>
    ///         <code>
    /// @Html.BlockTitleBuilder()
    ///     .SetTitle("A stable foundation", TitleLevel.Two)
    ///     .SetIconBootstrap("bi-boxes")
    ///     .AddSubtitle("Each package covers a specific part of web rendering.")
    ///     .AddSubtitle("No changes to the way your team works in Razor.", SubtitleSize.Small)
    ///     .Render()
    /// </code>
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class BlockTitleBuilder :
        HtmlBuilderBase<BlockTitleBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin,
        ICanUsePadding,
        ICanUseWidth,
        ICanUseHeight,
        ICanUseTextAlign,
        ICanUseShadow,
        ICanUseTextShadow,
        ICanUseBorder,
        ICanUseBorderRadius,
        ICanUseBackgroundVariant,
        ICanUseTextVariant,
        ICanUseOpacity,
        ICanUseThemeVisibility
    {
        #region Instance fields and properties

        private IconStruct _icon = IconStruct.Empty;
        private readonly List<BlockTitleSubtitle> _subtitles = new();
        private Shadow? _textShadow;
        private VariantStyle? _textVariant;

        private string _title = string.Empty;
        private TitleLevel _titleLevel = TitleLevel.Two;
        private Shadow? _titleShadow;
        private VariantStyle? _titleVariant;

        #endregion

        #region Instance constructors and destructors

        /// <summary>Initializes a new <see cref="BlockTitleBuilder" />.</summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public BlockTitleBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("block-title-builder");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Appends a subtitle line below the title.
        /// </summary>
        /// <param name="text">The subtitle text content.</param>
        /// <param name="size">The display size. Defaults to <see cref="SubtitleSize.Normal" />.</param>
        /// <param name="textVariant">
        ///     Optional per-line color override. When set, takes precedence over
        ///     <see cref="SetTextVariant" />. Omit to inherit the builder-level variant.
        /// </param>
        /// <param name="textShadow">
        ///     Optional per-line shadow override. When set, takes precedence over
        ///     <see cref="SetTextShadow" />. Omit to inherit the builder-level shadow.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder AddSubtitle(
            string text,
            SubtitleSize size = SubtitleSize.Normal,
            VariantStyle? textVariant = null,
            Shadow? textShadow = null
        )
        {
            if (!string.IsNullOrWhiteSpace(text)) _subtitles.Add(new BlockTitleSubtitle { Text = text, Size = size, TextVariant = textVariant, TextShadow = textShadow });
            return this;
        }

        /// <inheritdoc />
        protected override BlockTitleBuilder CreateInstance() => new(_textWriter, _htmlHelper);

        /// <inheritdoc />
        protected override void InternalClone(BlockTitleBuilder source)
        {
            base.InternalClone(source);
            _subtitles.Clear();
            _subtitles.AddRange(source._subtitles);
            _title = source._title;
            _titleLevel = source._titleLevel;
            _icon = source._icon;
            _textVariant = source._textVariant;
            _textShadow = source._textShadow;
            _titleVariant = source._titleVariant;
            _titleShadow = source._titleShadow;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            using StringWriter sw = new();
            WriteToCore(sw, HtmlEncoder.Default);
            return new HtmlString(sw.ToString());
        }

        private string ResolveSubtitleColorClass(BlockTitleSubtitle subtitle)
        {
            VariantStyle? effective = subtitle.TextVariant ?? _textVariant;
            if (!effective.HasValue) return "text-muted";

            string v = effective.Value.GetVariantCss();
            return string.IsNullOrWhiteSpace(v) ? "text-muted" : $"text-{v}";
        }

        private string ResolveSubtitleShadowClass(BlockTitleSubtitle subtitle)
        {
            Shadow? effective = subtitle.TextShadow ?? _textShadow;
            if (!effective.HasValue) return string.Empty;

            string css = effective.Value.GetTextCss();
            return string.IsNullOrWhiteSpace(css) ? string.Empty : $" {css}";
        }

        /// <summary>
        ///     Sets an icon displayed to the left of the title.
        /// </summary>
        /// <param name="icon">The <see cref="IconStruct" /> representing the icon.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetIcon(IconStruct icon)
        {
            _icon = icon;
            return this;
        }

        /// <summary>
        ///     Sets a Bootstrap icon by its CSS class name, displayed to the left of the title.
        /// </summary>
        /// <param name="iconClass">
        ///     The Bootstrap icon CSS class (e.g. <c>"bi-boxes"</c>).
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetIconBootstrap(string iconClass)
        {
            _icon = IconStruct.Bootstrap(iconClass);
            return this;
        }

        /// <summary>
        ///     Sets the text shadow applied to the title and all subtitle lines that have no individual
        ///     override.
        /// </summary>
        /// <param name="shadow">The <see cref="Shadow" /> intensity to apply.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetTextShadow(Shadow shadow)
        {
            _textShadow = shadow;
            return this;
        }

        /// <summary>
        ///     Sets the text color variant applied to the title and all subtitle lines that have no
        ///     individual override.
        /// </summary>
        /// <param name="variant">The <see cref="VariantStyle" /> to apply.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetTextVariant(VariantStyle variant)
        {
            _textVariant = variant;
            return this;
        }

        /// <summary>
        ///     Sets the title text and heading level.
        /// </summary>
        /// <param name="title">The title text to display.</param>
        /// <param name="level">
        ///     The HTML heading level. Defaults to <see cref="TitleLevel.Two" />.
        /// </param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetTitle(string title, TitleLevel level = TitleLevel.Two)
        {
            _title = title ?? string.Empty;
            _titleLevel = level;
            return this;
        }

        /// <summary>
        ///     Overrides the text shadow for the title line only, independently of
        ///     <see cref="SetTextShadow" />.
        /// </summary>
        /// <param name="shadow">The <see cref="Shadow" /> intensity to apply to the title.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetTitleShadow(Shadow shadow)
        {
            _titleShadow = shadow;
            return this;
        }

        /// <summary>
        ///     Overrides the text color for the title line only, independently of
        ///     <see cref="SetTextVariant" />.
        /// </summary>
        /// <param name="variant">The <see cref="VariantStyle" /> to apply to the title.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public BlockTitleBuilder SetTitleVariant(VariantStyle variant)
        {
            _titleVariant = variant;
            return this;
        }

        private void WriteSubtitles(TextWriter writer, HtmlEncoder encoder)
        {
            for (int i = 0; i < _subtitles.Count; i++)
            {
                BlockTitleSubtitle subtitle = _subtitles[i];
                bool isLast = i == _subtitles.Count - 1;
                string margin = isLast ? "mb-0" : "mb-1";

                string colorClass = ResolveSubtitleColorClass(subtitle);
                string shadowClass = ResolveSubtitleShadowClass(subtitle);

                string cssClass = subtitle.Size switch
                {
                    SubtitleSize.Lead => $"lead {colorClass} {margin}{shadowClass}",
                    SubtitleSize.Small => $"small {colorClass} {margin}{shadowClass}",
                    _ => $"{colorClass} {margin}{shadowClass}"
                };

                writer.Write($"<p class=\"{cssClass.Trim()}\">");
                encoder.Encode(writer, subtitle.Text);
                writer.Write("</p>");
            }
        }

        private void WriteTitle(TextWriter writer, HtmlEncoder encoder)
        {
            if (string.IsNullOrWhiteSpace(_title)) return;

            var tb = _htmlHelper.TitleBuilder().SetTitle(_title, _titleLevel);

            if (!_icon.IsEmpty) tb = tb.SetIcon(_icon);

            VariantStyle? effectiveVariant = _titleVariant ?? _textVariant;
            Shadow? effectiveShadow = _titleShadow ?? _textShadow;

            if (effectiveVariant.HasValue) tb = tb.SetTextVariant(effectiveVariant.Value);

            if (effectiveShadow.HasValue) tb = tb.SetTextShadow(effectiveShadow.Value);

            tb.Render().WriteTo(writer, encoder);
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write($"<div{BuildAttributes()}>");
            WriteTitle(writer, encoder);
            WriteSubtitles(writer, encoder);
            writer.Write("</div>");
        }

        #endregion
    }
}