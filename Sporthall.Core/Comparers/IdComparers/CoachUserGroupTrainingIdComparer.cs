using Sporthall.Core.Entities.Joins;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class CoachUserGroupTrainingIdComparer : IEqualityComparer<CoachUserGroupTraining>
    {
        public bool Equals([AllowNull] CoachUserGroupTraining x, [AllowNull] CoachUserGroupTraining y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.CoachUserId == y.CoachUserId && x.GroupTrainingId == y.GroupTrainingId;
        }

        public int GetHashCode([DisallowNull] CoachUserGroupTraining obj)
        {
            return (obj.CoachUserId, obj.GroupTrainingId).GetHashCode();
        }
    }
}
