using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers
{
    public class ManagerInfoFullComparer : IEqualityComparer<ManagerInfo>
    {
        public bool Equals([AllowNull] ManagerInfo x, [AllowNull] ManagerInfo y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return
                x.UserId == y.UserId &&
                x.Description == y.Description &&
                x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] ManagerInfo obj)
        {
            return (obj.Description, obj.UserId).GetHashCode();
        }
    }
}
