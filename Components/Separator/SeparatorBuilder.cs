#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj SeparatorBuilder.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;
using DMBPageBuilder;
using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Renders a horizontal separator with optional text or icon content.
    /// </summary>
    public sealed class SeparatorBuilder :
        HtmlBuilderBase<SeparatorBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin
    {
        private string _text = string.Empty;
        private IconStruct _icon = IconStruct.Empty;
        private bool _pageSpacing;

        public SeparatorBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("d-flex", "align-items-center");
            SetData("separator", "true");
        }

        public SeparatorBuilder SetText(string? text)
        {
            _text = text ?? string.Empty;
            _icon = IconStruct.Empty;
            return this;
        }

        public SeparatorBuilder SetIcon(IconStruct icon)
        {
            _icon = icon;
            _text = string.Empty;
            return this;
        }

        public SeparatorBuilder SetIcon(string? icon)
        {
            return SetIcon(IconStruct.Parse(icon));
        }

        public SeparatorBuilder UsePageSpacing(bool active = true)
        {
            _pageSpacing = active;

            if (_pageSpacing)
            {
                InternalAddClasses("mt-5", "mb-5");
            }

            return this;
        }

        public override IHtmlContent Render()
        {
            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        protected override SeparatorBuilder CreateInstance()
        {
            return new SeparatorBuilder(_textWriter, _htmlHelper);
        }

        protected override void InternalClone(SeparatorBuilder source)
        {
            base.InternalClone(source);
            _text = source._text;
            _icon = source._icon;
            _pageSpacing = source._pageSpacing;
        }

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
    }
}
