#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj BlockTitleExtensions.cs create at 2026/05/19
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="BlockTitleBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class BlockTitleExtensions
    {
        /// <summary>
        /// Creates a <see cref="BlockTitleBuilder"/> — a structured block composed of a title and
        /// one or more subtitle lines of varying sizes.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="BlockTitleBuilder"/> ready for configuration.</returns>
        [Documented]
        public static BlockTitleBuilder BlockTitleBuilder(this IHtmlHelper html)
            => new(html.ViewContext.Writer, html);
    }
}
