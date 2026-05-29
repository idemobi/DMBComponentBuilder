#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Provides Razor helper methods for rendering <see cref="CodeBlockBuilder" /> components.
    /// </summary>
    public static class CodeBlockExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a new <see cref="CodeBlockBuilder" /> for rendering syntax-highlighted code.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="code">The code to render.</param>
        /// <param name="language">The syntax language used by the highlighter.</param>
        /// <returns>A configured code block builder.</returns>
        public static CodeBlockBuilder CodeBlock(
            this IHtmlHelper html,
            string? code = null,
            CodeLanguage language = CodeLanguage.PlainText
        )
        {
            return new CodeBlockBuilder(html.ViewContext.Writer, html)
                .SetCode(code)
                .SetLanguage(language);
        }

        #endregion
    }
}