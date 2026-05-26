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
    public static class FestivalExtensions
    {
        public static FestivalBuilder Festival(this IHtmlHelper html, GDFFestival festival)
        {
            return new FestivalBuilder(html, festival);
        }
    }
}
