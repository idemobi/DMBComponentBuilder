#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Defines a contract for providers that build festival definitions for a specific year.
    /// </summary>
    public interface IFestivalInjector
    {
        #region Instance methods

        /// <summary>
        ///     Builds the festival definitions for the requested year.
        /// </summary>
        /// <param name="year">The year used to compute fixed and movable festival dates.</param>
        /// <returns>The festival definitions available for the requested year.</returns>
        public List<GDFFestival> BuildFestivalList(int year);

        #endregion
    }
}
