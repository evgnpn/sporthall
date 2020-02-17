using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class UserIdComparer : IEqualityComparer<User>
    {
        public bool Equals([AllowNull] User x, [AllowNull] User y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
