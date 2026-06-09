#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBComponentBuilder;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DMBComponentBuilderLabs.Controllers
{
    /// <summary>
    ///     Provides the festival showcase page.
    /// </summary>
    public class FestivalController : RawBootstrapController
    {
        #region Instance methods

        /// <summary>
        ///     Renders the festival showcase page with the available festival list.
        /// </summary>
        /// <returns>The festival showcase view.</returns>
        public IActionResult Index()
        {
            SetTitle("ComponentBuilder - Festival");
            SetDescription("Festival component examples");
            SetKeywords("ComponentBuilder", "Festival", "GDFFestival", "DMBComponentBuilder");

            DateTime today = DateTime.UtcNow.Date;
            List<GDFFestival> festivals = new()
            {
                new GDFFestival
                {
                    Name = "Component release day",
                    ViewName = "ComponentRelease",
                    StartDate = today,
                    EndDate = today
                },
                new GDFFestival
                {
                    Name = "Documentation sprint",
                    ViewName = "DocumentationSprint",
                    StartDate = today.AddDays(-2),
                    EndDate = today.AddDays(3)
                },
                new GDFFestival
                {
                    Name = "Seasonal showcase",
                    ViewName = "SeasonalShowcase",
                    StartDate = new DateTime(today.Year, 12, 21),
                    EndDate = new DateTime(today.Year, 12, 31)
                }
            };

            return View(festivals);
        }

        #endregion
    }
}