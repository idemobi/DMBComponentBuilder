using System.Text.Encodings.Web;
using DMBBootstrapBuilder;
using DMBPageBuilder;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DMBComponentBuilder
{
    public sealed class StepBlockBuilder :
        HtmlBuilderBase<StepBlockBuilder>,
        IDisposable
    {
        private StringWriter? _captureWriter;
        private TextWriter? _originalWriter;

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

        private bool _asFieldset
        {
            get => GetInternal("_asFieldset", false);
            set => SetInternal("_asFieldset", value);
        }

        private string? _label
        {
            get => GetInternal<string?>("_label", null);
            set => SetInternal("_label", value);
        }

        private StepBlockState _state
        {
            get => GetInternal("_state", StepBlockState.Future);
            set => SetInternal("_state", value);
        }

        private bool _started
        {
            get => GetInternal("_started", false);
            set => SetInternal("_started", value);
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

        public StepBlockBuilder(TextWriter writer, IHtmlHelper html)
            : base(writer, html)
        {
            _tag = "section";
            SetData("step-block", "true");
        }

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

        public StepBlockBuilder Id(string id)
        {
            SetId(HtmlIdGenerator.CleanId(id) ?? string.Empty);
            return this;
        }

        public StepBlockBuilder AsFieldset(bool value = true)
        {
            _asFieldset = value;
            return this;
        }

        public StepBlockBuilder WithTitle(string? title, TitleLevel level = TitleLevel.Three)
        {
            _title = title;
            _titleLevel = level;
            return this;
        }

        public StepBlockBuilder WithSubtitle(string? subtitle)
        {
            _subtitle = subtitle;
            return this;
        }

        public StepBlockBuilder WithLabel(string? label)
        {
            _label = label;
            return this;
        }

        public StepBlockBuilder WithStep(int step)
        {
            _label = step.ToString();
            return this;
        }

        public StepBlockBuilder WithIcon(IconStruct icon)
        {
            _icon = icon;
            return this;
        }

        public StepBlockBuilder SetState(StepBlockState state)
        {
            _state = state;
            return this;
        }

        public StepBlockBuilder Current(bool value = true)
        {
            _state = value ? StepBlockState.Current : StepBlockState.Future;
            return this;
        }

        public StepBlockBuilder Done(bool value = true)
        {
            _state = value ? StepBlockState.Done : StepBlockState.Future;
            return this;
        }

        public StepBlockBuilder Disabled(bool value = true)
        {
            _state = value ? StepBlockState.Disabled : StepBlockState.Future;
            return this;
        }

        public StepBlockBuilder SetVariant(VariantStyle variant)
        {
            _variant = variant;
            return this;
        }

        protected override StepBlockBuilder CreateInstance()
        {
            return new StepBlockBuilder(_textWriter, _htmlHelper);
        }

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

        public override IHtmlContent Render()
        {
            throw new InvalidOperationException("StepBlockBuilder does not render directly. Use Begin()/Dispose() inside StepAreaBuilder.");
        }

        protected override void WriteToCore(TextWriter writer, HtmlEncoder encoder)
        {
            throw new InvalidOperationException("StepBlockBuilder does not render directly. Use Begin()/Dispose() inside StepAreaBuilder.");
        }

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
    }
}
