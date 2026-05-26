#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj GDFMindMapExtensions.cs create at 2026/05/21
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper methods for rendering <see cref="GDFMindMapBuilder"/> components.
    /// </summary>
    public static class GDFMindMapExtensions
    {
        /// <summary>
        /// Creates a new empty <see cref="GDFMindMapBuilder"/>.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <returns>A configured mind map builder.</returns>
        public static GDFMindMapBuilder GDFMindMap(this IHtmlHelper html)
        {
            return new GDFMindMapBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a new <see cref="GDFMindMapBuilder"/> from the provided root topic.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="root">The root topic and its child branches.</param>
        /// <returns>A configured mind map builder.</returns>
        public static GDFMindMapBuilder GDFMindMap(this IHtmlHelper html, GDFMindMapNode root)
        {
            return new GDFMindMapBuilder(html.ViewContext.Writer, html)
                .SetRoot(root);
        }

        /// <summary>
        /// Creates a new <see cref="GDFMindMapBuilder"/> from the provided root topic.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="root">The root topic and its child branches.</param>
        /// <returns>A configured mind map builder.</returns>
        public static GDFMindMapBuilder MindMap(this IHtmlHelper html, GDFMindMapNode root)
        {
            return html.GDFMindMap(root);
        }

        /// <summary>
        /// Creates a new empty <see cref="GDFMindMapBuilder"/>.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <returns>A configured mind map builder.</returns>
        public static GDFMindMapBuilder MindMap(this IHtmlHelper html)
        {
            return html.GDFMindMap();
        }
    }
}
