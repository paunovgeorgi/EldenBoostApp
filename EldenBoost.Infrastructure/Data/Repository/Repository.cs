using EldenBoost.Data;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Infrastructure.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly EldenBoostDbContext context;

        public Repository(EldenBoostDbContext _context)
        {
            context = _context;
        }
        private DbSet<T> DbSet<T>() where T : class
        {
            return context.Set<T>();
        }
        public IQueryable<T> All<T>() where T : class
        {
            return DbSet<T>();
        }

        public IQueryable<T> AllReadOnly<T>() where T : class
        {
            return DbSet<T>().AsNoTracking();
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            await DbSet<T>().AddAsync(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync<T>(object id) where T : class
        {
            return await DbSet<T>().FindAsync(id);
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            DbSet<T>().Remove(entity);

            await context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync<T>(IQueryable<T> entities) where T : class
        {
            DbSet<T>().RemoveRange(entities);
            await context.SaveChangesAsync();
        }
    }
}
