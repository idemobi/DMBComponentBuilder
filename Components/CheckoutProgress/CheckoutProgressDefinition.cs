#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    internal sealed class CheckoutProgressDefinition
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets css class used by checkout progress component rendering.
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        ///     Gets or sets icon used by checkout progress component rendering.
        /// </summary>
        public IconStruct Icon { get; set; } = IconStruct.Empty;

        /// <summary>
        ///     Gets or sets id used by checkout progress component rendering.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        ///     Gets or sets label used by checkout progress component rendering.
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        ///     Gets or sets state used by checkout progress component rendering.
        /// </summary>
        public CheckoutProgressState State { get; set; } = CheckoutProgressState.Inactive;

        /// <summary>
        ///     Gets or sets subtitle used by checkout progress component rendering.
        /// </summary>
        public string? Subtitle { get; set; }

        /// <summary>
        ///     Gets or sets title used by checkout progress component rendering.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Gets or sets variant used by checkout progress component rendering.
        /// </summary>
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Creates a copy of the current checkout progress definition.
        /// </summary>
        /// <returns>The generated checkout progress value.</returns>
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

        #endregion
    }
}