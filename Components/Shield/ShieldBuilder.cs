#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj ShieldBuilder.cs create at 2026/05/12
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Renders a compact two-part status shield.
    /// </summary>
    public sealed class ShieldBuilder :
        HtmlBuilderBase<ShieldBuilder>,
        ICanUseMargin,
        ICanUseCustomClasses
    {
        private const string ShieldCssPath = "/css/components/Shield.css";

        private string _label = string.Empty;
        private string _value = string.Empty;
        private string _labelColor = "dimgrey";
        private string _valueColor = "darkgrey";

        /// <summary>
        /// Initializes a new instance of the <see cref="ShieldBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer used by the current rendering context.</param>
        /// <param name="html">The HTML helper associated with the current Razor view.</param>
        public ShieldBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "span";
            InternalAddClass("shield");
            SetData("shield", "true");
        }

        /// <summary>
        /// Sets the label rendered on the left side of the shield.
        /// </summary>
        /// <param name="label">The label text.</param>
        /// <returns>The current builder instance.</returns>
        public ShieldBuilder SetLabel(string? label)
        {
            _label = label ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets the value rendered on the right side of the shield.
        /// </summary>
        /// <param name="value">The value text.</param>
        /// <returns>The current builder instance.</returns>
        public ShieldBuilder SetValue(string? value)
        {
            _value = value ?? string.Empty;
            return this;
        }

        /// <summary>
        /// Sets the label background color.
        /// </summary>
        /// <param name="color">The CSS color value.</param>
        /// <returns>The current builder instance.</returns>
        public ShieldBuilder SetLabelColor(string? color)
        {
            _labelColor = NormalizeColor(color, "dimgrey");
            return this;
        }

        /// <summary>
        /// Sets the value background color.
        /// </summary>
        /// <param name="color">The CSS color value.</param>
        /// <returns>The current builder instance.</returns>
        public ShieldBuilder SetValueColor(string? color)
        {
            _valueColor = NormalizeColor(color, "darkgrey");
            return this;
        }

        /// <summary>
        /// Sets both shield background colors.
        /// </summary>
        /// <param name="labelColor">The label CSS color value.</param>
        /// <param name="valueColor">The value CSS color value.</param>
        /// <returns>The current builder instance.</returns>
        public ShieldBuilder SetColors(string? labelColor, string? valueColor)
        {
            return SetLabelColor(labelColor).SetValueColor(valueColor);
        }
        /// <inheritdoc />
        protected override ShieldBuilder CreateInstance()
        {
            return new ShieldBuilder(_textWriter, _htmlHelper);
        }
        /// <inheritdoc />
        protected override void InternalClone(ShieldBuilder source)
        {
            base.InternalClone(source);
            _label = source._label;
            _value = source._value;
            _labelColor = source._labelColor;
            _valueColor = source._valueColor;
        }

        /// <summary>
        /// Renders the shield to an HTML content instance.
        /// </summary>
        /// <returns>The generated HTML content.</returns>
        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write("<span class=\"shield-label\" style=\"background-color:");
            encoder.Encode(writer, _labelColor);
            writer.Write("\">");
            encoder.Encode(writer, _label);
            writer.Write("</span><span class=\"shield-value\" style=\"background-color:");
            encoder.Encode(writer, _valueColor);
            writer.Write("\">");
            encoder.Encode(writer, _value);
            writer.Write($"</span></{GetTag()}>");
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(ShieldCssPath);
        }

        private static string NormalizeColor(string? color, string fallback)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                return fallback;
            }

            return color.Trim();
        }
    }
}
