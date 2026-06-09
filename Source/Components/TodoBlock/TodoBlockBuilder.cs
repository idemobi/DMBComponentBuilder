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
    ///     Renders a Bootstrap card containing a structured list of items, each with an icon, a title
    ///     and a subtitle. Supports an optional header (image, title, subtitle) and an optional
    ///     checkable mode that adds a visual checkbox per item.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         <b>Use cases:</b> feature lists, task lists, capability overviews, onboarding checklists,
    ///         product highlights.
    ///     </para>
    ///     <para>
    ///         <b>Example:</b>
    ///         <code>
    /// @Html.TodoBlockBuilder()
    ///     .SetHeader("/logo/logo.png", "PageBuilder ecosystem", "Razor, Bootstrap, effects, docs")
    ///     .SetVariant(VariantStyle.Dark)
    ///     .SetCheckable(false)
    ///     .AddItem(IconStruct.Bootstrap("bi-braces"),          "Declarative", "Fluent C# API",          VariantStyle.Warning)
    ///     .AddItem(IconStruct.Bootstrap("bi-layers"),          "Composable",  "sections, cards, actions", VariantStyle.Warning)
    ///     .AddItem(IconStruct.Bootstrap("bi-magic"),           "Alive",       "built-in visual effects",  VariantStyle.Warning)
    ///     .AddItem(IconStruct.Bootstrap("bi-journal-richtext"),"Documented",  "package references",       VariantStyle.Warning)
    ///     .Render()
    /// </code>
    ///     </para>
    /// </remarks>
    [Documented]
    public sealed class TodoBlockBuilder :
        HtmlBuilderBase<TodoBlockBuilder>,
        ICanUseCustomClasses,
        ICanUseMargin,
        ICanUsePadding,
        ICanUseShadow,
        ICanUseBorder,
        ICanUseBorderRadius,
        ICanUseWidth,
        ICanUseThemeVisibility
    {
        #region Instance fields and properties

        private bool _checkable;
        private string _headerImageAlt = string.Empty;

        private string _headerImageSrc = string.Empty;
        private string _headerSubtitle = string.Empty;
        private string _headerTitle = string.Empty;
        private readonly List<TodoBlockItem> _items = new();
        private VariantStyle _variant = VariantStyle.Normal;

        #endregion

        #region Instance constructors and destructors

        /// <summary>Initializes a new <see cref="TodoBlockBuilder" />.</summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public TodoBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            InternalAddClasses("todo-block-builder", "card");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Adds an item to the list.
        /// </summary>
        /// <param name="icon">Icon displayed on the left of the item.</param>
        /// <param name="title">Bold label for the item.</param>
        /// <param name="subtitle">Secondary description displayed below the title.</param>
        /// <param name="iconVariant">Bootstrap variant used to color the icon. Default: <see cref="VariantStyle.Warning" />.</param>
        /// <param name="checked">Initial checked state when checkable mode is enabled.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TodoBlockBuilder AddItem(
            IconStruct icon,
            string title,
            string subtitle,
            VariantStyle iconVariant = VariantStyle.Warning,
            bool @checked = false
        )
        {
            _items.Add(new TodoBlockItem
            {
                Icon = icon,
                IconVariant = iconVariant,
                Title = title ?? string.Empty,
                Subtitle = subtitle ?? string.Empty,
                Checked = @checked
            });
            return this;
        }

        private void ApplyVariantClasses()
        {
            if (_variant == VariantStyle.Normal) return;

            string v = _variant.GetVariantCss();
            InternalAddClasses($"text-bg-{v}");

            if (_variant == VariantStyle.Dark) _attributes["data-bs-theme"] = "dark";
        }

        /// <inheritdoc />
        protected override TodoBlockBuilder CreateInstance() => new(_textWriter, _htmlHelper);

        private void EnsureCheckableAssets()
        {
            const string key = "todo-block-toggle";
            const string script =
                "(function(){" +
                "document.addEventListener('change',function(e){" +
                "var cb=e.target;" +
                "if(!cb.matches('.todo-block-builder .list-group-item input[type=\"checkbox\"]'))return;" +
                "var item=cb.closest('.list-group-item');" +
                "if(!item)return;" +
                "item.classList.toggle('opacity-50',cb.checked);" +
                "var title=item.querySelector('.lh-sm .fw-semibold');" +
                "if(title)title.classList.toggle('text-decoration-line-through',cb.checked);" +
                "});" +
                "})();";

            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.AddScriptInline(key, script);
        }

        /// <inheritdoc />
        protected override void InternalClone(TodoBlockBuilder source)
        {
            base.InternalClone(source);
            _items.Clear();
            _items.AddRange(source._items);
            _headerImageSrc = source._headerImageSrc;
            _headerImageAlt = source._headerImageAlt;
            _headerTitle = source._headerTitle;
            _headerSubtitle = source._headerSubtitle;
            _variant = source._variant;
            _checkable = source._checkable;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            using StringWriter sw = new();
            WriteToCore(sw, HtmlEncoder.Default);
            return new HtmlString(sw.ToString());
        }

        /// <summary>
        ///     Enables or disables checkable mode. When enabled, each item renders a visual checkbox
        ///     that reflects the item's checked state.
        /// </summary>
        /// <param name="checkable"><c>true</c> to show checkboxes; <c>false</c> to hide them.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TodoBlockBuilder SetCheckable(bool checkable)
        {
            _checkable = checkable;
            return this;
        }

        /// <summary>
        ///     Adds an optional header to the card with an image, a title and a subtitle.
        /// </summary>
        /// <param name="imageSrc">Path or URL of the header image.</param>
        /// <param name="title">Bold title displayed next to the image.</param>
        /// <param name="subtitle">Secondary text displayed below the title.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TodoBlockBuilder SetHeader(string imageSrc, string title, string subtitle)
        {
            _headerImageSrc = imageSrc ?? string.Empty;
            _headerImageAlt = title ?? string.Empty;
            _headerTitle = title ?? string.Empty;
            _headerSubtitle = subtitle ?? string.Empty;
            return this;
        }

        /// <summary>
        ///     Sets the Bootstrap color variant applied to the card background.
        ///     Translates to <c>bg-{variant} text-{variant}</c> utility classes.
        /// </summary>
        /// <param name="variant">The Bootstrap variant style.</param>
        /// <returns>The current builder instance for chaining.</returns>
        [Documented]
        public TodoBlockBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        private void WriteHeader(TextWriter writer, HtmlEncoder encoder)
        {
            if (string.IsNullOrWhiteSpace(_headerTitle)) return;

            writer.Write("<div class=\"card-header d-flex align-items-center gap-3 py-3\">");

            if (!string.IsNullOrWhiteSpace(_headerImageSrc))
            {
                writer.Write($"<img src=\"{encoder.Encode(_headerImageSrc)}\" alt=\"{encoder.Encode(_headerImageAlt)}\" class=\"rounded-2 flex-shrink-0\" style=\"width:48px;height:48px;object-fit:contain;\">");
            }

            writer.Write("<div class=\"lh-sm\">");
            writer.Write("<div class=\"fw-semibold\">");
            encoder.Encode(writer, _headerTitle);
            writer.Write("</div>");

            if (!string.IsNullOrWhiteSpace(_headerSubtitle))
            {
                writer.Write("<div class=\"small opacity-75 mt-1\">");
                encoder.Encode(writer, _headerSubtitle);
                writer.Write("</div>");
            }

            writer.Write("</div></div>");
        }

        private void WriteItem(TextWriter writer, HtmlEncoder encoder, TodoBlockItem item)
        {
            bool isDone = _checkable && item.Checked;
            string itemOpacity = isDone ? " opacity-50" : string.Empty;

            writer.Write($"<div class=\"list-group-item d-flex align-items-center gap-3 bg-transparent py-3{itemOpacity}\">");

            if (_checkable)
            {
                string checkedAttr = item.Checked ? " checked" : string.Empty;
                writer.Write($"<input type=\"checkbox\" class=\"form-check-input flex-shrink-0 mt-0\"{checkedAttr}>");
            }

            if (!item.Icon.IsEmpty)
            {
                string v = item.IconVariant.GetVariantCss();
                string iconBgClass = $"bg-{v} bg-opacity-10";
                string iconTxtClass = $"text-{v} fs-5";
                writer.Write($"<span class=\"flex-shrink-0 d-inline-flex align-items-center justify-content-center rounded-2 {iconBgClass}\" style=\"width:2.25rem;height:2.25rem;\">");
                writer.Write($"<span class=\"{iconTxtClass}\">");
                _htmlHelper.IconBuilder(item.Icon).WriteTo(writer, encoder);
                writer.Write("</span></span>");
            }

            writer.Write("<div class=\"lh-sm\">");

            string titleClass = isDone ? "fw-semibold text-decoration-line-through" : "fw-semibold";
            writer.Write($"<div class=\"{titleClass}\">");
            encoder.Encode(writer, item.Title);
            writer.Write("</div>");

            if (!string.IsNullOrWhiteSpace(item.Subtitle))
            {
                writer.Write("<div class=\"small opacity-75 mt-1\">");
                encoder.Encode(writer, item.Subtitle);
                writer.Write("</div>");
            }

            writer.Write("</div></div>");
        }

        private void WriteItems(TextWriter writer, HtmlEncoder encoder)
        {
            if (_items.Count == 0) return;

            writer.Write("<div class=\"list-group list-group-flush\">");

            foreach (TodoBlockItem item in _items) WriteItem(writer, encoder, item);

            writer.Write("</div>");
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            ApplyVariantClasses();

            if (_checkable) EnsureCheckableAssets();

            writer.Write($"<div{BuildAttributes()}>");
            WriteHeader(writer, encoder);
            WriteItems(writer, encoder);
            writer.Write("</div>");
        }

        #endregion
    }
}