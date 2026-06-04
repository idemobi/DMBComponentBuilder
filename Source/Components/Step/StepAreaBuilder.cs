#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Builds and renders the step visual component for Razor views.
    /// </summary>
    public sealed class StepAreaBuilder :
        HtmlBuilderBase<StepAreaBuilder>,
        ICanUseCustomClasses,
        IDisposable
    {
        #region Constants

        private const string CurrentContextKey = "__DMB_CURRENT_STEP_AREA_BUILDER__";
        private const string StepCssPath = "/css/components/Step.css";
        private const string StepJsPath = "/js/components/Step.js";

        #endregion

        #region Static methods

        internal static StepAreaBuilder? GetCurrent(IHtmlHelper html)
        {
            if (html?.ViewContext?.HttpContext?.Items == null)
            {
                return null;
            }

            return html.ViewContext.HttpContext.Items.TryGetValue(CurrentContextKey, out object? value)
                ? value as StepAreaBuilder
                : null;
        }

        private static void SetCurrent(IHtmlHelper html, StepAreaBuilder? area)
        {
            if (html?.ViewContext?.HttpContext?.Items == null)
            {
                return;
            }

            if (area == null)
            {
                html.ViewContext.HttpContext.Items.Remove(CurrentContextKey);
            }
            else
            {
                html.ViewContext.HttpContext.Items[CurrentContextKey] = area;
            }
        }

        private static void WriteContent(TextWriter writer, StepDefinition item)
        {
            if (string.IsNullOrWhiteSpace(item.ContentHtml))
            {
                return;
            }

            writer.Write("<div class=\"step-content\">");
            writer.Write(item.ContentHtml);
            writer.Write("</div>");
        }

        #endregion

        #region Instance fields and properties

        private bool _disposed
        {
            get => GetInternal("_disposed", false);
            set => SetInternal("_disposed", value);
        }

        private readonly List<StepDefinition> _items = new();

        private bool _numbered
        {
            get => GetInternal("_numbered", true);
            set => SetInternal("_numbered", value);
        }

        private readonly TextWriter _outputWriter;
        private StepAreaBuilder? _previousStepArea;
        private readonly List<StepRuleDefinition> _rules = new();

        private bool _started
        {
            get => GetInternal("_started", false);
            set => SetInternal("_started", value);
        }

        private bool _useFieldsets
        {
            get => GetInternal("_useFieldsets", false);
            set => SetInternal("_useFieldsets", value);
        }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StepAreaBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public StepAreaBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "div";
            _outputWriter = html.ViewContext.Writer;

            this.AddClass("step-area");
            SetData("step-area", "true");
        }

        #endregion

        #region Instance methods

        private StepAreaBuilder AddRule(string targetStepId, string fieldId, string condition)
        {
            string? cleanTargetStepId = HtmlIdGenerator.CleanId(targetStepId);

            if (string.IsNullOrWhiteSpace(cleanTargetStepId) || string.IsNullOrWhiteSpace(fieldId))
            {
                return this;
            }

            _rules.Add(new StepRuleDefinition
            {
                TargetStepId = cleanTargetStepId,
                FieldId = fieldId,
                Condition = condition
            });

            return this;
        }

        /// <summary>
        ///     Starts the step rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            EnsureAssets();

            _started = true;

            if (string.IsNullOrWhiteSpace(GetId()))
            {
                SetEnsureId("step-area");
            }

            _previousStepArea = GetCurrent(_htmlHelper);
            SetCurrent(_htmlHelper, this);

            return this;
        }

        /// <inheritdoc />
        protected override StepAreaBuilder CreateInstance()
        {
            return new StepAreaBuilder(_textWriter, _htmlHelper);
        }

        /// <summary>
        ///     Adds an enablement rule for step when all values in the step component.
        /// </summary>
        /// <param name="targetStepId">The target step id value.</param>
        /// <param name="fieldIds">The field ids value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder EnableStepWhenAllValues(string targetStepId, params string[] fieldIds)
        {
            string? cleanTargetStepId = HtmlIdGenerator.CleanId(targetStepId);

            if (string.IsNullOrWhiteSpace(cleanTargetStepId))
            {
                return this;
            }

            List<string> cleanFieldIds = fieldIds
                .Where(fieldId => string.IsNullOrWhiteSpace(fieldId) == false)
                .ToList();

            if (cleanFieldIds.Count == 0)
            {
                return this;
            }

            _rules.Add(new StepRuleDefinition
            {
                TargetStepId = cleanTargetStepId,
                FieldIds = cleanFieldIds,
                Condition = "all-values"
            });

            return this;
        }

        /// <summary>
        ///     Adds an enablement rule for step when checked in the step component.
        /// </summary>
        /// <param name="targetStepId">The target step id value.</param>
        /// <param name="fieldId">The field id value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder EnableStepWhenChecked(string targetStepId, string fieldId)
        {
            return AddRule(targetStepId, fieldId, "checked");
        }

        /// <summary>
        ///     Adds an enablement rule for step when value in the step component.
        /// </summary>
        /// <param name="targetStepId">The target step id value.</param>
        /// <param name="fieldId">The field id value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder EnableStepWhenValue(string targetStepId, string fieldId)
        {
            return AddRule(targetStepId, fieldId, "value");
        }

        private void EnsureAssets()
        {
            PageInformation page = PageRegistry.GetOrCreatePageInformation(_htmlHelper.ViewContext.HttpContext);
            page.SetStylesheet(StepCssPath);
            page.SetScriptFile(StepJsPath);
        }

        /// <inheritdoc />
        protected override void InternalClone(StepAreaBuilder source)
        {
            base.InternalClone(source);

            _items.Clear();
            _items.AddRange(source._items.Select(x => x.Clone()));

            _rules.Clear();
            _rules.AddRange(source._rules.Select(x => x.Clone()));

            _previousStepArea = null;
            _disposed = false;
            _started = false;
            _numbered = source._numbered;
            _useFieldsets = source._useFieldsets;
        }

        /// <summary>
        ///     Configures numbered behavior for the step component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder Numbered(bool value = true)
        {
            _numbered = value;
            return this;
        }

        internal void RegisterItem(StepDefinition definition)
        {
            ArgumentNullException.ThrowIfNull(definition);

            int index = _items.Count;

            definition.Id = string.IsNullOrWhiteSpace(definition.Id)
                ? $"{GetId()}_item_{index}"
                : HtmlIdGenerator.CleanId(definition.Id) ?? $"{GetId()}_item_{index}";

            if (_numbered && string.IsNullOrWhiteSpace(definition.Label) && definition.Icon.IsEmpty)
            {
                definition.Label = (index + 1).ToString();
            }

            if (_useFieldsets)
            {
                definition.UseFieldset = true;
            }

            _items.Add(definition);
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
        ///     Configures whether the fieldsets option is used by the step component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public StepAreaBuilder UseFieldsets(bool value = true)
        {
            _useFieldsets = value;
            return this;
        }

        private void WriteHeader(TextWriter writer, HtmlEncoder encoder, StepDefinition item)
        {
            if (string.IsNullOrWhiteSpace(item.Title) && string.IsNullOrWhiteSpace(item.Subtitle))
            {
                return;
            }

            writer.Write("<header class=\"step-header\">");

            if (!string.IsNullOrWhiteSpace(item.Title))
            {
                string tag = item.TitleLevel.Tag();
                writer.Write($"<{tag} class=\"step-title\">{WebUtility.HtmlEncode(item.Title)}</{tag}>");
            }

            if (!string.IsNullOrWhiteSpace(item.Subtitle))
            {
                writer.Write($"<div class=\"step-subtitle\">{WebUtility.HtmlEncode(item.Subtitle)}</div>");
            }

            writer.Write("</header>");
        }

        private void WriteItem(TextWriter writer, HtmlEncoder encoder, StepDefinition item)
        {
            string variant = item.Variant.GetVariantCss();
            if (string.IsNullOrWhiteSpace(variant))
            {
                variant = "primary";
            }

            string state = item.State.ToString().ToLowerInvariant();
            string tag = item.UseFieldset ? "fieldset" : "section";
            string disabledAttribute = item.UseFieldset && item.State == StepBlockState.Disabled
                ? " disabled=\"disabled\""
                : string.Empty;

            writer.Write($"<{tag} id=\"{WebUtility.HtmlEncode(item.Id)}\" class=\"step-block step-state-{state} step-variant-{WebUtility.HtmlEncode(variant)}\"{disabledAttribute}>");
            writer.Write("<div class=\"step-axis\">");
            writer.Write($"<span class=\"step-marker border-{WebUtility.HtmlEncode(variant)} text-{WebUtility.HtmlEncode(variant)}\">");

            if (!item.Icon.IsEmpty)
            {
                HtmlLayoutExtensions.IconBuilder(_htmlHelper, item.Icon, null, null)
                    .WriteTo(writer, encoder);
            }
            else if (!string.IsNullOrWhiteSpace(item.Label))
            {
                writer.Write(WebUtility.HtmlEncode(item.Label));
            }

            writer.Write("</span>");
            writer.Write("</div>");

            writer.Write("<div class=\"step-body\">");
            WriteHeader(writer, encoder, item);
            WriteContent(writer, item);
            writer.Write("</div>");

            writer.Write($"</{tag}>");
        }

        private void WriteRules(TextWriter writer)
        {
            if (_rules.Count == 0)
            {
                return;
            }

            string json = JsonSerializer.Serialize(_rules, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            writer.Write($"<script type=\"application/json\" data-step-rules=\"{WebUtility.HtmlEncode(GetId())}\">");
            writer.Write(json);
            writer.Write("</script>");
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            if (_items.Count == 0)
            {
                return;
            }

            writer.Write($"<{GetTag()}{BuildAttributes()}>");

            foreach (StepDefinition item in _items)
            {
                WriteItem(writer, encoder, item);
            }

            writer.Write($"</{GetTag()}>");
            WriteRules(writer);
        }

        #region From interface IDisposable

        /// <summary>
        ///     Completes the active step rendering or capture scope.
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            SetCurrent(_htmlHelper, _previousStepArea);

            if (!_started)
            {
                return;
            }

            WriteTo(_outputWriter, HtmlEncoder.Default);
        }

        #endregion

        #endregion
    }
}