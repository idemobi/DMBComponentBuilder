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
    /// <summary>
    /// Provides Razor helper and fluent extension methods for copy block components.
    /// </summary>
    public static class CopyBlockExtensions
    {
        /// <summary>
        /// Creates or renders the copy block component through the copy block helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <param name="content">The content value.</param>
        /// <param name="style">The style value.</param>
        /// <returns>The configured builder instance.</returns>
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
