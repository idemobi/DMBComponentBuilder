#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj FestivalBuilder.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    public sealed class FestivalBuilder : IHtmlContent
    {
        private readonly IHtmlHelper _htmlHelper;
        private readonly GDFFestival _festival;

        public FestivalBuilder(IHtmlHelper htmlHelper, GDFFestival festival)
        {
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            _festival = festival ?? throw new ArgumentNullException(nameof(festival));
        }

        public IHtmlContent Render()
        {
            if (string.IsNullOrWhiteSpace(_festival.ViewName))
            {
                return HtmlString.Empty;
            }

            return _htmlHelper.PartialAsync($"Festival/{_festival.ViewName}", _festival).GetAwaiter().GetResult();
        }

        public void WriteTo(TextWriter writer, System.Text.Encodings.Web.HtmlEncoder encoder)
        {
            Render().WriteTo(writer, encoder);
        }
    }
}
