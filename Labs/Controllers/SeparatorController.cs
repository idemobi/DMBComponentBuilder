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
    ///     Provides live examples for the separator component.
    /// </summary>
    public class SeparatorController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the separator example page.
        /// </summary>
        /// <returns>The separator example view.</returns>
        public IActionResult Index()
        {
            SetTitle("ComponentBuilder - Separator");
            SetDescription("Separator component examples");
            SetKeywords("ComponentBuilder", "Separator", "SeparatorBuilder", "DMBComponentBuilder");
            return View();
        }

        #endregion
    }
}