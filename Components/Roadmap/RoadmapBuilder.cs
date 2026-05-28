using System.Net;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Builds and renders the roadmap visual component for Razor views.
    /// </summary>
    public sealed class RoadmapBuilder :
        HtmlBuilderBase<RoadmapBuilder>,
        ICanUseCustomClasses,
        ICanUseRoadmapEffects,
        IDisposable
    {
        private const string CurrentContextKey = "__DMB_CURRENT_ROADMAP_BUILDER__";
        private const string RoadmapCssPath = "/css/components/Roadmap.css";
        private const string RoadmapJsPath = "/js/components/Roadmap.js";

        private readonly List<RoadmapDefinition> _items = new();
        private readonly TextWriter _outputWriter;

        private RoadmapBuilder? _previousRoadmap;

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
        /// <summary>
        /// Initializes a new instance of the <see cref="RoadmapBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public RoadmapBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            _outputWriter = html.ViewContext.Writer;

            this.AddClass("roadmap");
            SetData("roadmap", "true");
        }
        /// <summary>
        /// Starts the roadmap rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public RoadmapBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            EnsureAssets();

            _started = true;

            if (string.IsNullOrWhiteSpace(GetId()))
            {
                SetEnsureId("roadmap");
            }

            _previousRoadmap = GetCurrent(_htmlHelper);
            SetCurrent(_htmlHelper, this);

            return this;
        }

        internal static RoadmapBuilder? GetCurrent(IHtmlHelper html)
        {
            if (html?.ViewContext?.HttpContext?.Items == null)
            {
                return null;
            }

            return html.ViewContext.HttpContext.Items.TryGetValue(CurrentContextKey, out object? value)
                ? value as RoadmapBuilder
                : null;
        }

        private static void SetCurrent(IHtmlHelper html, RoadmapBuilder? roadmap)
        {
            if (html?.ViewContext?.HttpContext?.Items == null)
            {
                return;
            }

            if (roadmap == null)
            {
                html.ViewContext.HttpContext.Items.Remove(CurrentContextKey);
            }
            else
            {
                html.ViewContext.HttpContext.Items[CurrentContextKey] = roadmap;
            }
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(RoadmapCssPath);
            page.SetScriptFile(RoadmapJsPath);
        }

        internal void RegisterItem(RoadmapDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition);

            int index = _items.Count;

            definition.Id = string.IsNullOrWhiteSpace(definition.Id)
                ? $"{GetId()}_item_{index}"
                : definition.Id;

            _items.Add(definition);
        }
        /// <inheritdoc />
        protected override RoadmapBuilder CreateInstance()
        {
            return new RoadmapBuilder(_textWriter, _htmlHelper);
        }
        /// <inheritdoc />
        protected override void InternalClone(RoadmapBuilder source)
        {
            base.InternalClone(source);

            _items.Clear();
            _items.AddRange(source._items.Select(x => new RoadmapDefinition
            {
                Id = x.Id,
                Title = x.Title,
                Subtitle = x.Subtitle,
                DateTitle = x.DateTitle,
                DateSubtitle = x.DateSubtitle,
                Icon = x.Icon,
                Variant = x.Variant,
                State = x.State,
                Content = x.Content
            }));

            _previousRoadmap = null;
            _disposed = false;
            _started = false;
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
            if (_items.Count == 0)
            {
                return;
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write("<div class=\"roadmap-track\">");

            foreach (RoadmapDefinition item in _items)
            {
                WriteItem(writer, encoder, item);
            }

            writer.Write("</div>");
            writer.Write($"</{GetTag()}>");
        }

        private void WriteItem(TextWriter writer, HtmlEncoder encoder, RoadmapDefinition item)
        {
            string variant = item.Variant.GetVariantCss();
            string state = item.State.ToString().ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(variant))
            {
                variant = "primary";
            }

            writer.Write($"<div id=\"{WebUtility.HtmlEncode(item.Id)}\" class=\"roadmap-item roadmap-state-{state}\">");

            if (!string.IsNullOrWhiteSpace(item.DateTitle) || !string.IsNullOrWhiteSpace(item.DateSubtitle))
            {
                writer.Write("<div class=\"roadmap-date\">");

                if (!string.IsNullOrWhiteSpace(item.DateTitle))
                {
                    writer.Write($"<span class=\"roadmap-date-title\">{WebUtility.HtmlEncode(item.DateTitle)}</span>");
                }

                if (!string.IsNullOrWhiteSpace(item.DateSubtitle))
                {
                    writer.Write($"<span class=\"roadmap-date-subtitle\">{WebUtility.HtmlEncode(item.DateSubtitle)}</span>");
                }

                writer.Write("</div>");
            }

            writer.Write($"<div class=\"roadmap-marker border-{WebUtility.HtmlEncode(variant)} text-{WebUtility.HtmlEncode(variant)}\">");
            
            if (!item.Icon.IsEmpty)
            {
                HtmlLayoutExtensions.IconBuilder(_htmlHelper, item.Icon, null, null)
                    .WriteTo(writer, HtmlEncoder.Default);
            }
            
            writer.Write("</div>");

            writer.Write($"<div class=\"roadmap-card roadmap-card-{WebUtility.HtmlEncode(variant)}\">");

            if (!string.IsNullOrWhiteSpace(item.Title))
            {
                writer.Write($"<div class=\"roadmap-title text-{WebUtility.HtmlEncode(variant)}\">{WebUtility.HtmlEncode(item.Title)}</div>");
            }

            if (!string.IsNullOrWhiteSpace(item.Subtitle))
            {
                writer.Write($"<div class=\"roadmap-subtitle\">{WebUtility.HtmlEncode(item.Subtitle)}</div>");
            }

            if (item.Content != null)
            {
                writer.Write("<div class=\"roadmap-description\">");
                item.Content.WriteTo(writer, encoder);
                writer.Write("</div>");
            }

            writer.Write("</div>");
            writer.Write("</div>");
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

            SetCurrent(_htmlHelper, _previousRoadmap);

            if (!_started)
            {
                return;
            }

            WriteTo(_outputWriter, HtmlEncoder.Default);
        }
    }
}
