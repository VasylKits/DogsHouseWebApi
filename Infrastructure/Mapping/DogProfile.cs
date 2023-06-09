using Application.Features.Dogs.Commands.Create;
using Application.Features.Dogs.Commands.Edit;
using Application.Features.Dogs.Queries.GetAll;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mapping;
public class DogProfile : Profile
{
    public DogProfile()
    {
        CreateMap<Dog, GetDogsResponse>()
            .ReverseMap();
        
        CreateMap<Dog, CreateDogResponse>()
            .ReverseMap();

        CreateMap<CreateDogCommand, Dog>()
            .ReverseMap();

        CreateMap<Dog, EditDogResponse>()
           .ReverseMap();

        CreateMap<EditDogCommand, Dog>()
            .ReverseMap();
    }
}