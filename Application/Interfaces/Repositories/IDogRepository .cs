using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface IDogRepository : IBaseRepository<Dog>
{
    Task<Dog> GetByNameAsync(string name);    
}