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
    ///     Provides live examples for the checkout progress component.
    /// </summary>
    public class CheckoutProgressController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the checkout progress example page.
        /// </summary>
        /// <returns>The checkout progress example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}