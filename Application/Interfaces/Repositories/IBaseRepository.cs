namespace Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    IQueryable<T> Entities { get; }
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}