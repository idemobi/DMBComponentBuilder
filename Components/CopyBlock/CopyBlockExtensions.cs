#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj CopyBlockExtensions.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBBootstrapBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    public static class CopyBlockExtensions
    {
        public static CopyBlockBuilder CopyBlock(
            this IHtmlHelper html,
            string? content = null,
            BootstrapFullKindOfStyle style = BootstrapFullKindOfStyle.OutlinePrimary
        )
        {
            return new CopyBlockBuilder(html.ViewContext.Writer, html)
                .SetContent(content)
                .SetStyle(style);
        }
    }
}
