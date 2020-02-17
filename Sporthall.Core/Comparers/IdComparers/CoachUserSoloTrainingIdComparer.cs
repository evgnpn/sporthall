using Sporthall.Core.Entities.Joins;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class CoachUserSoloTrainingIdComparer : IEqualityComparer<CoachUserSoloTraining>
    {
        public bool Equals([AllowNull] CoachUserSoloTraining x, [AllowNull] CoachUserSoloTraining y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.CoachUserId == y.CoachUserId && x.SoloTrainingId == y.SoloTrainingId;
        }

        public int GetHashCode([DisallowNull] CoachUserSoloTraining obj)
        {
            return (obj.CoachUserId, obj.SoloTrainingId).GetHashCode();
        }
    }
}
