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
    ///     Provides live examples for the timeline component.
    /// </summary>
    public class TimelineController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the timeline example page.
        /// </summary>
        /// <returns>The timeline example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}