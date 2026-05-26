#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj SeparatorExtensions.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBPageBuilder;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Provides Razor helper methods for <see cref="SeparatorBuilder"/>.
    /// </summary>
    public static class SeparatorExtensions
    {
        public static SeparatorBuilder SeparatorBuilder(this IHtmlHelper html)
        {
            return new SeparatorBuilder(html.ViewContext.Writer, html);
        }

        public static SeparatorBuilder SeparatorText(this IHtmlHelper html, string? text)
        {
            return html.SeparatorBuilder()
                .SetText(text);
        }

        public static SeparatorBuilder SeparatorIcon(this IHtmlHelper html, IconStruct icon)
        {
            return html.SeparatorBuilder()
                .SetIcon(icon);
        }

        public static SeparatorBuilder SeparatorIcon(this IHtmlHelper html, string? icon)
        {
            return html.SeparatorBuilder()
                .SetIcon(icon);
        }

        public static SeparatorBuilder PageSeparatorText(this IHtmlHelper html, string? text)
        {
            return html.SeparatorText(text)
                .UsePageSpacing();
        }
    }
}
