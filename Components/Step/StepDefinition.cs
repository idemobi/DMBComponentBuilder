using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBComponentBuilder
{
    internal sealed class StepDefinition
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public TitleLevel TitleLevel { get; set; } = TitleLevel.Three;
        public string? Subtitle { get; set; }
        public string? Label { get; set; }
        public IconStruct Icon { get; set; } = IconStruct.Empty;
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;
        public StepBlockState State { get; set; } = StepBlockState.Future;
        public bool UseFieldset { get; set; }
        public string? ContentHtml { get; set; }

        public StepDefinition Clone()
        {
            return new StepDefinition
            {
                Id = Id,
                Title = Title,
                TitleLevel = TitleLevel,
                Subtitle = Subtitle,
                Label = Label,
                Icon = Icon,
                Variant = Variant,
                State = State,
                UseFieldset = UseFieldset,
                ContentHtml = ContentHtml
            };
        }
    }
}
