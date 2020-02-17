using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers
{
    public class HallScheduleFullComparer : IEqualityComparer<HallSchedule>
    {
        public bool Equals([AllowNull] HallSchedule x, [AllowNull] HallSchedule y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return
                x.HallId == y.HallId &&
                x.DayOfWeek == y.DayOfWeek &&
                x.BeginTime == y.BeginTime &&
                x.EndTime == y.EndTime;
        }

        public int GetHashCode([DisallowNull] HallSchedule obj)
        {
            return (obj.HallId, obj.DayOfWeek, obj.BeginTime, obj.EndTime).GetHashCode();
        }
    }
}
