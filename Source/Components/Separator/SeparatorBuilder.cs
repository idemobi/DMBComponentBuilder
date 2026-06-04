#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

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
    ///     Renders a horizontal separator with optional text or icon content.
    /// </summary>
    public sealed class SeparatorBuilder :
        HtmlBuilderBase<SeparatorBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin
    {
        #region Instance fields and properties

        private IconStruct _icon = IconStruct.Empty;
        private bool _pageSpacing;
        private string _text = string.Empty;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="SeparatorBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public SeparatorBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("d-flex", "align-items-center");
            SetData("separator", "true");
        }

        #endregion

        #region Instance methods

        /// <inheritdoc />
        protected override SeparatorBuilder CreateInstance()
        {
            return new SeparatorBuilder(_textWriter, _htmlHelper);
        }

        /// <inheritdoc />
        protected override void InternalClone(SeparatorBuilder source)
        {
            base.InternalClone(source);
            _text = source._text;
            _icon = source._icon;
            _pageSpacing = source._pageSpacing;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        /// <summary>
        ///     Configures the icon for the separator component.
        /// </summary>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public SeparatorBuilder SetIcon(IconStruct icon)
        {
            _icon = icon;
            _text = string.Empty;
            return this;
        }

        /// <summary>
        ///     Configures the icon for the separator component.
        /// </summary>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public SeparatorBuilder SetIcon(string? icon)
        {
            return SetIcon(IconStruct.Parse(icon));
        }

        /// <summary>
        ///     Configures the text for the separator component.
        /// </summary>
        /// <param name="text">The text value.</param>
        /// <returns>The configured builder instance.</returns>
        public SeparatorBuilder SetText(string? text)
        {
            _text = text ?? string.Empty;
            _icon = IconStruct.Empty;
            return this;
        }

        /// <summary>
        ///     Configures whether the page spacing option is used by the separator component.
        /// </summary>
        /// <param name="active">True to mark the item active; false to clear the active state.</param>
        /// <returns>The configured builder instance.</returns>
        public SeparatorBuilder UsePageSpacing(bool active = true)
        {
            _pageSpacing = active;

            if (_pageSpacing)
            {
                InternalAddClasses("mt-5", "mb-5");
            }

            return this;
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write("<hr class=\"flex-grow-1\">");
            writer.Write("<span class=\"mx-3 text-muted d-inline-flex align-items-center gap-1\">");

            if (!_icon.IsEmpty)
            {
                _htmlHelper.IconBuilder(_icon).WriteTo(writer, encoder);
            }
            else
            {
                encoder.Encode(writer, _text);
            }

            writer.Write("</span>");
            writer.Write("<hr class=\"flex-grow-1\">");
            writer.Write($"</{GetTag()}>");
        }

        #endregion
    }
}