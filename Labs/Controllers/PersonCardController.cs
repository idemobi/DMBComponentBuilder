using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc;

namespace DMBComponentBuilderLabs.Controllers
{
    /// <summary>
    /// Provides live examples for the PersonCard component.
    /// </summary>
    public class PersonCardController : RawBootstrapController
    {
        /// <summary>
        /// Renders the PersonCard example page.
        /// </summary>
        /// <returns>The PersonCard example view.</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
