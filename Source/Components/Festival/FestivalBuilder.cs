#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.IO;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Builds and renders the festival visual component for Razor views.
    /// </summary>
    public sealed class FestivalBuilder : IHtmlContent
    {
        #region Instance fields and properties

        private readonly GDFFestival _festival;
        private readonly IHtmlHelper _htmlHelper;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="FestivalBuilder" /> class.
        /// </summary>
        /// <param name="htmlHelper">The html helper value.</param>
        /// <param name="festival">The festival value.</param>
        public FestivalBuilder(IHtmlHelper htmlHelper, GDFFestival festival)
        {
            _htmlHelper = htmlHelper ?? throw new ArgumentNullException(nameof(htmlHelper));
            _festival = festival ?? throw new ArgumentNullException(nameof(festival));
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Renders the configured festival component as HTML content.
        /// </summary>
        /// <returns>The generated festival value.</returns>
        public IHtmlContent Render()
        {
            if (string.IsNullOrWhiteSpace(_festival.ViewName))
            {
                return HtmlString.Empty;
            }

            return _htmlHelper.PartialAsync($"Festival/{_festival.ViewName}", _festival).GetAwaiter().GetResult();
        }

        #region From interface IHtmlContent

        /// <summary>
        ///     Writes the configured festival component to the target HTML writer.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="encoder">The HTML encoder used for writer output.</param>
        public void WriteTo(TextWriter writer, System.Text.Encodings.Web.HtmlEncoder encoder)
        {
            Render().WriteTo(writer, encoder);
        }

        #endregion

        #endregion
    }
}