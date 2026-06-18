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
    ///     Provides live examples for the conversation component.
    /// </summary>
    public class ConversationController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the conversation example page.
        /// </summary>
        /// <returns>The conversation example view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        #endregion
    }
}
