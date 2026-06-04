#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

using System;

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Represents gdf festival data used by festival components.
    /// </summary>
    [Serializable]
    public class GDFFestival
    {
        #region Instance fields and properties

        /// <summary>
        ///     Gets or sets end date used by festival component rendering.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        ///     Gets or sets name used by festival component rendering.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets start date used by festival component rendering.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     Gets or sets view name used by festival component rendering.
        /// </summary>
        public string ViewName { get; set; } = string.Empty;

        #endregion
    }
}