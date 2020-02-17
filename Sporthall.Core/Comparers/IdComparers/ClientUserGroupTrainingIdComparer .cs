using Sporthall.Core.Entities.Joins;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Sporthall.Core.Comparers.IdComparers
{
    public class ClientUserGroupTrainingIdComparer : IEqualityComparer<ClientUserGroupTraining>
    {
        public bool Equals([AllowNull] ClientUserGroupTraining x, [AllowNull] ClientUserGroupTraining y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.ClientUserId == y.ClientUserId && x.GroupTrainingId == y.GroupTrainingId;
        }

        public int GetHashCode([DisallowNull] ClientUserGroupTraining obj)
        {
            return (obj.ClientUserId, obj.GroupTrainingId).GetHashCode();
        }
    }
}
