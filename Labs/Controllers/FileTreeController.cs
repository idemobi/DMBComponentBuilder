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
    ///     Provides live examples for the GDF file tree component.
    /// </summary>
    public class FileTreeController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the GDF file tree example page.
        /// </summary>
        /// <returns>The GDF file tree example view.</returns>
        public IActionResult Index()
        {
            SetTitle("File Tree - Live Examples");
            SetDescription("Finder-style folder and file tree examples for ComponentBuilder");
            SetKeywords("FileTree", "DMBComponentBuilder", "Bootstrap Icons", "Folder Tree");
            return View();
        }

        #endregion
    }
}