using Microsoft.EntityFrameworkCore;

namespace Repository.Config.Db
{
    public static class DbSetExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this DbSet<T> dbSet) where T : class
        {
            var query = dbSet.AsQueryable();

            var navigationProperties = typeof(T).GetProperties()
                .Where(p => (p.PropertyType.IsGenericType && typeof(IEnumerable<>)
                    .IsAssignableFrom(p.PropertyType.GetGenericTypeDefinition()))
                    || (!p.PropertyType.IsValueType && p.PropertyType != typeof(string)));

            foreach (var property in navigationProperties)
            {
                query = query.Include(property.Name);
            }

            return query;
        }
    }
}