using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers
{
    public class HallScheduleDayComparer : IEqualityComparer<HallSchedule>
    {
        public bool Equals([AllowNull] HallSchedule x, [AllowNull] HallSchedule y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.DayOfWeek == y.DayOfWeek;
        }

        public int GetHashCode([DisallowNull] HallSchedule obj)
        {
            return obj.DayOfWeek.GetHashCode();
        }
    }
}
