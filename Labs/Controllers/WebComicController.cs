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
    ///     Provides the web comic showcase page.
    /// </summary>
    public class WebComicController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the web comic showcase page.
        /// </summary>
        /// <returns>The web comic showcase view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}