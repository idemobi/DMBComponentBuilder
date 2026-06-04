#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System.Collections.Generic;

namespace DMBComponentBuilder
{
    internal sealed class StepRuleDefinition
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets active state used by step component rendering.
        /// </summary>
        public string ActiveState { get; set; } = "current";

        /// <summary>
        ///     Gets or sets condition used by step component rendering.
        /// </summary>
        public string Condition { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets field id used by step component rendering.
        /// </summary>
        public string FieldId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the field ids that must satisfy the rule condition.
        /// </summary>
        public List<string> FieldIds { get; set; } = new();

        /// <summary>
        ///     Gets or sets inactive state used by step component rendering.
        /// </summary>
        public string InactiveState { get; set; } = "disabled";

        /// <summary>
        ///     Gets or sets target step id used by step component rendering.
        /// </summary>
        public string TargetStepId { get; set; } = string.Empty;

        #endregion

        #region Instance methods

        /// <summary>
        ///     Creates a copy of the current step definition.
        /// </summary>
        /// <returns>The generated step value.</returns>
        public StepRuleDefinition Clone()
        {
            return new StepRuleDefinition
            {
                TargetStepId = TargetStepId,
                FieldId = FieldId,
                FieldIds = new List<string>(FieldIds),
                Condition = Condition,
                ActiveState = ActiveState,
                InactiveState = InactiveState
            };
        }

        #endregion
    }
}