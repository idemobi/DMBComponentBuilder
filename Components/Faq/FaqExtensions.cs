#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj FaqExtensions.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper methods for <see cref="FaqBuilder"/>.
    /// </summary>
    public static class FaqExtensions
    {
        public static FaqBuilder FaqBuilder(this IHtmlHelper html)
        {
            return new FaqBuilder(html.ViewContext.Writer, html);
        }
    }
}
