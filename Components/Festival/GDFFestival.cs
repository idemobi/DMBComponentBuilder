#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj GDFFestival.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBComponentBuilder
{
    [Serializable]
    public class GDFFestival
    {
        public string Name { get; set; } = string.Empty;
        public string ViewName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
