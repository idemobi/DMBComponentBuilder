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
    ///     Provides live examples for the PersonCard component.
    /// </summary>
    public class PersonCardController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the PersonCard example page.
        /// </summary>
        /// <returns>The PersonCard example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}