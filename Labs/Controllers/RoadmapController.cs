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
    ///     Provides the project roadmap page.
    /// </summary>
    public class RoadmapController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the project roadmap page.
        /// </summary>
        /// <returns>The roadmap view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}