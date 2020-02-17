using Sporthall.Core.Entities.Joins;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class ManagerUserHallIdComparer : IEqualityComparer<ManagerUserHall>
    {
        public bool Equals([AllowNull] ManagerUserHall x, [AllowNull] ManagerUserHall y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.ManagerUserId == y.ManagerUserId && x.HallId == y.HallId;
        }

        public int GetHashCode([DisallowNull] ManagerUserHall obj)
        {
            return (obj.ManagerUserId, obj.HallId).GetHashCode();
        }
    }
}
