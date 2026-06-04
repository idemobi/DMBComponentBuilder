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
    ///     Provides live examples for the to-do block component.
    /// </summary>
    public class TodoBlockController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the to-do block example page.
        /// </summary>
        /// <returns>The to-do block example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}