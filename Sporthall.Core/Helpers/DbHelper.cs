using System.Collections.Generic;
using System.Linq;

namespace Sporthall.Core.Helpers
{
    public class EntitiesDiff<TEntity>
    {
        public IEnumerable<TEntity> ToUpdateEntities { get; set; }
        public IEnumerable<TEntity> ToRemoveEntities { get; set; }
        public IEnumerable<TEntity> ToAddEntities { get; set; }

        public bool HasToUpdateEntities =>
            ToUpdateEntities != null && ToUpdateEntities.Count() > 0;

        public bool HasToRemoveEntities =>
            ToRemoveEntities != null && ToRemoveEntities.Count() > 0;

        public bool HasToAddEntities =>
            ToAddEntities != null && ToAddEntities.Count() > 0;
    }

    public static class DbHelper
    {
        public static EntitiesDiff<TEntity> EntitiesDiff<TEntity>(
            IEnumerable<TEntity> existEntities,
            IEnumerable<TEntity> toSetEntities,
            IEqualityComparer<TEntity> fullComparer,
            IEqualityComparer<TEntity> IdComparer
        )
        {
            EntitiesDiff<TEntity> diff = EntitiesDiff(existEntities, toSetEntities, IdComparer);
            diff.ToUpdateEntities = toSetEntities.Intersect(existEntities, IdComparer).
                Except(existEntities, fullComparer);

            return diff;
        }

        public static EntitiesDiff<TEntity> EntitiesDiff<TEntity>(
           IEnumerable<TEntity> existEntities,
           IEnumerable<TEntity> toSetEntities,
           IEqualityComparer<TEntity> IdComparer
       )
        {
            EntitiesDiff<TEntity> diff = new EntitiesDiff<TEntity>
            {
                ToAddEntities = toSetEntities.Except(existEntities, IdComparer),
                ToRemoveEntities = existEntities.Except(toSetEntities, IdComparer)
            };

            return diff;
        }
    }
}
