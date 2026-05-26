#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj FaqBuilder.cs create at 2026/05/12
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
    /// Renders a Bootstrap based frequently asked questions area.
    /// </summary>
    public sealed class FaqBuilder :
        HtmlBuilderBase<FaqBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin
    {
        private readonly List<FaqItem> _items = new();

        private string _title = "Frequently asked questions";
        private IconStruct _icon = IconStruct.Bootstrap("bi-question-square");
        private string _emptyMessage = string.Empty;
        private string _contactSeparatorText = string.Empty;
        private string _contactText = string.Empty;
        private string _contactUrl = string.Empty;
        private bool _showIfEmpty = true;

        public FaqBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "section";
            InternalAddClasses("faq-builder", "card", "mt-3");
            SetData("faq", "true");
        }

        public FaqBuilder SetTitle(string? title, IconStruct icon = default)
        {
            _title = title ?? string.Empty;
            _icon = icon.IsEmpty ? _icon : icon;
            return this;
        }

        public FaqBuilder SetEmptyMessage(string? message)
        {
            _emptyMessage = message ?? string.Empty;
            return this;
        }

        public FaqBuilder SetShowIfEmpty(bool showIfEmpty)
        {
            _showIfEmpty = showIfEmpty;
            return this;
        }

        public FaqBuilder SetContactAction(string? separatorText, string? text, string? url)
        {
            _contactSeparatorText = separatorText ?? string.Empty;
            _contactText = text ?? string.Empty;
            _contactUrl = url ?? string.Empty;
            return this;
        }

        public FaqBuilder AddItem(FaqItem? item)
        {
            if (item != null)
            {
                _items.Add(item);
            }

            return this;
        }

        public FaqBuilder AddItem(string? question, string? answer)
        {
            return AddItem(new FaqItem
            {
                Question = question ?? string.Empty,
                Answer = answer ?? string.Empty
            });
        }

        public FaqBuilder AddItems(IEnumerable<FaqItem>? items)
        {
            if (items == null)
            {
                return this;
            }

            foreach (FaqItem item in items)
            {
                AddItem(item);
            }

            return this;
        }

        public override IHtmlContent Render()
        {
            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        protected override FaqBuilder CreateInstance()
        {
            return new FaqBuilder(_textWriter, _htmlHelper);
        }

        protected override void InternalClone(FaqBuilder source)
        {
            base.InternalClone(source);
            _items.Clear();
            _items.AddRange(source._items);
            _title = source._title;
            _icon = source._icon;
            _emptyMessage = source._emptyMessage;
            _contactSeparatorText = source._contactSeparatorText;
            _contactText = source._contactText;
            _contactUrl = source._contactUrl;
            _showIfEmpty = source._showIfEmpty;
        }

        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            List<FaqItem> visibleItems = _items
                .Where(item => item.IsVisible || item.ActionContent != null || item.HiddenNotice != null)
                .ToList();

            if (!_showIfEmpty && visibleItems.Count == 0)
            {
                writer.Write("<!-- FAQ empty -->");
                return;
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            WriteHeader(writer, encoder);
            writer.Write("<div class=\"card-body\">");

            if (visibleItems.Count > 0)
            {
                writer.Write("<div class=\"row g-3\">");
                foreach (FaqItem item in visibleItems)
                {
                    WriteItem(writer, encoder, item);
                }
                writer.Write("</div>");
            }
            else if (!string.IsNullOrWhiteSpace(_emptyMessage))
            {
                writer.Write("<div class=\"row\"><div class=\"col-12\">");
                encoder.Encode(writer, _emptyMessage);
                writer.Write("</div></div>");
            }

            WriteContactAction(writer, encoder, visibleItems.Count > 0);
            writer.Write("</div>");
            writer.Write($"</{GetTag()}>");
        }

        private void WriteHeader(TextWriter writer, HtmlEncoder encoder)
        {
            writer.Write("<div class=\"card-header d-flex align-items-center gap-2\">");
            _htmlHelper.IconBuilder(_icon).WriteTo(writer, encoder);
            writer.Write("<h5 class=\"mb-0\">");
            encoder.Encode(writer, _title);
            writer.Write("</h5></div>");
        }

        private void WriteItem(TextWriter writer, HtmlEncoder encoder, FaqItem item)
        {
            string visibilityClass = item.IsVisible ? string.Empty : " opacity-25";

            writer.Write("<div class=\"col-12 col-lg-6\">");
            writer.Write("<div class=\"d-flex justify-content-between gap-2\">");
            writer.Write($"<h5 class=\"fs-6{visibilityClass}\">");
            encoder.Encode(writer, item.Question);
            writer.Write("</h5>");
            item.ActionContent?.WriteTo(writer, encoder);
            writer.Write("</div>");

            if (item.HiddenNotice != null)
            {
                item.HiddenNotice.WriteTo(writer, encoder);
            }

            WriteIconLine(writer, encoder, item, visibilityClass);
            writer.Write($"<p class=\"small mb-0{visibilityClass}\">");
            encoder.Encode(writer, item.Answer);
            writer.Write("</p>");
            writer.Write("</div>");
        }

        private void WriteIconLine(TextWriter writer, HtmlEncoder encoder, FaqItem item, string visibilityClass)
        {
            if (item.StartIcons.Count == 0 && item.EndIcons.Count == 0)
            {
                return;
            }

            writer.Write($"<div class=\"d-flex justify-content-between text-muted mb-2{visibilityClass}\"><div class=\"d-flex gap-2\">");
            foreach (IconStruct icon in item.StartIcons)
            {
                _htmlHelper.IconBuilder(icon).WriteTo(writer, encoder);
            }

            writer.Write("</div><div class=\"d-flex gap-2\">");
            foreach (IconStruct icon in item.EndIcons)
            {
                _htmlHelper.IconBuilder(icon).WriteTo(writer, encoder);
            }

            writer.Write("</div></div>");
        }

        private void WriteContactAction(TextWriter writer, HtmlEncoder encoder, bool hasItems)
        {
            if (string.IsNullOrWhiteSpace(_contactText) || string.IsNullOrWhiteSpace(_contactUrl))
            {
                return;
            }

            string separatorText = string.IsNullOrWhiteSpace(_contactSeparatorText)
                ? hasItems ? "More questions?" : "Any questions?"
                : _contactSeparatorText;

            _htmlHelper.SeparatorText(separatorText).WriteTo(writer, encoder);
            writer.Write("<div class=\"text-center p-4\"><a class=\"btn btn-primary btn-sm\" href=\"");
            encoder.Encode(writer, _contactUrl);
            writer.Write("\">");
            encoder.Encode(writer, _contactText);
            writer.Write("</a></div>");
        }
    }
}
