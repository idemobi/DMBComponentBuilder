#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBComponentBuilderLabs.Navigation;

/// <summary>
///     Provides reusable navigation fragments for DMBComponentBuilder labs hosts.
/// </summary>
/// <remarks>
///     The agent only builds DMBComponentBuilder-specific menu and sidebar fragments. Host websites remain
///     responsible for assembling these fragments into their own navbar providers, sidebar filters, and
///     global navigation structures.
/// </remarks>
public static class DMBComponentBuilderLabsNavigationAgent
{
    #region Static fields and properties

    private static readonly HashSet<string> ModuleControllers = new(StringComparer.OrdinalIgnoreCase)
    {
        "ComponentBuilder",
        "Faq",
        "RatingBadge",
        "FileTree",
        "Roadmap",
        "Timeline",
        "CodeBlock",
        "CheckoutProgress",
        "Step",
        "Shield",
        "WebComic",
        "MindMap",
        "Festival",
        "SocialShare",
        "Separator",
        "TodoBlock",
        "PersonCard"
    };

    #endregion

    #region Static methods

    /// <summary>
    ///     Creates an action item for a DMBComponentBuilder labs page.
    /// </summary>
    /// <param name="controller">The MVC controller name.</param>
    /// <param name="action">The MVC action name.</param>
    /// <param name="title">The action title shown in navigation UI.</param>
    /// <param name="icon">The Bootstrap Icons CSS class used by the action.</param>
    /// <param name="currentController">The current MVC controller name used to mark the action active.</param>
    /// <param name="currentAction">The current MVC action name used to mark the action active.</param>
    /// <returns>The configured <see cref="AspRouteActionItem" />.</returns>
    public static AspRouteActionItem CreateAction(
        string controller,
        string action,
        string title,
        string icon,
        string? currentController = null,
        string? currentAction = null
    )
    {
        bool active =
            string.Equals(currentController, controller, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(currentAction, action, StringComparison.OrdinalIgnoreCase);

        return ActionItemFactory.AspRoute(controller, action)
            .SetTitle(title)
            .SetIcon(IconStruct.Bootstrap(icon))
            .SetActive(active);
    }

    /// <summary>
    ///     Creates the DMBComponentBuilder navbar menu group.
    /// </summary>
    /// <returns>The configured <see cref="GroupActionItem" /> containing DMBComponentBuilder labs page links.</returns>
    public static GroupActionItem CreateMenuGroup()
    {
        return ActionItemFactory.Group("DMBComponentBuilder", IconStruct.Bootstrap("bi-ui-checks-grid"))
            .AddItems(
                ActionItemFactory.Group("General", IconStruct.Bootstrap("bi-info-circle"))
                    .AddItems(
                        CreateAction("ComponentBuilder", "Introduction", "Introduction", "bi-info-circle"),
                        CreateAction("ComponentBuilder", "GettingStarted", "Getting Started", "bi-play-circle"),
                        CreateAction("ComponentBuilder", "Architecture", "Architecture", "bi-diagram-3"),
                        CreateAction("ComponentBuilder", "RenderingPipeline", "Rendering Pipeline", "bi-bezier2")
                    ),
                ActionItemFactory.Group("Components", IconStruct.Bootstrap("bi-ui-checks-grid"))
                    .AddItems(
                        CreateAction("Faq", "Index", "FaqBuilder", "bi-question-square"),
                        CreateAction("RatingBadge", "Index", "RatingBadge", "bi-star-half"),
                        CreateAction("FileTree", "Index", "FileTree", "bi-folder2-open"),
                        CreateAction("Roadmap", "Index", "Roadmap", "bi-map"),
                        CreateAction("Timeline", "Index", "Timeline", "bi-clock-history"),
                        CreateAction("CodeBlock", "Index", "CodeBlock", "bi-code-square"),
                        CreateAction("CheckoutProgress", "Index", "Checkout progress", "bi-cart-check"),
                        CreateAction("Step", "Index", "Step by Step", "bi-list-ol"),
                        CreateAction("Shield", "Index", "ShieldBuilder", "bi-shield-check"),
                        CreateAction("WebComic", "Index", "WebComic viewer", "bi-images"),
                        CreateAction("MindMap", "Index", "MindMap", "bi-diagram-3"),
                        CreateAction("Festival", "Index", "Festival", "bi-calendar-event"),
                        CreateAction("SocialShare", "Index", "SocialShare", "bi-share"),
                        CreateAction("Separator", "Index", "SeparatorBuilder", "bi-hr"),
                        CreateAction("TodoBlock", "Index", "TodoBlock", "bi-check2-square"),
                        CreateAction("PersonCard", "Index", "PersonCard", "bi-person-circle")
                    )
            );
    }

    /// <summary>
    ///     Creates the DMBComponentBuilder sidebar component.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <param name="sidebarId">The HTML identifier applied to the sidebar component.</param>
    /// <param name="localStorageKey">The browser local-storage key used for sidebar state.</param>
    /// <returns>The configured <see cref="SideBarComponent" />.</returns>
    public static SideBarComponent CreateSidebar(
        string? currentController,
        string? currentAction,
        string sidebarId = "component_builder_sidebar",
        string localStorageKey = "dmbcomponentbuilder.labs.sidebar"
    )
    {
        SideBarComponent sidebar = new SideBarComponent()
            .WithId(sidebarId)
            .WithLocalStorageKey(localStorageKey)
            .WithAutoExpandActivePath()
            .WithRememberExpandedState();

        sidebar.AddSection(CreateSidebarSection(currentController, currentAction));

        return sidebar;
    }

    /// <summary>
    ///     Creates the DMBComponentBuilder sidebar section.
    /// </summary>
    /// <param name="currentController">The current MVC controller name used to mark the active item.</param>
    /// <param name="currentAction">The current MVC action name used to mark the active item.</param>
    /// <returns>The configured <see cref="SideBarSectionComponent" />.</returns>
    public static SideBarSectionComponent CreateSidebarSection(string? currentController, string? currentAction)
    {
        return new SideBarSectionComponent("DMBComponentBuilder")
            .Add(
                ActionItemFactory.Group("General", IconStruct.Bootstrap("bi-info-circle"))
                    .AddItems(
                        CreateAction("ComponentBuilder", "Introduction", "Introduction", "bi-info-circle", currentController, currentAction),
                        CreateAction("ComponentBuilder", "GettingStarted", "Getting Started", "bi-play-circle", currentController, currentAction),
                        CreateAction("ComponentBuilder", "Architecture", "Architecture", "bi-diagram-3", currentController, currentAction),
                        CreateAction("ComponentBuilder", "RenderingPipeline", "Rendering Pipeline", "bi-bezier2", currentController, currentAction)
                    ),
                ActionItemFactory.Group("Components", IconStruct.Bootstrap("bi-ui-checks-grid"))
                    .AddItems(
                        CreateAction("Faq", "Index", "FaqBuilder", "bi-question-square", currentController, currentAction),
                        CreateAction("RatingBadge", "Index", "RatingBadge", "bi-star-half", currentController, currentAction),
                        CreateAction("FileTree", "Index", "FileTree", "bi-folder2-open", currentController, currentAction),
                        CreateAction("Roadmap", "Index", "Roadmap", "bi-map", currentController, currentAction),
                        CreateAction("Timeline", "Index", "Timeline", "bi-clock-history", currentController, currentAction),
                        CreateAction("CodeBlock", "Index", "CodeBlock", "bi-code-square", currentController, currentAction),
                        CreateAction("CheckoutProgress", "Index", "Checkout progress", "bi-cart-check", currentController, currentAction),
                        CreateAction("Step", "Index", "Step by Step", "bi-list-ol", currentController, currentAction),
                        CreateAction("Shield", "Index", "ShieldBuilder", "bi-shield-check", currentController, currentAction),
                        CreateAction("WebComic", "Index", "WebComic viewer", "bi-images", currentController, currentAction),
                        CreateAction("MindMap", "Index", "MindMap", "bi-diagram-3", currentController, currentAction),
                        CreateAction("Festival", "Index", "Festival", "bi-calendar-event", currentController, currentAction),
                        CreateAction("SocialShare", "Index", "SocialShare", "bi-share", currentController, currentAction),
                        CreateAction("Separator", "Index", "SeparatorBuilder", "bi-hr", currentController, currentAction),
                        CreateAction("TodoBlock", "Index", "TodoBlock", "bi-check2-square", currentController, currentAction),
                        CreateAction("PersonCard", "Index", "PersonCard", "bi-person-circle", currentController, currentAction)
                    )
            );
    }

    /// <summary>
    ///     Determines whether a controller belongs to the DMBComponentBuilder labs module.
    /// </summary>
    /// <param name="controllerName">The MVC controller name to inspect.</param>
    /// <returns><see langword="true" /> when the controller is part of this labs module; otherwise, <see langword="false" />.</returns>
    public static bool IsModuleController(string? controllerName)
    {
        return !string.IsNullOrWhiteSpace(controllerName) && ModuleControllers.Contains(controllerName);
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for a DMBComponentBuilder labs action.
    /// </summary>
    /// <param name="currentController">The MVC controller name to resolve.</param>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The icon value represented as an <see cref="IconStruct" />.</returns>
    public static IconStruct ResolveActionIcon(string? currentController, string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => IconStruct.Bootstrap("bi-play-circle"),
            "Architecture" => IconStruct.Bootstrap("bi-diagram-3"),
            "RenderingPipeline" => IconStruct.Bootstrap("bi-bezier2"),
            _ => ResolveIndexIcon(currentController)
        };
    }

    /// <summary>
    ///     Resolves the display title for a DMBComponentBuilder labs action.
    /// </summary>
    /// <param name="currentController">The MVC controller name to resolve.</param>
    /// <param name="actionName">The MVC action name to resolve.</param>
    /// <returns>The display title for the action.</returns>
    public static string ResolveActionTitle(string? currentController, string? actionName)
    {
        return actionName switch
        {
            "GettingStarted" => "Getting Started",
            "Architecture" => "Architecture",
            "RenderingPipeline" => "Rendering Pipeline",
            "Index" => ResolveIndexTitle(currentController),
            _ => string.Equals(currentController, "ComponentBuilder", StringComparison.OrdinalIgnoreCase)
                ? "Introduction"
                : ResolveIndexTitle(currentController)
        };
    }

    private static IconStruct ResolveIndexIcon(string? currentController)
    {
        return currentController switch
        {
            "Faq" => IconStruct.Bootstrap("bi-question-square"),
            "RatingBadge" => IconStruct.Bootstrap("bi-star-half"),
            "FileTree" => IconStruct.Bootstrap("bi-folder2-open"),
            "Roadmap" => IconStruct.Bootstrap("bi-map"),
            "Timeline" => IconStruct.Bootstrap("bi-clock-history"),
            "CodeBlock" => IconStruct.Bootstrap("bi-code-square"),
            "CheckoutProgress" => IconStruct.Bootstrap("bi-cart-check"),
            "Step" => IconStruct.Bootstrap("bi-list-ol"),
            "Shield" => IconStruct.Bootstrap("bi-shield-check"),
            "WebComic" => IconStruct.Bootstrap("bi-images"),
            "MindMap" => IconStruct.Bootstrap("bi-diagram-3"),
            "Festival" => IconStruct.Bootstrap("bi-calendar-event"),
            "SocialShare" => IconStruct.Bootstrap("bi-share"),
            "Separator" => IconStruct.Bootstrap("bi-hr"),
            "TodoBlock" => IconStruct.Bootstrap("bi-check2-square"),
            "PersonCard" => IconStruct.Bootstrap("bi-person-circle"),
            _ => IconStruct.Bootstrap("bi-info-circle")
        };
    }

    private static string ResolveIndexTitle(string? currentController)
    {
        return currentController switch
        {
            "Faq" => "FaqBuilder",
            "CheckoutProgress" => "Checkout progress",
            "Step" => "Step by Step",
            "Shield" => "ShieldBuilder",
            "WebComic" => "WebComic viewer",
            "Separator" => "SeparatorBuilder",
            null or "" => "Introduction",
            _ => currentController
        };
    }

    /// <summary>
    ///     Resolves the module root controller for a DMBComponentBuilder labs controller.
    /// </summary>
    /// <param name="currentController">The MVC controller name to resolve.</param>
    /// <returns>The module root controller name.</returns>
    public static string ResolveModuleController(string? currentController)
    {
        return "ComponentBuilder";
    }

    /// <summary>
    ///     Resolves the default action for the DMBComponentBuilder labs module root controller.
    /// </summary>
    /// <param name="currentController">The MVC controller name to resolve.</param>
    /// <returns>The module default action name.</returns>
    public static string ResolveModuleDefaultAction(string? currentController)
    {
        return "Introduction";
    }

    /// <summary>
    ///     Resolves the Bootstrap icon for the DMBComponentBuilder labs module.
    /// </summary>
    /// <param name="currentController">The MVC controller name to resolve.</param>
    /// <returns>The module icon value represented as an <see cref="IconStruct" />.</returns>
    public static IconStruct ResolveModuleIcon(string? currentController)
    {
        return IconStruct.Bootstrap("bi-ui-checks-grid");
    }

    /// <summary>
    ///     Resolves the display title for the DMBComponentBuilder labs module.
    /// </summary>
    /// <param name="currentController">The MVC controller name to resolve.</param>
    /// <returns>The module display title.</returns>
    public static string ResolveModuleTitle(string? currentController)
    {
        return "DMBComponentBuilder";
    }

    #endregion
}