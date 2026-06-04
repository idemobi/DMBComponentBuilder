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
    ///     Provides the entry-point extension method to create a <see cref="BlockTitleBuilder" />
    ///     from an <see cref="IHtmlHelper" />.
    /// </summary>
    [Documented]
    public static class BlockTitleExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="BlockTitleBuilder" /> — a structured block composed of a title and
        ///     one or more subtitle lines of varying sizes.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="BlockTitleBuilder" /> ready for configuration.</returns>
        [Documented]
        public static BlockTitleBuilder BlockTitleBuilder(this IHtmlHelper html)
            => new(html.ViewContext.Writer, html);

        #endregion
    }
}