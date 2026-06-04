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
    ///     Provides live examples for the shield badge component.
    /// </summary>
    public class ShieldController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the shield badge example page.
        /// </summary>
        /// <returns>The shield badge example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}