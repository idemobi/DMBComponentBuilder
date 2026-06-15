#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DMBComponentBuilderLabs.Controllers
{
    /// <summary>
    ///     Provides live examples for the Chart.js component.
    /// </summary>
    public class ChartJsController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the Chart.js example page.
        /// </summary>
        /// <returns>The Chart.js example view.</returns>
        public IActionResult Index()
        {
            SetTitle("ChartJs - Live Examples");
            SetDescription("Chart.js canvas rendering examples for ComponentBuilder");
            SetKeywords("ChartJs", "Chart.js", "ChartJSCore", "DMBComponentBuilder", "Statistics");
            return View();
        }

        #endregion
    }
}
