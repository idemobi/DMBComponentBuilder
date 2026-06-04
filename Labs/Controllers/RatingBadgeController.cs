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
    ///     Provides live examples for the rating badge component.
    /// </summary>
    public class RatingBadgeController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the rating badge example page.
        /// </summary>
        /// <returns>The rating badge example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}