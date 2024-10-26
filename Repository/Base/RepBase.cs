using Domain.Base;
using Microsoft.EntityFrameworkCore;
using Repository.Config.Db;

namespace Repository.Base
{
    public class RepBase<T>(DbContext contexto) : IRepBase<T> where T : IdBase
    {
        public async Task<List<T>> FindAllAsync() => await contexto.Set<T>().IncludeAll().ToListAsync();

        public async Task<T?> FindByCodigoAsync(int id) => await contexto.Set<T>().IncludeAll().FirstOrDefaultAsync(x => x.Id == id);

        public async Task SaveChangesAsync(T entity)
        {
            _ = contexto.Set<T>().Update(entity);
            _ = await contexto.SaveChangesAsync();
        }

        public async Task SaveChangesRangeAsync(IEnumerable<T> entities)
        {
            contexto.Set<T>().UpdateRange(entities);
            _ = await contexto.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await contexto.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _ = contexto.Set<T>().Remove(entity);
                _ = await contexto.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdsAsync(List<int> ids)
        {
            List<T> entities = [.. contexto.Set<T>().Where(x => ids.Contains(x.Id))];
            if (entities.Count > 0)
            {
                contexto.Set<T>().RemoveRange(entities);
                _ = await contexto.SaveChangesAsync();
            }
        }
    }
}