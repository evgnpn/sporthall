using Sporthall.Core.Entities.Joins;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class CoachUserHallIdComparer : IEqualityComparer<CoachUserHall>
    {
        public bool Equals([AllowNull] CoachUserHall x, [AllowNull] CoachUserHall y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.CoachUserId == y.CoachUserId && x.HallId == y.HallId;
        }

        public int GetHashCode([DisallowNull] CoachUserHall obj)
        {
            return (obj.CoachUserId, obj.HallId).GetHashCode();
        }
    }
}
