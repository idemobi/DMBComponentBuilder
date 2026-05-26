#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj ShieldExtensions.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper methods for <see cref="ShieldBuilder"/>.
    /// </summary>
    public static class ShieldExtensions
    {
        /// <summary>
        /// Creates a new empty <see cref="ShieldBuilder"/>.
        /// </summary>
        /// <param name="html">The current HTML helper.</param>
        /// <returns>A shield builder instance.</returns>
        public static ShieldBuilder ShieldBuilder(this IHtmlHelper html)
        {
            return new ShieldBuilder(html.ViewContext.Writer, html);
        }

        /// <summary>
        /// Creates a configured two-part status shield.
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
            string valueColor = "darkgrey")
        {
            return html.ShieldBuilder()
                .SetLabel(label)
                .SetValue(value)
                .SetColors(labelColor, valueColor);
        }
    }
}
