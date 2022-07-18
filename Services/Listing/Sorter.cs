using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Services.Listing
{
    internal class Sorter<TEntity>
    {
        public static IQueryable<TEntity> Sort(IQueryable<TEntity> entities, List<KeyValuePair<string, string>> sort)
        {
            List<KeyValuePair<string, string>> tempSort = new List<KeyValuePair<string, string>>();
            bool IsFirst = true;
            for (int i = 0; i < sort.Count; i++)
            {
                if (!String.IsNullOrEmpty(sort[i].Key))
                {
                    PropertyInfo prop = typeof(TEntity).GetProperty(sort[i].Key);
                    if (prop != null)
                    {
                        if (IsFirst)
                        {
                            entities = OrderBy(entities, sort[i], i);
                            IsFirst = false;
                        }
                        else
                        {
                            entities = ThenBy((IOrderedQueryable<TEntity>)entities, sort[i], i);
                        }
                        tempSort.Add(sort[i]);
                    }
                }
            }

            sort = tempSort;

            return entities;
        }

        private static IQueryable<TEntity> OrderBy(IQueryable<TEntity> entities, KeyValuePair<string, string> sort, int index)
        {
            if (sort.Value.ToUpper() == "DESC")
            {
                entities = entities.OrderByDescending(u => EF.Property<object>(u, sort.Key));
            }
            else
            {
                entities = entities.OrderBy(u => EF.Property<object>(u, sort.Key));
            }

            return entities;
        }

        private static IQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> entities, KeyValuePair<string, string> sort, int index)
        {
            if (sort.Value.ToUpper() == "DESC")
            {
                entities = entities.ThenByDescending(u => EF.Property<object>(u, sort.Key));
            }
            else
            {
                entities = entities.ThenBy(u => EF.Property<object>(u, sort.Key));
            }

            return entities;
        }
    }
}