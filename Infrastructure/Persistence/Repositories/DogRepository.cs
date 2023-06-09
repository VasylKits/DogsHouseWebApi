using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class DogRepository : IDogRepository
{
    private readonly DataContext _context;

    public DogRepository(DataContext context)
    {
        _context = context;
    }

    public IQueryable<Dog> Entities => _context.Dogs.AsQueryable();

    public async Task<Dog> GetByIdAsync(int id)
    {
        var dog = await Entities.FirstOrDefaultAsync(x => x.Id == id);

        return dog;
    }

    public async Task<Dog> GetByNameAsync(string name)
    {
        var dog = await Entities.FirstOrDefaultAsync(x => x.Name.Equals(name));

        return dog;
    }

    public async Task<Dog> CreateAsync(Dog dog)
    {
        await _context.Dogs.AddAsync(dog);
        await Save();

        return dog;
    }
    public async Task<Dog> UpdateAsync(Dog dog)
    {
        _context.Dogs.Update(dog);
        await Save();

        return dog;
    }

    public async Task DeleteAsync(Dog dog)
    {
        _context.Dogs.Remove(dog);
        await Save();
    }
    
    private async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}