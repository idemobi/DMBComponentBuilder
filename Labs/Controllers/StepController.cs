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
    ///     Provides live examples for the step indicator component.
    /// </summary>
    public class StepController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the step indicator example page.
        /// </summary>
        /// <returns>The step indicator example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}