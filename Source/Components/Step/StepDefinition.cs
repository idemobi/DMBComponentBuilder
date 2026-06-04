#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using DMBBootstrapBuilder;
using DMBPageBuilder;

#endregion

namespace DMBComponentBuilder
{
    internal sealed class StepDefinition
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets content html used by step component rendering.
        /// </summary>
        public string? ContentHtml { get; set; }

        /// <summary>
        ///     Gets or sets icon used by step component rendering.
        /// </summary>
        public IconStruct Icon { get; set; } = IconStruct.Empty;

        /// <summary>
        ///     Gets or sets id used by step component rendering.
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        ///     Gets or sets label used by step component rendering.
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        ///     Gets or sets state used by step component rendering.
        /// </summary>
        public StepBlockState State { get; set; } = StepBlockState.Future;

        /// <summary>
        ///     Gets or sets subtitle used by step component rendering.
        /// </summary>
        public string? Subtitle { get; set; }

        /// <summary>
        ///     Gets or sets title used by step component rendering.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        ///     Gets or sets title level used by step component rendering.
        /// </summary>
        public TitleLevel TitleLevel { get; set; } = TitleLevel.Three;

        /// <summary>
        ///     Gets or sets use fieldset used by step component rendering.
        /// </summary>
        public bool UseFieldset { get; set; }

        /// <summary>
        ///     Gets or sets variant used by step component rendering.
        /// </summary>
        public VariantStyle Variant { get; set; } = VariantStyle.Primary;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Creates a copy of the current step definition.
        /// </summary>
        /// <returns>The generated step value.</returns>
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

        #endregion
    }
}