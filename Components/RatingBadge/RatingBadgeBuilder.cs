#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj RatingBadgeBuilder.cs create at 2026/05/05
// ©2024-2026 idéMobi SARL FRANCE

#endregion

#region

using System.Globalization;
using System.Net;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Renders a Bootstrap Icons based rating badge with full, half, and empty stars.
    /// </summary>
    /// <remarks>
    /// The component supports configurable maximum star counts, Bootstrap variants,
    /// cartouche presentation, optional rating value display, and CSS-based effects.
    /// </remarks>
    public sealed class RatingBadgeBuilder :
        HtmlBuilderBase<RatingBadgeBuilder>,
        ICanUseMargin,
        ICanUsePadding,
        ICanUseCustomClasses,
        ICanUseRatingBadgeStyle,
        ICanUseRatingBadgeEffects
    {
        private const string RatingCssPath = "/css/components/RatingBadge.css";

        private double _value;
        private int _maxStars = 5;
        private bool _showValue;
        private string? _label;

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(RatingCssPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RatingBadgeBuilder"/> class.
        /// </summary>
        /// <param name="writer">The writer used by the current rendering context.</param>
        /// <param name="html">The HTML helper associated with the current Razor view.</param>
        public RatingBadgeBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            this.AddClass("rating-badge");
            SetAttribute("role", "img");
            SetData("rating-badge", "true");
            SetCssComposer(new RatingBadgeComposer());
        }

        /// <summary>
        /// Sets the rating value to render.
        /// </summary>
        /// <param name="value">The rating value before clamping and half-star rounding.</param>
        /// <returns>The current builder instance.</returns>
        public RatingBadgeBuilder SetRating(double value)
        {
            _value = value;
            return this;
        }

        /// <summary>
        /// Sets the maximum number of stars displayed by the badge.
        /// </summary>
        /// <param name="maxStars">The maximum number of stars. Values lower than one are normalized to one.</param>
        /// <returns>The current builder instance.</returns>
        public RatingBadgeBuilder SetMaxStars(int maxStars)
        {
            _maxStars = Math.Max(1, maxStars);
            return this;
        }

        /// <summary>
        /// Shows or hides the textual rating value next to the stars.
        /// </summary>
        /// <param name="value">A value indicating whether the numeric value should be shown.</param>
        /// <returns>The current builder instance.</returns>
        public RatingBadgeBuilder SetShowValue(bool value = true)
        {
            _showValue = value;
            return this;
        }

        /// <summary>
        /// Sets the accessible label announced for the rating badge.
        /// </summary>
        /// <param name="label">The label to use. When omitted, a label is generated from the rating and maximum star count.</param>
        /// <returns>The current builder instance.</returns>
        public RatingBadgeBuilder SetLabel(string? label)
        {
            _label = label;
            return this;
        }
        /// <inheritdoc />
        protected override RatingBadgeBuilder CreateInstance()
        {
            return new RatingBadgeBuilder(_textWriter, _htmlHelper);
        }
        /// <inheritdoc />
        protected override void InternalClone(RatingBadgeBuilder source)
        {
            base.InternalClone(source);
            _value = source._value;
            _maxStars = source._maxStars;
            _showValue = source._showValue;
            _label = source._label;
        }

        /// <summary>
        /// Renders the rating badge to an HTML content instance.
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

            double roundedValue = GetRoundedValue();
            string ariaLabel = _label ?? $"{roundedValue.ToString("0.#", CultureInfo.CurrentCulture)} / {_maxStars}";

            SetAttribute("aria-label", ariaLabel);

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write("<span class=\"rating-badge-stars\" aria-hidden=\"true\">");

            int fullStars = (int)Math.Floor(roundedValue);
            bool hasHalfStar = roundedValue - fullStars >= 0.5d;

            for (int i = 0; i < _maxStars; i++)
            {
                string icon = i < fullStars
                    ? "bi-star-fill"
                    : i == fullStars && hasHalfStar
                        ? "bi-star-half"
                        : "bi-star";

                string emptyClass = i >= fullStars && !(i == fullStars && hasHalfStar)
                    ? " rating-badge-star-empty"
                    : string.Empty;

                writer.Write($"<i class=\"bi {icon} rating-badge-star{emptyClass}\"></i>");
            }

            writer.Write("</span>");

            if (_showValue)
            {
                writer.Write("<span class=\"rating-badge-value\">");
                writer.Write(WebUtility.HtmlEncode($"{roundedValue.ToString("0.#", CultureInfo.CurrentCulture)} / {_maxStars}"));
                writer.Write("</span>");
            }

            writer.Write($"</{GetTag()}>");
        }

        private double GetRoundedValue()
        {
            double clampedValue = Math.Clamp(_value, 0d, _maxStars);
            return Math.Round(clampedValue * 2d, MidpointRounding.AwayFromZero) / 2d;
        }
    }
}
