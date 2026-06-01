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
    ///     Provides the entry-point extension method to create a <see cref="PersonCardBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class PersonCardExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="PersonCardBuilder" /> — a centered person card composed of a
        ///     Bootstrap icon or a circular photo, a name, a role line, and an optional description.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="PersonCardBuilder" /> ready for configuration.</returns>
        [Documented]
        public static PersonCardBuilder PersonCardBuilder(this IHtmlHelper html)
            => new(html.ViewContext.Writer, html);

        #endregion
    }
}
