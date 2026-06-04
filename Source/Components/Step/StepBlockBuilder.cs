#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System;
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
    ///     Builds and renders the step visual component for Razor views.
    /// </summary>
    public sealed class StepBlockBuilder :
        HtmlBuilderBase<StepBlockBuilder>,
        IDisposable
    {
        #region Instance fields and properties

        private bool _asFieldset
        {
            get => GetInternal("_asFieldset", false);
            set => SetInternal("_asFieldset", value);
        }

        private StringWriter? _captureWriter;

        private bool _disposed
        {
            get => GetInternal("_disposed", false);
            set => SetInternal("_disposed", value);
        }

        private IconStruct _icon
        {
            get => GetInternal("_icon", IconStruct.Empty);
            set => SetInternal("_icon", value);
        }

        private string? _label
        {
            get => GetInternal<string?>("_label", null);
            set => SetInternal("_label", value);
        }

        private TextWriter? _originalWriter;

        private bool _started
        {
            get => GetInternal("_started", false);
            set => SetInternal("_started", value);
        }

        private StepBlockState _state
        {
            get => GetInternal("_state", StepBlockState.Future);
            set => SetInternal("_state", value);
        }

        private string? _subtitle
        {
            get => GetInternal<string?>("_subtitle", null);
            set => SetInternal("_subtitle", value);
        }

        private string? _title
        {
            get => GetInternal<string?>("_title", null);
            set => SetInternal("_title", value);
        }

        private TitleLevel _titleLevel
        {
            get => GetInternal("_titleLevel", TitleLevel.Three);
            set => SetInternal("_titleLevel", value);
        }

        private VariantStyle _variant
        {
            get => GetInternal("_variant", VariantStyle.Primary);
            set => SetInternal("_variant", value);
        }

        #endregion

        #region Instance constructors and destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="StepBlockBuilder" /> class.
        /// </summary>
        /// <param name="writer">The writer that receives the rendered HTML output.</param>
        /// <param name="html">The Razor HTML helper used to create the component builder.</param>
        public StepBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "section";
            SetData("step-block", "true");
        }

        #endregion

        #region Instance methods

        /// <summary>
        ///     Configures as fieldset behavior for the step component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder AsFieldset(bool value = true)
        {
            _asFieldset = value;
            return this;
        }

        /// <summary>
        ///     Starts the step rendering or capture scope.
        /// </summary>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder Begin()
        {
            if (_started)
            {
                return this;
            }

            StepAreaBuilder? area = StepAreaBuilder.GetCurrent(_htmlHelper);
            if (area == null)
            {
                throw new InvalidOperationException("StepBlockBuilder must be opened inside a StepAreaBuilder context.");
            }

            _started = true;
            _originalWriter = _htmlHelper.ViewContext.Writer;
            _captureWriter = new StringWriter();

            _htmlHelper.ViewContext.Writer = _captureWriter;

            return this;
        }

        /// <inheritdoc />
        protected override StepBlockBuilder CreateInstance()
        {
            return new StepBlockBuilder(_textWriter, _htmlHelper);
        }

        /// <summary>
        ///     Configures current behavior for the step component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder Current(bool value = true)
        {
            _state = value ? StepBlockState.Current : StepBlockState.Future;
            return this;
        }

        /// <summary>
        ///     Configures disabled behavior for the step component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder Disabled(bool value = true)
        {
            _state = value ? StepBlockState.Disabled : StepBlockState.Future;
            return this;
        }

        /// <summary>
        ///     Configures done behavior for the step component.
        /// </summary>
        /// <param name="value">True to enable the option; false to disable it.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder Done(bool value = true)
        {
            _state = value ? StepBlockState.Done : StepBlockState.Future;
            return this;
        }

        /// <summary>
        ///     Creates or renders the step component through the id helper.
        /// </summary>
        /// <param name="id">The HTML id or stable component id.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder Id(string id)
        {
            SetId(HtmlIdGenerator.CleanId(id) ?? string.Empty);
            return this;
        }

        /// <inheritdoc />
        protected override void InternalClone(StepBlockBuilder source)
        {
            base.InternalClone(source);

            _captureWriter = null;
            _originalWriter = null;

            _disposed = false;
            _started = false;

            _icon = source._icon;
            _asFieldset = source._asFieldset;
            _label = source._label;
            _state = source._state;
            _subtitle = source._subtitle;
            _title = source._title;
            _titleLevel = source._titleLevel;
            _variant = source._variant;
        }

        /// <inheritdoc />
        public override IHtmlContent Render()
        {
            throw new InvalidOperationException("StepBlockBuilder does not render directly. Use Begin()/Dispose() inside StepAreaBuilder.");
        }

        /// <summary>
        ///     Configures the state for the step component.
        /// </summary>
        /// <param name="state">The state value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder SetState(StepBlockState state)
        {
            _state = state;
            return this;
        }

        /// <summary>
        ///     Configures the variant for the step component.
        /// </summary>
        /// <param name="variant">The variant value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        /// <summary>
        ///     Configures the icon for the step component.
        /// </summary>
        /// <param name="icon">The icon value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder WithIcon(IconStruct icon)
        {
            _icon = icon;
            return this;
        }

        /// <summary>
        ///     Configures the label for the step component.
        /// </summary>
        /// <param name="label">The label value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder WithLabel(string? label)
        {
            _label = label;
            return this;
        }

        /// <summary>
        ///     Configures the step for the step component.
        /// </summary>
        /// <param name="step">The step value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder WithStep(int step)
        {
            _label = step.ToString();
            return this;
        }

        /// <summary>
        ///     Configures the subtitle for the step component.
        /// </summary>
        /// <param name="subtitle">The subtitle value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder WithSubtitle(string? subtitle)
        {
            _subtitle = subtitle;
            return this;
        }

        /// <summary>
        ///     Configures the title for the step component.
        /// </summary>
        /// <param name="title">The title value.</param>
        /// <param name="level">The level value.</param>
        /// <returns>The configured builder instance.</returns>
        public StepBlockBuilder WithTitle(string? title, TitleLevel level = TitleLevel.Three)
        {
            _title = title;
            _titleLevel = level;
            return this;
        }

        /// <inheritdoc />
        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            throw new InvalidOperationException("StepBlockBuilder does not render directly. Use Begin()/Dispose() inside StepAreaBuilder.");
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

            if (!_started)
            {
                return;
            }

            if (_originalWriter != null)
            {
                _htmlHelper.ViewContext.Writer = _originalWriter;
            }

            StepAreaBuilder? area = StepAreaBuilder.GetCurrent(_htmlHelper);
            if (area == null)
            {
                throw new InvalidOperationException("No active StepAreaBuilder context found while closing StepBlockBuilder.");
            }

            area.RegisterItem(new StepDefinition
            {
                Id = GetId(),
                Title = _title,
                TitleLevel = _titleLevel,
                Subtitle = _subtitle,
                Label = _label,
                Icon = _icon,
                Variant = _variant,
                State = _state,
                UseFieldset = _asFieldset,
                ContentHtml = _captureWriter?.ToString() ?? string.Empty
            });
        }

        #endregion

        #endregion
    }
}