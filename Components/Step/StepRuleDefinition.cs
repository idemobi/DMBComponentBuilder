namespace DMBComponentBuilder
{
    internal sealed class StepRuleDefinition
    {
        public string TargetStepId { get; set; } = string.Empty;
        public string FieldId { get; set; } = string.Empty;
        public List<string> FieldIds { get; set; } = new();
        public string Condition { get; set; } = string.Empty;
        public string ActiveState { get; set; } = "current";
        public string InactiveState { get; set; } = "disabled";

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
    }
}
