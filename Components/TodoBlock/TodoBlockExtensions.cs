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
    ///     Provides the entry-point extension method to create a <see cref="TodoBlockBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class TodoBlockExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="TodoBlockBuilder" /> — a Bootstrap card containing a structured
        ///     list of items each with an icon, a title and a subtitle.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="TodoBlockBuilder" /> ready for configuration.</returns>
        [Documented]
        public static TodoBlockBuilder TodoBlockBuilder(this IHtmlHelper html)
            => new(html.ViewContext.Writer, html);

        #endregion
    }
}