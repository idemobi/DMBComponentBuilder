using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Builds and renders the timeline visual component for Razor views.
    /// </summary>
    public sealed class TimelineBlockBuilder :
        HtmlBuilderBase<TimelineBlockBuilder>,
        IDisposable
    {
        private StringWriter? _captureWriter;
        private TextWriter? _originalWriter;

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

        private string? _dateTitle
        {
            get => GetInternal<string?>("_dateTitle", null);
            set => SetInternal("_dateTitle", value);
        }

        private string? _dateSubtitle
        {
            get => GetInternal<string?>("_dateSubtitle", null);
            set => SetInternal("_dateSubtitle", value);
        }

        private string? _title
        {
            get => GetInternal<string?>("_title", null);
            set => SetInternal("_title", value);
        }

        private TitleLevel _titleLevel
        {
            get => GetInternal("_titleLevel", TitleLevel.Four);
            set => SetInternal("_titleLevel", value);
        }

        private string? _subtitle
        {
            get => GetInternal<string?>("_subtitle", null);
            set => SetInternal("_subtitle", value);
        }

        private IconStruct _icon
        {
            get => GetInternal<IconStruct>("_icon", IconStruct.Empty);
            set => SetInternal("_icon", value);
        }

        private VariantStyle _variant
        {
            get => GetInternal("_variant", VariantStyle.Primary);
            set => SetInternal("_variant", value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TimelineBlockBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public TimelineBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            SetData("timeline-block", "true");
        }
        /// <summary>
        /// Starts the timeline rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public TimelineBlockBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            TimelineBuilder? timeline = TimelineBuilder.GetCurrent(_htmlHelper);
            if (timeline == null)
            {
                throw new InvalidOperationException("TimelineBlockBuilder must be opened inside a TimelineBuilder context.");
            }

            _started = true;
            _originalWriter = _htmlHelper.ViewContext.Writer;
            _captureWriter = new StringWriter();

            _htmlHelper.ViewContext.Writer = _captureWriter;

            return this;
        }
        /// <summary>
        /// Configures the date for the timeline component.
        /// </summary>
        /// <param name="dateTitle">The date title value.</param>
        /// <param name="dateSubtitle">The date subtitle value.</param>
        /// <returns>The configured builder instance.</returns>
        public TimelineBlockBuilder WithDate(string? dateTitle, string? dateSubtitle = null)
        {
            _dateTitle = dateTitle;
            _dateSubtitle = dateSubtitle;
            return this;
        }
        /// <summary>
        /// Configures the title for the timeline component.
        /// </summary>
        /// <param name="title">The title value.</param>
        /// <param name="level">The level value.</param>
        /// <returns>The configured builder instance.</returns>
        public TimelineBlockBuilder WithTitle(string? title, TitleLevel level = TitleLevel.Four)
        {
            _title = title;
            _titleLevel = level;
            return this;
        }
        /// <summary>
        /// Configures the subtitle for the timeline component.
        /// </summary>
        /// <param name="subtitle">The subtitle value.</param>
        /// <returns>The configured builder instance.</returns>
        public TimelineBlockBuilder WithSubtitle(string? subtitle)
        {
            _subtitle = subtitle;
            return this;
        }
        /// <summary>
        /// Configures the icon for the timeline component.
        /// </summary>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public TimelineBlockBuilder WithIcon(IconStruct icon)
        {
            _icon = icon;
            return this;
        }
        /// <summary>
        /// Configures the variant for the timeline component.
        /// </summary>
        /// <param name="variant">The variant value.</param>
        /// <returns>The configured builder instance.</returns>
        public TimelineBlockBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }
        /// <inheritdoc />
        protected override TimelineBlockBuilder CreateInstance()
        {
            return new TimelineBlockBuilder(_textWriter, _htmlHelper);
        }
        /// <inheritdoc />
        protected override void InternalClone(TimelineBlockBuilder source)
        {
            base.InternalClone(source);

            _captureWriter = null;
            _originalWriter = null;

            _disposed = false;
            _started = false;

            _dateTitle = source._dateTitle;
            _dateSubtitle = source._dateSubtitle;
            _title = source._title;
            _titleLevel = source._titleLevel;
            _subtitle = source._subtitle;
            _icon = source._icon;
            _variant = source._variant;
        }
        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            throw new InvalidOperationException("TimelineBlockBuilder does not render directly. Use Begin()/Dispose() inside TimelineBuilder.");
        }
        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            throw new InvalidOperationException("TimelineBlockBuilder does not render directly. Use Begin()/Dispose() inside TimelineBuilder.");
        }
        /// <summary>
        /// Completes the active timeline rendering or capture scope.
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

            TimelineBuilder? timeline = TimelineBuilder.GetCurrent(_htmlHelper);
            if (timeline == null)
            {
                throw new InvalidOperationException("No active TimelineBuilder context found while closing TimelineBlockBuilder.");
            }

            timeline.RegisterItem(new TimelineDefinition
            {
                Id = GetId(),
                DateTitle = _dateTitle,
                DateSubtitle = _dateSubtitle,
                Title = _title,
                TitleLevel = _titleLevel,
                Subtitle = _subtitle,
                Icon = _icon,
                Variant = _variant,
                ContentHtml = _captureWriter?.ToString() ?? string.Empty
            });
        }
    }
}