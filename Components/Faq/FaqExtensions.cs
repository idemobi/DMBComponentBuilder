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
    ///     Provides Razor helper methods for <see cref="FaqBuilder" />.
    /// </summary>
    public static class FaqExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates or renders the faq component through the faq builder helper.
        /// </summary>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        /// <returns>The configured builder instance.</returns>
        public static FaqBuilder FaqBuilder(this IHtmlHelper html)
        {
            return new FaqBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}