using Application.Features.Dogs.Queries.GetAll;
using Domain.Entities;
using Shared.Common.Models.Result.Abstractions;
using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations;

namespace Application.Interfaces;

public interface IDogsService
{
    IQueryable<Dog> GetDogs();
    Task<IResult<PaginatedList<GetDogsResponse>>> GetAll(GetAllDogsQuery request);
    Task<IResult<Dog>> GetByIdAsync(int id);
    Task<IResult<Dog>> GetByNameAsync(string name);
    Task<IResult<Dog>> AddDogAsync(Dog dog);
    Task<IResult<Dog>> UpdateDogAsync(Dog dog);
    Task<IResult> DeleteDogAsync(int id);
}