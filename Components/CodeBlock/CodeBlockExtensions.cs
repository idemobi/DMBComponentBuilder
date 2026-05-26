#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj CodeBlockExtensions.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper methods for rendering <see cref="CodeBlockBuilder"/> components.
    /// </summary>
    public static class CodeBlockExtensions
    {
        /// <summary>
        /// Creates a new <see cref="CodeBlockBuilder"/> for rendering syntax-highlighted code.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="code">The code to render.</param>
        /// <param name="language">The syntax language used by the highlighter.</param>
        /// <returns>A configured code block builder.</returns>
        public static CodeBlockBuilder CodeBlock(
            this IHtmlHelper html,
            string? code = null,
            CodeLanguage language = CodeLanguage.PlainText)
        {
            return new CodeBlockBuilder(html.ViewContext.Writer, html)
                .SetCode(code)
                .SetLanguage(language);
        }
    }
}
