using System.Net;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public sealed class TimelineBuilder :
        HtmlBuilderBase<TimelineBuilder>,
        ICanUseCustomClasses,
        ICanUseTimelineEffects,
        IDisposable
    {
        private const string CurrentContextKey = "__DMB_CURRENT_TIMELINE_BUILDER__";

        private readonly List<TimelineDefinition> _items = new();

        private TimelineBuilder? _previousTimeline;
        private readonly TextWriter _outputWriter;

        private bool _disposed
        {
            get => GetInternal("_disposed", false);
            set => SetInternal("_disposed", value);
        }

        private bool _started
        {
            get => GetInternal("_started", false);
            set => SetInternal("_started", value);
        }

        public TimelineBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            _outputWriter = html.ViewContext.Writer;

            this.AddClass("timeline");
            SetData("timeline", "true");
        }

        public TimelineBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            EnsureAssets();

            _started = true;

            if (string.IsNullOrWhiteSpace(GetId()))
            {
                SetEnsureId("timeline");
            }

            _previousTimeline = GetCurrent(_htmlHelper);
            SetCurrent(_htmlHelper, this);

            return this;
        }

        internal static TimelineBuilder? GetCurrent(IHtmlHelper html)
        {
            if (html?.ViewContext?.HttpContext?.Items == null)
            {
                return null;
            }

            return html.ViewContext.HttpContext.Items.TryGetValue(CurrentContextKey, out object? value)
                ? value as TimelineBuilder
                : null;
        }

        private static void SetCurrent(IHtmlHelper html, TimelineBuilder? timeline)
        {
            if (html?.ViewContext?.HttpContext?.Items == null)
            {
                return;
            }

            if (timeline == null)
            {
                html.ViewContext.HttpContext.Items.Remove(CurrentContextKey);
            }
            else
            {
                html.ViewContext.HttpContext.Items[CurrentContextKey] = timeline;
            }
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet("/css/components/Timeline.css");
            page.SetScriptFile("/js/components/Timeline.js");
        }

        internal void RegisterItem(TimelineDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition);

            int index = _items.Count;

            definition.Id = string.IsNullOrWhiteSpace(definition.Id)
                ? $"{GetId()}_item_{index}"
                : definition.Id;

            definition.IsLeft = index % 2 == 0;

            _items.Add(definition);
        }

        protected override TimelineBuilder CreateInstance()
        {
            return new TimelineBuilder(_textWriter, _htmlHelper);
        }

        protected override void InternalClone(TimelineBuilder source)
        {
            base.InternalClone(source);

            _items.Clear();
            _items.AddRange(source._items.Select(x => new TimelineDefinition
            {
                Id = x.Id,
                DateTitle = x.DateTitle,
                DateSubtitle = x.DateSubtitle,
                Title = x.Title,
                TitleLevel = x.TitleLevel,
                Subtitle = x.Subtitle,
                Icon = x.Icon,
                Variant = x.Variant,
                IsLeft = x.IsLeft,
                ContentHtml = x.ContentHtml
            }));

            _previousTimeline = null;
            _disposed = false;
            _started = false;
        }

        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            if (_items.Count == 0)
            {
                return;
            }

            writer.Write($"<{GetTag()}{this.BuildAttributes()}>");

            foreach (TimelineDefinition item in _items)
            {
                WriteItem(writer, encoder, item);
            }

            writer.Write($"</{GetTag()}>");
        }
        private void WriteItem(TextWriter writer, HtmlEncoder encoder, TimelineDefinition item)
        {
            string sideClass = item.IsLeft ? "timeline-left" : "timeline-right";
            string variant = item.Variant.GetVariantCss();

            if (string.IsNullOrWhiteSpace(variant))
            {
                variant = "primary";
            }

            writer.Write($"<div id=\"{WebUtility.HtmlEncode(item.Id)}\" class=\"timeline-row {sideClass}\">");

            if (item.IsLeft)
            {
                WriteCardSide(writer, encoder, item, variant);
                WriteAxis(writer, variant);
                WriteTimeSide(writer, item);
            }
            else
            {
                WriteTimeSide(writer, item);
                WriteAxis(writer, variant);
                WriteCardSide(writer, encoder, item, variant);
            }

            writer.Write("</div>");
        }

        private void WriteAxis(TextWriter writer, string variant)
        {
            writer.Write("<div class=\"timeline-axis\">");
            writer.Write($"<span class=\"timeline-node border-{WebUtility.HtmlEncode(variant)}\"></span>");
            writer.Write("</div>");
        }

        private void WriteTimeSide(TextWriter writer, TimelineDefinition item)
        {
            writer.Write("<div class=\"timeline-side timeline-time-side\">");
            WriteTime(writer, item);
            writer.Write("</div>");
        }

        private void WriteCardSide(TextWriter writer, HtmlEncoder encoder, TimelineDefinition item, string variant)
        {
            writer.Write("<div class=\"timeline-side timeline-card-side\">");
            writer.Write($"<div class=\"timeline-content timeline-content-{WebUtility.HtmlEncode(variant)}\">");
            
            WriteTitle(writer, encoder, item, variant);
            WriteSubtitle(writer, item);
            WriteContent(writer, item);

            writer.Write("</div>");
            writer.Write("</div>");
        }

        private static void WriteTime(TextWriter writer, TimelineDefinition item)
        {
            writer.Write("<div class=\"timeline-time\">");

            if (!string.IsNullOrWhiteSpace(item.DateTitle))
            {
                writer.Write($"<strong>{WebUtility.HtmlEncode(item.DateTitle)}</strong>");
            }

            if (!string.IsNullOrWhiteSpace(item.DateSubtitle))
            {
                writer.Write($"<small>{WebUtility.HtmlEncode(item.DateSubtitle)}</small>");
            }

            writer.Write("</div>");
        }

        private static void WriteNode(TextWriter writer, string variant)
        {
            writer.Write($"<span class=\"timeline-node border-{WebUtility.HtmlEncode(variant)}\"></span>");
        }

        private void WriteTitle(TextWriter writer, HtmlEncoder encoder, TimelineDefinition item, string variant)
        {
            if (string.IsNullOrWhiteSpace(item.Title) && item.Icon.IsEmpty)
            {
                return;
            }

            writer.Write($"<div class=\"timeline-title bg-{WebUtility.HtmlEncode(variant)}-subtle text-{WebUtility.HtmlEncode(variant)}-emphasis\">");

                if (!item.Icon.IsEmpty)
                {
                    HtmlLayoutExtensions.IconBuilder(_htmlHelper, item.Icon, null, null)
                        .WriteTo(writer, HtmlEncoder.Default);
                }

            if (!string.IsNullOrWhiteSpace(item.Title))
            {
                writer.Write($"<span>{WebUtility.HtmlEncode(item.Title)}</span>");
            }

            writer.Write("</div>");
        }

        private static void WriteSubtitle(TextWriter writer, TimelineDefinition item)
        {
            if (string.IsNullOrWhiteSpace(item.Subtitle))
            {
                return;
            }

            writer.Write($"<div class=\"timeline-subtitle\">{WebUtility.HtmlEncode(item.Subtitle)}</div>");
        }

        private static void WriteContent(TextWriter writer, TimelineDefinition item)
        {
            if (string.IsNullOrWhiteSpace(item.ContentHtml))
            {
                return;
            }

            writer.Write("<div class=\"timeline-description\">");
            writer.Write(item.ContentHtml);
            writer.Write("</div>");
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            SetCurrent(_htmlHelper, _previousTimeline);

            if (!_started)
            {
                return;
            }

            WriteTo(_outputWriter, HtmlEncoder.Default);
        }
    }
}