using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

internal class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DataContext _context;

    public BaseRepository(DataContext context)
    {
        _context = context;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        T exist = _context.Set<T>().Find(entity);
        _context.Entry(exist).CurrentValues.SetValues(entity);

        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);

        return Task.CompletedTask;
    }
}