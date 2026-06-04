#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Defines the syntax languages supported by <see cref="CodeBlockBuilder" />.
    /// </summary>
    public enum CodeLanguage
    {
        /// <summary>
        ///     Renders plain text without syntax highlighting.
        /// </summary>
        PlainText,

        /// <summary>
        ///     Renders C# source code.
        /// </summary>
        CSharp,

        /// <summary>
        ///     Renders JSON content.
        /// </summary>
        Json,

        /// <summary>
        ///     Renders XML content.
        /// </summary>
        Xml,

        /// <summary>
        ///     Renders HTML markup.
        /// </summary>
        Html,

        /// <summary>
        ///     Renders CSS stylesheet content.
        /// </summary>
        Css,

        /// <summary>
        ///     Renders JavaScript source code.
        /// </summary>
        JavaScript,

        /// <summary>
        ///     Renders TypeScript source code.
        /// </summary>
        TypeScript,

        /// <summary>
        ///     Renders Bash shell commands.
        /// </summary>
        Bash,

        /// <summary>
        ///     Renders SQL statements.
        /// </summary>
        Sql,

        /// <summary>
        ///     Renders Markdown content.
        /// </summary>
        Markdown
    }
}