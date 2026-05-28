#region Copyright

// Game-Data-Forge Solution
// Written by CONTART Jean-François & BOULOGNE Quentin
// DMBComponentBuilder.csproj GDFFestival.cs create at 2026/05/11
// ©2024-2026 idéMobi SARL FRANCE

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    /// Represents gdf festival data used by festival components.
    /// </summary>
    [Serializable]
    public class GDFFestival
    {
        /// <summary>
        /// Gets or sets name used by festival component rendering.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets view name used by festival component rendering.
        /// </summary>
        public string ViewName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets start date used by festival component rendering.
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets end date used by festival component rendering.
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
