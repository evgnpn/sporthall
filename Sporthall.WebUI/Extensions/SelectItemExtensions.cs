using System;
using System.Collections.Generic;
using System.Linq;

namespace Sporthall.WebUI.Extensions
{
    public static class SelectItemExtensions
    {
        public static IEnumerable<SelectItem<T>> AsSelectItems<T>(this IEnumerable<T> list)
        {
            return list?.Select(a => new SelectItem<T> { Model = a });
        }

        public static IEnumerable<SelectItem<T>> AsSelectItems<T>(this IEnumerable<T> list, IEnumerable<T> selectedList, IEqualityComparer<T> comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (selectedList is null)
            {
                return AsSelectItems(list);
            }

            return list?.Select(a => new SelectItem<T> { Model = a, Selected = selectedList.Contains(a, comparer) });
        }

        public static IEnumerable<T> GetSelectedModels<T>(this IEnumerable<SelectItem<T>> selectItems)
        {
            return selectItems?.Where(a => a.Selected).Select(a => a.Model);
        }
    }
}
