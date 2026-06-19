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
    ///     Provides live examples for the avatar component.
    /// </summary>
    public class AvatarController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the avatar example page.
        /// </summary>
        /// <returns>The avatar example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
