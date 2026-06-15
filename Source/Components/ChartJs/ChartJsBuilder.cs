#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Net;
using System.Text.Encodings.Web;
using ChartJSCore.Models;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Renders a Chart.js canvas backed by a <see cref="ChartJSCore.Models.Chart" /> definition.
    /// </summary>
    /// <remarks>
    ///     The component registers the Chart.js runtime and emits deterministic canvas markup followed by
    ///     the ChartJSCore-generated initialization script.
    /// </remarks>
    public sealed class ChartJsBuilder :
        HtmlBuilderBase<ChartJsBuilder>,
        ICanUseCustomClasses
    {
        #region Constants

        private const string ChartJsCdnPath = "https://cdnjs.cloudflare.com/ajax/libs/Chart.js/4.4.1/chart.umd.js";

        #endregion

        #region Instance fields and properties

        private Chart? _chart;
        private string? _canvasId;
        private string _height = "100px";
        private string _width = "100%";

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ChartJsBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer used by the current rendering context.</param>
        /// <param name="html">The HTML helper associated with the current Razor view.</param>
        public ChartJsBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClass("dmb-chart-js");
            SetData("chart-js", "true");
        }

        #endregion

        #region Instance methods

        /// <inheritdoc />
        protected override ChartJsBuilder CreateInstance()
        {
            return new ChartJsBuilder(_textWriter, _htmlHelper);
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetScriptFile(ChartJsCdnPath);
        }

        private string GetCanvasId()
        {
            if (string.IsNullOrWhiteSpace(_canvasId) == false)
            {
                return _canvasId;
            }

            _canvasId = $"dmb-chart-js-{Guid.NewGuid():N}";
            return _canvasId;
        }

        /// <inheritdoc />
        protected override void InternalClone(ChartJsBuilder source)
        {
            base.InternalClone(source);
            _chart = source._chart;
            _canvasId = source._canvasId;
            _height = source._height;
            _width = source._width;
        }

        /// <summary>
        ///     Renders the Chart.js component to an HTML content instance.
        /// </summary>
        /// <returns>The generated HTML content.</returns>
        public override IHtmlContent Render()
        {
            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        /// <summary>
        ///     Sets the chart definition rendered inside the canvas.
        /// </summary>
        /// <param name="chart">The ChartJSCore chart definition.</param>
        /// <returns>The current builder instance.</returns>
        public ChartJsBuilder SetChart(Chart? chart)
        {
            _chart = chart;
            return this;
        }

        /// <summary>
        ///     Sets the HTML id used by the canvas element.
        /// </summary>
        /// <param name="canvasId">The canvas id. When null or empty, a unique id is generated.</param>
        /// <returns>The current builder instance.</returns>
        public ChartJsBuilder SetCanvasId(string? canvasId)
        {
            _canvasId = string.IsNullOrWhiteSpace(canvasId) ? null : canvasId;
            return this;
        }

        /// <summary>
        ///     Sets the canvas height attribute value.
        /// </summary>
        /// <param name="height">The canvas height value.</param>
        /// <returns>The current builder instance.</returns>
        public ChartJsBuilder SetHeight(string? height)
        {
            _height = string.IsNullOrWhiteSpace(height) ? "100px" : height;
            return this;
        }

        /// <summary>
        ///     Sets the canvas width attribute value.
        /// </summary>
        /// <param name="width">The canvas width value.</param>
        /// <returns>The current builder instance.</returns>
        public ChartJsBuilder SetWidth(string? width)
        {
            _width = string.IsNullOrWhiteSpace(width) ? "100%" : width;
            return this;
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            string canvasId = GetCanvasId();

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write($"<canvas id=\"{WebUtility.HtmlEncode(canvasId)}\" width=\"{WebUtility.HtmlEncode(_width)}\" height=\"{WebUtility.HtmlEncode(_height)}\"></canvas>");
            if (_chart != null)
            {
                writer.Write("<script type=\"text/javascript\">");
                writer.Write("(function dmbChartJsInit(){");
                writer.Write("if(typeof window.Chart==='undefined'){window.setTimeout(dmbChartJsInit,50);return;}");
                writer.Write("const Chart=window.Chart;");
                writer.Write(_chart.CreateChartCode(canvasId));
                writer.Write("})();");
                writer.Write("</script>");
            }
            writer.Write($"</{GetTag()}>");
        }

        #endregion
    }
}
