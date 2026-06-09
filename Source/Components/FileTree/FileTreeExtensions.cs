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
    ///     Provides Razor helper methods for rendering <see cref="FileTreeBuilder" /> components.
    /// </summary>
    public static class FileTreeExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a new empty <see cref="FileTreeBuilder" />.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <returns>A configured file tree builder.</returns>
        public static FileTreeBuilder FileTree(this IHtmlHelper html)
        {
            return new FileTreeBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        ///     Creates a new <see cref="FileTreeBuilder" /> from the provided root nodes.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="nodes">The root file and folder nodes.</param>
        /// <returns>A configured file tree builder.</returns>
        public static FileTreeBuilder FileTree(
            this IHtmlHelper html,
            IEnumerable<FileTreeNode> nodes
        )
        {
            return new FileTreeBuilder(html.ViewContext.Writer, html)
                .SetNodes(nodes);
        }

        /// <summary>
        ///     Creates a new <see cref="FileTreeBuilder" /> from the provided root nodes.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="nodes">The root file and folder nodes.</param>
        /// <returns>A configured file tree builder.</returns>
        public static FileTreeBuilder FileTree(
            this IHtmlHelper html,
            params FileTreeNode[] nodes
        )
        {
            return new FileTreeBuilder(html.ViewContext.Writer, html)
                .SetNodes(nodes);
        }

        #endregion
    }
}