using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class ManagerInfoIdComparer : IEqualityComparer<ManagerInfo>
    {
        public bool Equals([AllowNull] ManagerInfo x, [AllowNull] ManagerInfo y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] ManagerInfo obj)
        {
            return obj.UserId.GetHashCode();
        }
    }
}
