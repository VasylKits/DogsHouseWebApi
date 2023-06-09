using Application.Features.Dogs.Queries.GetAll;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Common.Constants;
using Shared.Common.Models.Result.Abstractions;
using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations;
using Shared.Common.Models.Result.Implementations.Generics;

namespace Infrastructure.Persistence.Services;

public class DogsService : IDogsService
{
    private readonly IDogRepository _dogRepository;
    private readonly IMapper _mapper;

    public DogsService(
        IDogRepository dogRepository,
        IMapper mapper)
    {
        _dogRepository = dogRepository;
        _mapper = mapper;
    }

    public IQueryable<Dog> GetDogs() => _dogRepository.Entities;

    public async Task<IResult<PaginatedList<GetDogsResponse>>> GetAll(GetAllDogsQuery request)
    {
        try
        {
            var dogs = _dogRepository.Entities.AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var search = request.Search.ToLower().Trim();

                dogs = dogs.Where(x => x.Name.ToLower().Contains(search) ||
                                          x.Color.ToLower().Contains(search) ||
                                         x.Weight.ToString().Contains(search) ||
                                          x.TailLength.ToString().Contains(search));
            }

            if (!string.IsNullOrEmpty(request.Attribute))
            {
                switch (request.Attribute.ToLower())
                {
                    case "id":
                        dogs = request.Order.ToLower() == "desc" ? dogs.OrderByDescending(d => d.Id) : dogs.OrderBy(d => d.Id);
                        break;
                    case "name":
                        dogs = request.Order.ToLower() == "desc" ? dogs.OrderByDescending(d => d.Name) : dogs.OrderBy(d => d.Name);
                        break;
                    case "color":
                        dogs = request.Order.ToLower() == "desc" ? dogs.OrderByDescending(d => d.Color) : dogs.OrderBy(d => d.Color);
                        break;
                    case "tail_length":
                        dogs = request.Order.ToLower() == "desc" ? dogs.OrderByDescending(d => d.TailLength) : dogs.OrderBy(d => d.TailLength);
                        break;
                    case "weight":
                        dogs = request.Order.ToLower() == "desc" ? dogs.OrderByDescending(d => d.Weight) : dogs.OrderBy(d => d.Weight);
                        break;
                    default:
                        break;
                }
            }

            var result = await PaginatedList<GetDogsResponse>.CreateAsync(_mapper, dogs, request.PageNumber, request.PageSize);

            return Result<PaginatedList<GetDogsResponse>>.CreateSuccess(result);
        }
        catch (Exception e)
        {
            return Result<PaginatedList<GetDogsResponse>>.CreateFailed(e.Message);
        }
    }

    public async Task<IResult<Dog>> GetByIdAsync(int id)
    {
        try
        {
            var dog = await _dogRepository.GetByIdAsync(id);

            if (dog is null)
                return Result<Dog>.CreateFailed(ErrorModel.DogNotFound);

            return Result<Dog>.CreateSuccess(dog);
        }
        catch (Exception e)
        {
            return Result<Dog>.CreateFailed(e.Message);
        }
    }

    public async Task<IResult<Dog>> GetByNameAsync(string name)
    {
        try
        {
            var dog = await _dogRepository.GetByNameAsync(name);

            if (dog is null)
                return Result<Dog>.CreateFailed(ErrorModel.DogNotFound);

            return Result<Dog>.CreateSuccess(dog);
        }
        catch (Exception e)
        {
            return Result<Dog>.CreateFailed(e.Message);
        }
    }

    public async Task<IResult<Dog>> AddDogAsync(Dog createDog)
    {
        try
        {
            var dog = await _dogRepository.CreateAsync(createDog);

            return dog is null
                ? Result<Dog>.CreateFailed(ErrorModel.DogIsNotCreated)
                : Result<Dog>.CreateSuccess(dog);

        }
        catch (Exception e)
        {
            return Result<Dog>.CreateFailed(e.Message);
        }
    }

    public async Task<IResult<Dog>> UpdateDogAsync(Dog updateDog)
    {
        try
        {
            var dog = await _dogRepository.GetByIdAsync(updateDog.Id);

            if (dog is null)
                return Result<Dog>.CreateFailed(ErrorModel.DogNotFound);

            dog.Name = updateDog.Name;
            dog.Color = updateDog.Color;
            dog.TailLength = updateDog.TailLength;
            dog.Weight = updateDog.Weight;

            var result = await _dogRepository.UpdateAsync(dog);

            return result is null
                ? Result<Dog>.CreateFailed(ErrorModel.DogIsNotUpdated)
                : Result<Dog>.CreateSuccess(result);
        }
        catch (Exception e)
        {
            return Result<Dog>.CreateFailed(e.Message);
        }
    }

    public async Task<IResult> DeleteDogAsync(int id)
    {
        try
        {
            var dogResult = await _dogRepository.GetByIdAsync(id);

            if (dogResult is null)
                return Result.CreateFailed(ErrorModel.DogNotFound);

            await _dogRepository.DeleteAsync(dogResult);

            return Result.CreateSuccess();
        }
        catch (Exception e)
        {
            return Result.CreateFailed(e.Message);
        }
    }
}