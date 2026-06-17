#region Copyright

// ©2002-2026 idéMobi
// www.idemobi.com

#endregion

#region

using System.Collections.Concurrent;

#endregion

namespace DMBComponentBuilder
{
    /// <summary>
    ///     Aggregates festival definitions from registered injectors and returns normalized festival lists.
    /// </summary>
    public static class FestivalManager
    {
        #region Static fields and properties

        private static readonly ConcurrentDictionary<int, List<GDFFestival>> FestivalCache = new();

        /// <summary>
        ///     Gets the registered festival injectors.
        /// </summary>
        public static List<IFestivalInjector> InjectorList { get; } = new();

        #endregion

        #region Static methods

        /// <summary>
        ///     Gets the festivals active around the requested date.
        /// </summary>
        /// <param name="date">The date used to select active festivals.</param>
        /// <param name="delayBefore">The number of days before the festival start date to include it.</param>
        /// <param name="delayAfter">The number of days after the festival end date to include it.</param>
        /// <returns>The active festivals ordered by start date and name.</returns>
        public static List<GDFFestival> GetFestivalsForDate(DateTime date, int delayBefore = 6, int delayAfter = 10)
        {
            DateTime day = date.Date;
            List<GDFFestival> festivals = GetAllFestivals(day.Year);

            return festivals
                .Where(festival => day >= festival.StartDate.Date.AddDays(-delayBefore) && day <= festival.EndDate.Date.AddDays(delayAfter))
                .OrderBy(festival => festival.StartDate)
                .ThenBy(festival => festival.Name)
                .ToList();
        }

        /// <summary>
        ///     Gets all festivals for the requested year.
        /// </summary>
        /// <param name="year">The year used to build festival definitions.</param>
        /// <returns>The normalized festival definitions ordered with upcoming dates first.</returns>
        public static List<GDFFestival> GetAllFestivals(int year)
        {
            DateTime today = DateTime.Now.Date;
            List<GDFFestival> festivals = FestivalCache.GetOrAdd(year, BuildFromInjectors);

            return festivals
                .OrderBy(festival => festival.StartDate < today)
                .ThenBy(festival => festival.StartDate)
                .ThenBy(festival => festival.Name)
                .ToList();
        }

        private static List<GDFFestival> BuildFromInjectors(int year)
        {
            List<GDFFestival> all = InjectorList
                .SelectMany(injector => injector.BuildFestivalList(year) ?? Enumerable.Empty<GDFFestival>())
                .Where(festival => festival is not null)
                .Select(NormalizeFestival)
                .ToList();

            return all
                .GroupBy(festival => (festival.ViewName ?? string.Empty).Trim(), StringComparer.OrdinalIgnoreCase)
                .Select(MergeFestivalsGroup)
                .OrderBy(festival => festival.StartDate)
                .ThenBy(festival => festival.Name, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static GDFFestival NormalizeFestival(GDFFestival festival)
        {
            string name = (festival.Name ?? string.Empty).Trim();
            string view = (festival.ViewName ?? string.Empty).Trim();

            DateTime start = festival.StartDate;
            DateTime end = festival.EndDate;

            if (end < start)
            {
                (start, end) = (end, start);
            }

            start = DateTime.SpecifyKind(start, DateTimeKind.Unspecified);
            end = DateTime.SpecifyKind(end, DateTimeKind.Unspecified);

            return new GDFFestival
            {
                Name = name,
                ViewName = view,
                StartDate = start,
                EndDate = end
            };
        }

        private static GDFFestival MergeFestivalsGroup(IGrouping<string, GDFFestival> group)
        {
            List<GDFFestival> list = group.ToList();
            string name = list.Select(festival => festival.Name).FirstOrDefault(value => !string.IsNullOrWhiteSpace(value)) ?? group.Key;

            return new GDFFestival
            {
                Name = name,
                ViewName = group.Key,
                StartDate = list.Min(festival => festival.StartDate),
                EndDate = list.Max(festival => festival.EndDate)
            };
        }

        #endregion
    }
}
