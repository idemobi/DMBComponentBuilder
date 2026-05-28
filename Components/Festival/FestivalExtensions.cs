#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj FestivalExtensions.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper and fluent extension methods for festival components.
    /// </summary>
    public static class FestivalExtensions
    {
        /// <summary>
        /// Creates or renders the festival component through the festival helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="festival">The festival value.</param>
        /// <returns>The configured builder instance.</returns>
        public static FestivalBuilder Festival(this IHtmlHelper html, GDFFestival festival)
        {
            return new FestivalBuilder(html, festival);
        }
    }
}
