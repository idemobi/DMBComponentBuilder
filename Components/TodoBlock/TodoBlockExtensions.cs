#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj TodoBlockExtensions.cs create at 2026/05/19
// ©2024-2026 idéMobi SARL FRANCE

#endregion

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides the entry-point extension method to create a <see cref="TodoBlockBuilder"/>
    /// from an <see cref="IHtmlHelper"/>.
    /// </summary>
    [Documented]
    public static class TodoBlockExtensions
    {
        /// <summary>
        /// Creates a <see cref="TodoBlockBuilder"/> — a Bootstrap card containing a structured
        /// list of items each with an icon, a title and a subtitle.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper"/> instance.</param>
        /// <returns>A new <see cref="TodoBlockBuilder"/> ready for configuration.</returns>
        [Documented]
        public static TodoBlockBuilder TodoBlockBuilder(this IHtmlHelper html)
            => new(html.ViewContext.Writer, html);
    }
}
