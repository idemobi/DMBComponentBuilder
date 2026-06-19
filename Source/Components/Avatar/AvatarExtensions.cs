#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Provides Razor helper methods for avatar components.
    /// </summary>
    [Documented]
    public static class AvatarExtensions
    {
        /// <summary>
        ///     Creates an <see cref="AvatarBuilder" /> for the current Razor view.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="AvatarBuilder" /> ready for configuration.</returns>
        [Documented]
        public static AvatarBuilder AvatarBuilder(this IHtmlHelper html)
        {
            return new AvatarBuilder(html.ViewContext.Writer, html);
        }
    }
}
