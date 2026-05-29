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
    ///     Provides Razor helper methods for <see cref="ShieldBuilder" />.
    /// </summary>
    public static class ShieldExtensions
    {
        #region Static methods

        /// <summary>
        ///     Creates a configured two-part status shield.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <param name="label">The label rendered on the left side.</param>
        /// <param name="value">The value rendered on the right side.</param>
        /// <param name="labelColor">The label background color.</param>
        /// <param name="valueColor">The value background color.</param>
        /// <returns>A configured shield builder.</returns>
        public static ShieldBuilder Shield(
            this IHtmlHelper html,
            string label,
            string value,
            string labelColor = "dimgrey",
            string valueColor = "darkgrey"
        )
        {
            return html.ShieldBuilder()
                .SetLabel(label)
                .SetValue(value)
                .SetColors(labelColor, valueColor);
        }

        /// <summary>
        ///     Creates a new empty <see cref="ShieldBuilder" />.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <returns>A shield builder instance.</returns>
        public static ShieldBuilder ShieldBuilder(this IHtmlHelper html)
        {
            return new ShieldBuilder(html.ViewContext.Writer, html);
        }

        #endregion
    }
}