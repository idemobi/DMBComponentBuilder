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
    ///     Provides the FAQ page.
    /// </summary>
    public class FaqController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the FAQ page.
        /// </summary>
        /// <returns>The FAQ view.</returns>
        public IActionResult Index()
        {
            SetTitle("ComponentBuilder - FAQ");
            SetDescription("Frequently asked questions component examples");
            SetKeywords("ComponentBuilder", "FAQ", "FaqBuilder", "DMBComponentBuilder");
            return View();
        }

        #endregion
    }
}