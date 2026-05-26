using DMBBootstrapBuilder;
using DMBPageBuilder;

namespace DMBComponentBuilder
{
    internal sealed class CheckoutProgressDefinition
    {
        public string? Id { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Label { get; set; }
        public IconStruct Icon { get; set; } = IconStruct.Empty;
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;
        public CheckoutProgressState State { get; set; } = CheckoutProgressState.Inactive;
        public string? CssClass { get; set; }

        public CheckoutProgressDefinition Clone()
        {
            return new CheckoutProgressDefinition
            {
                Id = Id,
                Title = Title,
                Subtitle = Subtitle,
                Label = Label,
                Icon = Icon,
                Variant = Variant,
                State = State,
                CssClass = CssClass
            };
        }
    }
}
