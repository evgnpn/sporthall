using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class HallIdComparer : IEqualityComparer<Hall>
    {
        public bool Equals([AllowNull] Hall x, [AllowNull] Hall y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] Hall obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
