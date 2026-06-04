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
    ///     Provides documentation pages for <see cref="DMBComponentBuilder.ComponentBuilderConfiguration" />.
    /// </summary>
    public class ComponentBuilderController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the ComponentBuilder architecture page.
        /// </summary>
        /// <returns>The architecture view.</returns>
        public IActionResult Architecture()
        {
            SetTitle("ComponentBuilder - Architecture");
            SetDescription("ComponentBuilder architecture");
            SetKeywords("ComponentBuilder", "DMBComponentBuilder", "Architecture", "ASP.NET Core");
            return View();
        }

        /// <summary>
        ///     Renders the ComponentBuilder getting started page.
        /// </summary>
        /// <returns>The getting started view.</returns>
        public IActionResult GettingStarted()
        {
            SetTitle("ComponentBuilder - Getting Started");
            SetDescription("ComponentBuilder getting started guide");
            SetKeywords("ComponentBuilder", "DMBComponentBuilder", "Getting Started", "NuGet", "ASP.NET Core");
            return View();
        }

        /// <summary>
        ///     Renders the ComponentBuilder introduction page.
        /// </summary>
        /// <returns>The introduction view.</returns>
        public IActionResult Introduction()
        {
            SetTitle("ComponentBuilder - Introduction");
            SetDescription("ComponentBuilder");
            SetKeywords("ComponentBuilder", "DMBComponentBuilder", "NuGet", "ASP.NET Core");
            return View();
        }

        /// <summary>
        ///     Renders the ComponentBuilder rendering pipeline page.
        /// </summary>
        /// <returns>The rendering pipeline view.</returns>
        public IActionResult RenderingPipeline()
        {
            SetTitle("ComponentBuilder - Rendering Pipeline");
            SetDescription("ComponentBuilder rendering pipeline");
            SetKeywords("ComponentBuilder", "DMBComponentBuilder", "Rendering Pipeline", "ASP.NET Core");
            return View();
        }

        #endregion
    }
}