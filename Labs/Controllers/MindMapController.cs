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
    ///     Provides live examples for the GDF mind map component.
    /// </summary>
    public class MindMapController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the GDF mind map example page.
        /// </summary>
        /// <returns>The GDF mind map example view.</returns>
        public IActionResult Index()
        {
            SetTitle("Mind Map - Live Examples");
            SetDescription("Carte heuristique examples for ComponentBuilder");
            SetKeywords("MindMap", "Carte heuristique", "GDFMindMapBuilder", "DMBComponentBuilder");
            return View();
        }

        #endregion
    }
}