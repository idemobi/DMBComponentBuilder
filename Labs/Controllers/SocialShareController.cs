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
    ///     Provides live examples for the social share component.
    /// </summary>
    public class SocialShareController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the social share example page.
        /// </summary>
        /// <returns>The social share example view.</returns>
        public IActionResult Index()
        {
            SetTitle("ComponentBuilder - SocialShare");
            SetDescription("SocialShare component examples");
            SetKeywords("ComponentBuilder", "SocialShare", "SocialShareDefinition", "DMBComponentBuilder");

            return View();
        }

        #endregion
    }
}