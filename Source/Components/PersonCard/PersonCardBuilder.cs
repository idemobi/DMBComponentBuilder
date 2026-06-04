#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.IO;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Renders a centered person card composed of a Bootstrap icon or a circular photo, a name,
    ///     a role line, and an optional description paragraph. Intended for team member listings and
    ///     contributor sections.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> team pages, contributor grids, author blocks.
    ///     </para>
    ///     <para>
    ///         <b>Example (icon mode):</b>
    ///         <code>
    /// @Html.PersonCardBuilder()
    ///     .SetName("Jean-François CONTART")
    ///     .SetRole("Lead Developer")
    ///     .SetDescription("Founder and architect of the DMB stack.")
    ///     .SetIconBootstrap("bi-person-circle")
    ///     .SetVariant(VariantStyle.Primary)
    ///     .Render()
    /// </code>
    ///     </para>
    ///     <para>
    ///         <b>Example (image mode):</b>
    ///         <code>
    /// @Html.PersonCardBuilder()
    ///     .SetName("Camille GOLLIOT")
    ///     .SetRole("Intern")
    ///     .SetImage("/images/team/cg.jpg")
    ///     .Render()
    /// </code>
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class PersonCardBuilder :
        HtmlBuilderBase<PersonCardBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin,
        ICanUsePadding,
        ICanUseWidth,
        ICanUseThemeVisibility
    {
        #region Instance fields and properties

        private string _description = string.Empty;
        private IconStruct _icon = IconStruct.Bootstrap("bi-person-circle");
        private string _imageSrc = string.Empty;
        private string _name = string.Empty;
        private string _role = string.Empty;
        private VariantStyle _variant = VariantStyle.Primary;

        #endregion

        #region Instance constructors and destructors

        /// <summary>Initializes a new <see cref="PersonCardBuilder" />.</summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public PersonCardBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("person-card-builder");
        }

        #endregion

        #region Instance methods

        /// <inheritdoc />
        protected override PersonCardBuilder CreateInstance() => new(_textWriter, _htmlHelper);

        /// <inheritdoc />
        protected override void InternalClone(PersonCardBuilder source)
        {
            base.InternalClone(source);
            _name = source._name;
            _role = source._role;
            _description = source._description;
            _icon = source._icon;
            _imageSrc = source._imageSrc;
            _variant = source._variant;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            using StringWriter sw = new();
            WriteToCore(sw, HtmlEncoder.Default);
            return new HtmlString(sw.ToString());
        }

        /// <summary>Sets the description paragraph displayed below the role.</summary>
        /// <param name="description">The description text.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PersonCardBuilder SetDescription(string description)
        {
            _description = description ?? string.Empty;
            return this;
        }

        /// <summary>Sets the icon using a Bootstrap Icons CSS class name.</summary>
        /// <param name="iconClass">The Bootstrap icon CSS class (e.g. <c>"bi-person-circle"</c>).</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PersonCardBuilder SetIconBootstrap(string iconClass)
        {
            _icon = IconStruct.Bootstrap(iconClass);
            return this;
        }

        /// <summary>
        ///     Sets a circular photo displayed instead of the icon.
        ///     When set, the icon is ignored.
        /// </summary>
        /// <param name="src">The image URL or path (e.g. <c>"/images/team/jf.jpg"</c>).</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PersonCardBuilder SetImage(string src)
        {
            _imageSrc = src ?? string.Empty;
            return this;
        }

        /// <summary>Sets the person's full name displayed as the card title.</summary>
        /// <param name="name">The full name.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PersonCardBuilder SetName(string name)
        {
            _name = name ?? string.Empty;
            return this;
        }

        /// <summary>Sets the role or job title displayed below the name.</summary>
        /// <param name="role">The role text.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PersonCardBuilder SetRole(string role)
        {
            _role = role ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the Bootstrap color variant applied to the icon.
        ///     Defaults to <see cref="VariantStyle.Primary" />.
        ///     Has no effect when an image is set via <see cref="SetImage" />.
        /// </summary>
        /// <param name="variant">The <see cref="VariantStyle" /> to apply.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public PersonCardBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            string variantClass = $"text-{_variant.GetVariantCss()}";

            writer.Write($"<div{BuildAttributes()} style=\"text-align:center\">");

            if (!string.IsNullOrWhiteSpace(_imageSrc))
            {
                writer.Write($"<div class=\"mb-3\"><img src=\"{_imageSrc}\" alt=\"\" width=\"64\" height=\"64\" class=\"rounded-circle\" /></div>");
            }
            else if (!_icon.IsEmpty)
            {
                writer.Write($"<div class=\"mb-3\"><i class=\"{_icon.Value} fs-1 {variantClass}\"></i></div>");
            }

            if (!string.IsNullOrWhiteSpace(_name))
            {
                writer.Write("<p class=\"fw-semibold mb-1\">");
                encoder.Encode(writer, _name);
                writer.Write("</p>");
            }

            if (!string.IsNullOrWhiteSpace(_role))
            {
                writer.Write("<p class=\"small text-muted mb-1\">");
                encoder.Encode(writer, _role);
                writer.Write("</p>");
            }

            if (!string.IsNullOrWhiteSpace(_description))
            {
                writer.Write("<p class=\"small text-muted mb-0\">");
                encoder.Encode(writer, _description);
                writer.Write("</p>");
            }

            writer.Write("</div>");
        }

        #endregion
    }
}
