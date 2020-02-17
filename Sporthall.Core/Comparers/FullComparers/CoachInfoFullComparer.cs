using Sporthall.Core.Entities;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers
{
    public class CoachInfoFullComparer : IEqualityComparer<CoachInfo>
    {
        public bool Equals([AllowNull] CoachInfo x, [AllowNull] CoachInfo y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return
                x.UserId == y.UserId &&
                x.Description == y.Description &&
                x.Speciality == y.Speciality &&
                x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] CoachInfo obj)
        {
            return (obj.UserId, obj.Description, obj.Speciality, obj.UserId).GetHashCode();
        }
    }
}
