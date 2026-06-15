#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using ChartJSCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Provides Razor helper methods for rendering Chart.js components.
    /// </summary>
    public static class ChartJsExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a new <see cref="ChartJsBuilder" /> for rendering a Chart.js canvas.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="chart">The ChartJSCore chart definition to render.</param>
        /// <returns>A configured Chart.js builder.</returns>
        public static ChartJsBuilder ChartJs(this IHtmlHelper html, Chart? chart = null)
        {
            return new ChartJsBuilder(html.ViewContext.Writer, html)
                .SetChart(chart);
        }

        #endregion
    }
}
