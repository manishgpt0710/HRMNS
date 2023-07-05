using System.Linq.Expressions;
using HRMS.Data;
using Microsoft.EntityFrameworkCore;
using HRMS.Data.Entities;

namespace HRMS.Repository
{

    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id, bool includeChildren = false);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }

    public partial class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly HRMSDbContext context;
        private readonly DbSet<T> entities;

        public GenericRepository(HRMSDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await entities.AsNoTracking().Where(expression).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id, bool includeChildren = false)
        {
            return await entities.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}