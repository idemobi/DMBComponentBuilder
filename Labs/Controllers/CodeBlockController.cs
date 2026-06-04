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
    ///     Provides live examples for the code block component.
    /// </summary>
    public class CodeBlockController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the code block example page.
        /// </summary>
        /// <returns>The code block example view.</returns>
        public IActionResult Index()
        {
            SetTitle("CodeBlock - Live Examples");
            SetDescription("CodeBlock syntax highlighting examples");
            SetKeywords("CodeBlock", "DMBComponentBuilder", "Syntax Highlighting", "PrismJS");
            return View();
        }

        #endregion
    }
}