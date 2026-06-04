#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBComponentBuilderLabs.Navigation;
using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Filters;

#endregion

namespace DMBComponentBuilderWebsite;

internal sealed class DMBComponentBuilderWebsiteSidebarActionFilter : IActionFilter
{
    #region Instance methods

    #region From interface IActionFilter

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Controller is not RawBootstrapController controller)
        {
            return;
        }

        string? currentController = context.RouteData.Values["controller"]?.ToString();
        string? currentAction = context.RouteData.Values["action"]?.ToString();

        if (!DMBComponentBuilderLabsNavigationAgent.IsModuleController(currentController))
        {
            return;
        }

        string actionName = string.IsNullOrWhiteSpace(currentAction) ? "Index" : currentAction;

        controller.SetSidebar(DMBComponentBuilderLabsNavigationAgent.CreateSidebar(currentController, currentAction));
        controller.AddBreadcrumb(
            ActionItemFactory.Url("Home", "/", IconStruct.Bootstrap("bi-house")),
            ActionItemFactory.AspRoute("ComponentBuilder", "Introduction")
                .SetTitle("DMBComponentBuilder")
                .SetIcon(IconStruct.Bootstrap("bi-ui-checks-grid")),
            ActionItemFactory.AspRoute(currentController ?? "ComponentBuilder", actionName)
                .SetTitle(DMBComponentBuilderLabsNavigationAgent.ResolveActionTitle(currentController, actionName))
                .SetIcon(DMBComponentBuilderLabsNavigationAgent.ResolveActionIcon(currentController, actionName))
        );
    }

    #endregion

    #endregion
}
