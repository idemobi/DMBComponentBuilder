#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj FaqItem.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using DMBPageBuilder;
using Microsoft.AspNetCore.Html;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Represents a single frequently asked question entry.
    /// </summary>
    public sealed class FaqItem
    {
        public string Question { get; set; } = string.Empty;

        public string Answer { get; set; } = string.Empty;

        public bool IsVisible { get; set; } = true;

        public List<IconStruct> StartIcons { get; } = new();

        public List<IconStruct> EndIcons { get; } = new();

        public IHtmlContent? ActionContent { get; set; }

        public IHtmlContent? HiddenNotice { get; set; }
    }
}
