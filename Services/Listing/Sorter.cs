using Microsoft.EntityFrameworkCore;

namespace Services.Listing
{
    internal class Sorter<TEntity>
    {
        public static IQueryable<TEntity> Sort(IQueryable<TEntity> entities, List<KeyValuePair<string, string>> sort)
        {
            for (int i = 0; i < sort.Count; i++)
            {
                if (!String.IsNullOrEmpty(sort[i].Key))
                {
                    if (i == 0)
                    {
                        entities = OrderByUser(entities, sort[i], i);
                    }
                    else
                    {
                        entities = ThenByUser((IOrderedQueryable<TEntity>)entities, sort[i], i);
                    }
                }
            }

            return entities;
        }

        public static IQueryable<TEntity> OrderByUser(IQueryable<TEntity> entities, KeyValuePair<string, string> sort, int index)
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

        public static IQueryable<TEntity> ThenByUser(IOrderedQueryable<TEntity> entities, KeyValuePair<string, string> sort, int index)
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