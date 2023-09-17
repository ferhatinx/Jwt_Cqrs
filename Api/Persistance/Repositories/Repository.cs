using Api.Core.Application.Interfaces;
using Api.Core.Domain;
using Api.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly JwtContext _context;
        private readonly DbSet<T> _table;

        public Repository(JwtContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public Task<List<T>> GetAllAsync()  => _table.AsNoTracking().ToListAsync();

        public async Task<T?> GetByFilter(Expression<Func<T, bool>> filter)
        => await _table.AsNoTracking().SingleOrDefaultAsync(filter);

        public async Task<T?> GetByIdAsync(int id)
       => await _table.FindAsync(id);

        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
