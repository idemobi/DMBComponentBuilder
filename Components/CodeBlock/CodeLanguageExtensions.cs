#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj CodeLanguageExtensions.cs create at 2026/05/06
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides rendering mapping helpers for <see cref="CodeLanguage"/>.
    /// </summary>
    public static class CodeLanguageExtensions
    {
        /// <summary>
        /// Gets the code block language identifier for the specified language.
        /// </summary>
        /// <param name="language">The language to map.</param>
        /// <returns>The code block language identifier.</returns>
        public static string GetCodeBlockLanguage(this CodeLanguage language)
        {
            return language switch
            {
                CodeLanguage.CSharp => "csharp",
                CodeLanguage.Json => "json",
                CodeLanguage.Xml => "xml",
                CodeLanguage.Html => "markup",
                CodeLanguage.Css => "css",
                CodeLanguage.JavaScript => "javascript",
                CodeLanguage.TypeScript => "typescript",
                CodeLanguage.Bash => "bash",
                CodeLanguage.Sql => "sql",
                CodeLanguage.Markdown => "markdown",
                _ => "none"
            };
        }

        /// <summary>
        /// Gets the display label for the specified language.
        /// </summary>
        /// <param name="language">The language to display.</param>
        /// <returns>The short language label.</returns>
        public static string GetDisplayName(this CodeLanguage language)
        {
            return language switch
            {
                CodeLanguage.PlainText => "Text",
                CodeLanguage.CSharp => "C#",
                CodeLanguage.Json => "JSON",
                CodeLanguage.Xml => "XML",
                CodeLanguage.Html => "HTML",
                CodeLanguage.Css => "CSS",
                CodeLanguage.JavaScript => "JavaScript",
                CodeLanguage.TypeScript => "TypeScript",
                CodeLanguage.Bash => "Bash",
                CodeLanguage.Sql => "SQL",
                CodeLanguage.Markdown => "Markdown",
                _ => language.ToString()
            };
        }

        /// <summary>
        /// Gets the Bootstrap Icons class that represents a file for the specified language.
        /// </summary>
        /// <param name="language">The language to represent.</param>
        /// <returns>The Bootstrap Icons CSS class.</returns>
        public static string GetBootstrapIconClass(this CodeLanguage language)
        {
            return language switch
            {
                CodeLanguage.CSharp => "bi-filetype-cs",
                CodeLanguage.Json => "bi-filetype-json",
                CodeLanguage.Xml => "bi-filetype-xml",
                CodeLanguage.Html => "bi-filetype-html",
                CodeLanguage.Css => "bi-filetype-css",
                CodeLanguage.JavaScript => "bi-filetype-js",
                CodeLanguage.TypeScript => "bi-filetype-tsx",
                CodeLanguage.Bash => "bi-filetype-sh",
                CodeLanguage.Sql => "bi-filetype-sql",
                CodeLanguage.Markdown => "bi-filetype-md",
                _ => "bi-file-earmark-text"
            };
        }
    }
}
