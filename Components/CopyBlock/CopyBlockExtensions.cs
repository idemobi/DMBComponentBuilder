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
    ///     Provides Razor helper and fluent extension methods for copy block components.
    /// </summary>
    public static class CopyBlockExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates or renders the copy block component through the copy block helper.
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

        #endregion
    }
}