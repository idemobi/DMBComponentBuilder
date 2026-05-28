using System.Text.Encodings.Web;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Builds and renders the roadmap visual component for Razor views.
    /// </summary>
    public sealed class RoadmapBlockBuilder :
        HtmlBuilderBase<RoadmapBlockBuilder>,
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

        private string? _title
        {
            get => GetInternal<string?>("_title", null);
            set => SetInternal("_title", value);
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

        private RoadmapState _state
        {
            get => GetInternal("_state", RoadmapState.Future);
            set => SetInternal("_state", value);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoadmapBlockBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public RoadmapBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            SetData("roadmap-block", "true");
        }
        /// <summary>
        /// Starts the roadmap rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            RoadmapBuilder? roadmap = RoadmapBuilder.GetCurrent(_htmlHelper);
            if (roadmap == null)
            {
                throw new InvalidOperationException("RoadmapBlockBuilder must be opened inside a RoadmapBuilder context.");
            }

            _started = true;
            _originalWriter = _htmlHelper.ViewContext.Writer;
            _captureWriter = new StringWriter();

            _htmlHelper.ViewContext.Writer = _captureWriter;

            return this;
        }
        /// <summary>
        /// Configures the title for the roadmap component.
        /// </summary>
        /// <param name="title">The title value.</param>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder WithTitle(string? title)
        {
            _title = title;
            return this;
        }
        /// <summary>
        /// Configures the subtitle for the roadmap component.
        /// </summary>
        /// <param name="subtitle">The subtitle value.</param>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder WithSubtitle(string? subtitle)
        {
            _subtitle = subtitle;
            return this;
        }
        /// <summary>
        /// Configures the date for the roadmap component.
        /// </summary>
        /// <param name="dateTitle">The date title value.</param>
        /// <param name="dateSubtitle">The date subtitle value.</param>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder WithDate(string? dateTitle, string? dateSubtitle = null)
        {
            _dateTitle = dateTitle;
            _dateSubtitle = dateSubtitle;
            return this;
        }
        /// <summary>
        /// Configures the icon for the roadmap component.
        /// </summary>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder WithIcon(IconStruct icon)
        {
            _icon = icon;
            return this;
        }
        /// <summary>
        /// Configures the variant for the roadmap component.
        /// </summary>
        /// <param name="variant">The variant value.</param>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }
        /// <summary>
        /// Configures the state for the roadmap component.
        /// </summary>
        /// <param name="state">The state value.</param>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder SetState(RoadmapState state)
        {
            _state = state;
            return this;
        }
        /// <summary>
        /// Configures as past behavior for the roadmap component.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder AsPast()
        {
            return SetState(RoadmapState.Past);
        }
        /// <summary>
        /// Configures as current behavior for the roadmap component.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder AsCurrent()
        {
            return SetState(RoadmapState.Current);
        }
        /// <summary>
        /// Configures as future behavior for the roadmap component.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder AsFuture()
        {
            return SetState(RoadmapState.Future);
        }
        /// <summary>
        /// Configures as blocked behavior for the roadmap component.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder AsBlocked()
        {
            return SetState(RoadmapState.Blocked);
        }
        /// <summary>
        /// Configures as cancelled behavior for the roadmap component.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBlockBuilder AsCancelled()
        {
            return SetState(RoadmapState.Cancelled);
        }
        /// <inheritdoc />
        protected override RoadmapBlockBuilder CreateInstance()
        {
            return new RoadmapBlockBuilder(_textWriter, _htmlHelper);
        }
        /// <inheritdoc />
        protected override void InternalClone(RoadmapBlockBuilder source)
        {
            base.InternalClone(source);

            _captureWriter = null;
            _originalWriter = null;

            _disposed = false;
            _started = false;

            _title = source._title;
            _subtitle = source._subtitle;
            _dateTitle = source._dateTitle;
            _dateSubtitle = source._dateSubtitle;
            _icon = source._icon;
            _variant = source._variant;
            _state = source._state;
        }
        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            throw new InvalidOperationException("RoadmapBlockBuilder does not render directly. Use Begin()/Dispose() inside RoadmapBuilder.");
        }
        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            throw new InvalidOperationException("RoadmapBlockBuilder does not render directly. Use Begin()/Dispose() inside RoadmapBuilder.");
        }
        /// <summary>
        /// Completes the active roadmap rendering or capture scope.
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

            RoadmapBuilder? roadmap = RoadmapBuilder.GetCurrent(_htmlHelper);
            if (roadmap == null)
            {
                throw new InvalidOperationException("No active RoadmapBuilder context found while closing RoadmapBlockBuilder.");
            }

            roadmap.RegisterItem(new RoadmapDefinition
            {
                Id = GetId(),
                Title = _title,
                Subtitle = _subtitle,
                DateTitle = _dateTitle,
                DateSubtitle = _dateSubtitle,
                Icon = _icon,
                Variant = _variant,
                State = _state,
                Content = new HtmlString(_captureWriter?.ToString() ?? string.Empty)
            });
        }
    }
}
