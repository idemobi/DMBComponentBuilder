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
    ///     Provides Razor helper methods for conversation components.
    /// </summary>
    public static class ConversationExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a <see cref="ConversationBuilder" /> for the current Razor view.
        /// </summary>
        /// <param name="html">The current Razor <see cref="IHtmlHelper" /> instance.</param>
        /// <returns>A new <see cref="ConversationBuilder" /> ready for configuration.</returns>
        public static ConversationBuilder ConversationBuilder(this IHtmlHelper html)
        {
            return new ConversationBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}
