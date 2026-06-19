#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

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
    ///     Renders a reusable avatar with an icon, image, initials, color variant, custom colors, and optional badges.
    /// </summary>
    /// <remarks>
    ///     The component is domain neutral and is intended for profile previews, conversations, author blocks, support
    ///     tickets, forum messages, and other places where a compact public identity is displayed.
    /// </remarks>
    [Documented]
    public sealed class AvatarBuilder :
        HtmlBuilderBase<AvatarBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin
    {
        #region Constants

        private const string AvatarCssPath = "/css/components/Avatar.css";
        private const string AvatarScriptPath = "/js/components/Avatar.js";

        #endregion

        #region Instance fields and properties

        private readonly List<AvatarBadge> _badges = new();
        private string _displayName = string.Empty;
        private string _foregroundColor = string.Empty;
        private string _backgroundColor = string.Empty;
        private IconStruct _icon = IconStruct.Bootstrap("bi-person-fill");
        private string _imageAlt = string.Empty;
        private string _imageSrc = string.Empty;
        private string _initials = string.Empty;
        private bool _rounded = true;
        private AvatarSize _size = AvatarSize.Medium;
        private VariantStyle _variant = VariantStyle.Secondary;

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="AvatarBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        [Documented]
        public AvatarBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("avatar-builder");
            SetData("avatar-builder", "true");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds a compact badge to the avatar.
        /// </summary>
        /// <param name="text">The badge text.</param>
        /// <param name="variant">The Bootstrap variant applied to the badge.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder AddBadge(string? text, VariantStyle variant = VariantStyle.Danger)
        {
            if (!string.IsNullOrWhiteSpace(text))
            {
                _badges.Add(new AvatarBadge
                {
                    Text = text.Trim(),
                    Variant = variant
                });
            }

            return this;
        }

        /// <inheritdoc />
        protected override AvatarBuilder CreateInstance()
        {
            return new AvatarBuilder(_textWriter, _htmlHelper);
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(AvatarCssPath);
            page.SetScriptFile(AvatarScriptPath);
        }

        private void RefreshDynamicClasses(string variant)
        {
            _classesOfComponent.RemoveWhere(cssClass =>
                string.Equals(cssClass, "avatar-builder-small", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-medium", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-large", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-extralarge", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-primary", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-secondary", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-success", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-info", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-warning", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-danger", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-light", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-dark", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-has-badge", StringComparison.Ordinal) ||
                string.Equals(cssClass, "avatar-builder-has-image", StringComparison.Ordinal));

            InternalAddClass($"avatar-builder-{_size.ToString().ToLowerInvariant()}");

            if (_badges.Count > 0)
            {
                InternalAddClass("avatar-builder-has-badge");
            }

            if (string.IsNullOrWhiteSpace(_backgroundColor))
            {
                InternalAddClass($"avatar-builder-{variant}");
            }

            if (!string.IsNullOrWhiteSpace(_imageSrc))
            {
                InternalAddClass("avatar-builder-has-image");
            }
        }

        /// <inheritdoc />
        protected override void InternalClone(AvatarBuilder source)
        {
            base.InternalClone(source);
            _badges.Clear();
            _badges.AddRange(source._badges.Select(badge => badge.Clone()));
            _displayName = source._displayName;
            _foregroundColor = source._foregroundColor;
            _backgroundColor = source._backgroundColor;
            _icon = source._icon;
            _imageAlt = source._imageAlt;
            _imageSrc = source._imageSrc;
            _initials = source._initials;
            _rounded = source._rounded;
            _size = source._size;
            _variant = source._variant;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }

        /// <summary>
        ///     Sets custom CSS colors for the avatar surface.
        /// </summary>
        /// <param name="backgroundColor">The background CSS color.</param>
        /// <param name="foregroundColor">The foreground CSS color.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetCustomColors(string? backgroundColor, string? foregroundColor)
        {
            _backgroundColor = backgroundColor ?? string.Empty;
            _foregroundColor = foregroundColor ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the display name used for accessibility and fallback initials.
        /// </summary>
        /// <param name="displayName">The public display name.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetDisplayName(string? displayName)
        {
            _displayName = displayName ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the icon rendered inside the avatar when no image is configured.
        /// </summary>
        /// <param name="icon">The icon to render.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetIcon(IconStruct icon)
        {
            _icon = icon;
            return this;
        }

        /// <summary>
        ///     Sets the Bootstrap icon class rendered inside the avatar when no image is configured.
        /// </summary>
        /// <param name="iconClass">The Bootstrap icon class.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetIconBootstrap(string? iconClass)
        {
            _icon = IconStruct.Bootstrap(iconClass ?? string.Empty);
            return this;
        }

        /// <summary>
        ///     Sets an image rendered inside the avatar.
        /// </summary>
        /// <param name="src">The image URL or path.</param>
        /// <param name="alt">The image alternative text.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetImage(string? src, string? alt = null)
        {
            _imageSrc = src ?? string.Empty;
            _imageAlt = alt ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets initials rendered when no image or icon is available.
        /// </summary>
        /// <param name="initials">The initials text.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetInitials(string? initials)
        {
            _initials = initials ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets whether the avatar surface should be circular.
        /// </summary>
        /// <param name="rounded">True to render a circular avatar; false for a square avatar with rounded corners.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetRounded(bool rounded = true)
        {
            _rounded = rounded;
            return this;
        }

        /// <summary>
        ///     Sets the avatar size.
        /// </summary>
        /// <param name="size">The size to render.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetSize(AvatarSize size)
        {
            _size = size;
            return this;
        }

        /// <summary>
        ///     Sets the Bootstrap variant used when custom colors are not configured.
        /// </summary>
        /// <param name="variant">The Bootstrap variant.</param>
        /// <returns>The configured builder instance.</returns>
        [Documented]
        public AvatarBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        private string ResolveAriaLabel()
        {
            return string.IsNullOrWhiteSpace(_displayName)
                ? "Avatar"
                : _displayName;
        }

        private string ResolveInitials()
        {
            if (!string.IsNullOrWhiteSpace(_initials))
            {
                return _initials.Trim().Length > 3
                    ? _initials.Trim()[..3]
                    : _initials.Trim();
            }

            string[] parts = _displayName.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length == 0)
            {
                return string.Empty;
            }

            return string.Concat(parts.Take(2).Select(part => char.ToUpperInvariant(part[0])));
        }

        private static string ResolveVariantCss(VariantStyle variant)
        {
            string variantCss = variant.GetVariantCss();
            return string.IsNullOrWhiteSpace(variantCss) ? "secondary" : variantCss;
        }

        private void WriteBadges(TextWriter writer, HtmlEncoder encoder)
        {
            if (_badges.Count == 0)
            {
                return;
            }

            writer.Write("<span class=\"avatar-builder-badges\" aria-hidden=\"true\">");

            foreach (AvatarBadge badge in _badges)
            {
                string badgeVariant = ResolveVariantCss(badge.Variant);
                writer.Write($"<span class=\"avatar-builder-badge text-bg-{badgeVariant}\">");
                encoder.Encode(writer, badge.Text);
                writer.Write("</span>");
            }

            writer.Write("</span>");
        }

        private void WriteContent(TextWriter writer, HtmlEncoder encoder)
        {
            if (!string.IsNullOrWhiteSpace(_imageSrc))
            {
                writer.Write("<img class=\"avatar-builder-image\" data-avatar-image src=\"");
                encoder.Encode(writer, _imageSrc);
                writer.Write("\" alt=\"");
                encoder.Encode(writer, string.IsNullOrWhiteSpace(_imageAlt) ? _displayName : _imageAlt);
                writer.Write("\" />");
                writer.Write("<span class=\"avatar-builder-fallback\" aria-hidden=\"true\">");
                WriteFallbackContent(writer, encoder);
                writer.Write("</span>");
                return;
            }

            WriteFallbackContent(writer, encoder);
        }

        private void WriteFallbackContent(TextWriter writer, HtmlEncoder encoder)
        {
            if (!_icon.IsEmpty)
            {
                _htmlHelper.IconBuilder(_icon, "avatar-builder-icon").WriteTo(writer, encoder);
                return;
            }

            string initials = ResolveInitials();
            if (!string.IsNullOrWhiteSpace(initials))
            {
                writer.Write("<span class=\"avatar-builder-initials\">");
                encoder.Encode(writer, initials);
                writer.Write("</span>");
            }
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            string variant = ResolveVariantCss(_variant);
            RefreshDynamicClasses(variant);

            SetAttribute("role", "img");
            SetAttribute("aria-label", ResolveAriaLabel());
            SetData("avatar-display-name", _displayName);

            if (!string.IsNullOrWhiteSpace(_backgroundColor))
            {
                SetStyle("--avatar-bg", _backgroundColor);
            }
            else
            {
                RemoveStyle("--avatar-bg");
            }

            if (!string.IsNullOrWhiteSpace(_foregroundColor))
            {
                SetStyle("--avatar-fg", _foregroundColor);
            }
            else
            {
                RemoveStyle("--avatar-fg");
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write("<span class=\"avatar-builder-frame\">");
            writer.Write($"<span class=\"avatar-builder-surface{(_rounded ? " avatar-builder-rounded" : " avatar-builder-soft")}\">");
            WriteContent(writer, encoder);
            writer.Write("</span>");
            WriteBadges(writer, encoder);
            writer.Write("</span>");
            writer.Write($"</{GetTag()}>");
        }

        #endregion
    }
}
