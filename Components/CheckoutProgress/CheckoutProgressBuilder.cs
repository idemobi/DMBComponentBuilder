using System.Net;
using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public sealed class CheckoutProgressBuilder :
        HtmlBuilderBase<CheckoutProgressBuilder>,
        ICanUseCustomClasses
    {
        private const string CheckoutProgressCssPath = "/css/components/CheckoutProgress.css";

        private readonly List<CheckoutProgressDefinition> _items = new();
        private bool _showSubtitles = true;
        private bool _compact;

        public CheckoutProgressBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "nav";
            this.AddClass("checkout-progress-builder");
            SetAttribute("aria-label", "Checkout steps");
            SetData("checkout-progress-builder", "true");
        }

        public CheckoutProgressBuilder AddStep(
            string? title,
            string? subtitle = null,
            bool active = false,
            IconStruct icon = default,
            VariantStyle variant = VariantStyle.Primary,
            string? id = null,
            string? cssClass = null)
        {
            return AddStep(title, subtitle, icon, variant, active ? CheckoutProgressState.Active : CheckoutProgressState.Inactive, id, cssClass);
        }

        public CheckoutProgressBuilder AddStep(
            string? title,
            string? subtitle,
            IconStruct icon,
            VariantStyle variant,
            CheckoutProgressState state,
            string? id = null,
            string? cssClass = null)
        {
            _items.Add(new CheckoutProgressDefinition
            {
                Id = id,
                Title = title,
                Subtitle = subtitle,
                Icon = icon,
                Variant = variant,
                State = state,
                CssClass = cssClass
            });

            return this;
        }

        public CheckoutProgressBuilder AddStep(
            string? title,
            CheckoutProgressState state,
            IconStruct icon = default,
            VariantStyle variant = VariantStyle.Primary,
            string? subtitle = null,
            string? id = null,
            string? cssClass = null)
        {
            return AddStep(title, subtitle, icon, variant, state, id, cssClass);
        }

        public CheckoutProgressBuilder SetCurrentStep(int stepNumber)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                int currentNumber = i + 1;
                _items[i].State = currentNumber < stepNumber
                    ? CheckoutProgressState.Complete
                    : currentNumber == stepNumber
                        ? CheckoutProgressState.Active
                        : CheckoutProgressState.Inactive;
            }

            return this;
        }

        public CheckoutProgressBuilder SetStepActive(int stepNumber, bool active = true)
        {
            CheckoutProgressDefinition? step = GetStep(stepNumber);
            if (step != null)
            {
                step.State = active ? CheckoutProgressState.Active : CheckoutProgressState.Inactive;
            }

            return this;
        }

        public CheckoutProgressBuilder SetStepComplete(int stepNumber, bool complete = true)
        {
            CheckoutProgressDefinition? step = GetStep(stepNumber);
            if (step != null)
            {
                step.State = complete ? CheckoutProgressState.Complete : CheckoutProgressState.Inactive;
            }

            return this;
        }

        public CheckoutProgressBuilder ShowSubtitles(bool value = true)
        {
            _showSubtitles = value;
            return this;
        }

        public CheckoutProgressBuilder Compact(bool value = true)
        {
            _compact = value;
            return this;
        }

        private CheckoutProgressDefinition? GetStep(int stepNumber)
        {
            if (stepNumber < 1 || stepNumber > _items.Count)
            {
                return null;
            }

            return _items[stepNumber - 1];
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(CheckoutProgressCssPath);
        }

        protected override CheckoutProgressBuilder CreateInstance()
        {
            return new CheckoutProgressBuilder(_textWriter, _htmlHelper);
        }

        protected override void InternalClone(CheckoutProgressBuilder source)
        {
            base.InternalClone(source);

            _items.Clear();
            _items.AddRange(source._items.Select(x => x.Clone()));
            _showSubtitles = source._showSubtitles;
            _compact = source._compact;
        }

        public override IHtmlContent Render()
        {
            EnsureAssets();

            using StringWriter writer = new();
            WriteToCore(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }

        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            EnsureAssets();

            if (_items.Count == 0)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(GetId()))
            {
                SetEnsureId("checkout-progress-builder");
            }

            if (_compact)
            {
                this.AddClass("checkout-progress-builder-compact");
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");
            writer.Write("<ol class=\"checkout-progress-list\">");

            for (int i = 0; i < _items.Count; i++)
            {
                WriteItem(writer, encoder, _items[i], i);
            }

            writer.Write("</ol>");
            writer.Write($"</{GetTag()}>");
        }

        private void WriteItem(TextWriter writer, HtmlEncoder encoder, CheckoutProgressDefinition item, int index)
        {
            int stepNumber = index + 1;
            string id = string.IsNullOrWhiteSpace(item.Id)
                ? $"{GetId()}_item_{index}"
                : HtmlIdGenerator.CleanId(item.Id) ?? $"{GetId()}_item_{index}";
            string variant = item.Variant.GetVariantCss();
            string state = item.State.ToString().ToLowerInvariant();
            string active = item.State == CheckoutProgressState.Inactive ? "false" : "true";
            string ariaCurrent = item.State == CheckoutProgressState.Active ? " aria-current=\"step\"" : string.Empty;
            string ariaDisabled = item.State == CheckoutProgressState.Inactive ? " aria-disabled=\"true\"" : string.Empty;
            string previousComplete = index > 0 && _items[index - 1].State == CheckoutProgressState.Complete
                ? " checkout-progress-previous-complete"
                : string.Empty;
            string customClass = string.IsNullOrWhiteSpace(item.CssClass) ? string.Empty : $" {WebUtility.HtmlEncode(item.CssClass)}";

            if (string.IsNullOrWhiteSpace(variant))
            {
                variant = "primary";
            }

            writer.Write($"<li id=\"{WebUtility.HtmlEncode(id)}\" class=\"checkout-progress-item checkout-progress-state-{WebUtility.HtmlEncode(state)} checkout-progress-variant-{WebUtility.HtmlEncode(variant)}{previousComplete}{customClass}\" data-checkout-progress-active=\"{active}\"{ariaCurrent}{ariaDisabled}>");
            writer.Write("<span class=\"checkout-progress-connector\" aria-hidden=\"true\"></span>");
            writer.Write($"<span class=\"checkout-progress-marker border-{WebUtility.HtmlEncode(variant)} text-{WebUtility.HtmlEncode(variant)}\" aria-hidden=\"true\">");

            if (!item.Icon.IsEmpty)
            {
                HtmlLayoutExtensions.IconBuilder(_htmlHelper, item.Icon, null, null)
                    .WriteTo(writer, encoder);
            }
            else
            {
                string label = string.IsNullOrWhiteSpace(item.Label) ? stepNumber.ToString() : item.Label;
                writer.Write(WebUtility.HtmlEncode(label));
            }

            writer.Write("</span>");
            writer.Write("<span class=\"checkout-progress-text\">");

            if (!string.IsNullOrWhiteSpace(item.Title))
            {
                writer.Write($"<span class=\"checkout-progress-title\">{WebUtility.HtmlEncode(item.Title)}</span>");
            }

            if (_showSubtitles && !string.IsNullOrWhiteSpace(item.Subtitle))
            {
                writer.Write($"<span class=\"checkout-progress-subtitle\">{WebUtility.HtmlEncode(item.Subtitle)}</span>");
            }

            writer.Write("</span>");
            writer.Write("</li>");
        }
    }
}
