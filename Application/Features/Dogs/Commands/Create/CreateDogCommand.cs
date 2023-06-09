using Shared.Common.Constants;
using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations.Generics;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Features.Dogs.Commands.Create;

public class CreateDogCommand : IRequest<IResult<CreateDogResponse>>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    [JsonPropertyName("tail_length")]
    public int TailLength { get; set; }
    [Required]
    public int Weight { get; set; }

    internal class CreateDogCommandHandler : IRequestHandler<CreateDogCommand, IResult<CreateDogResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDogsService _dogsService;

        public CreateDogCommandHandler(
            IMapper mapper,
            IDogsService dogsService)
        {
            _mapper = mapper;
            _dogsService = dogsService;
        }

        public async Task<IResult<CreateDogResponse>> Handle(CreateDogCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingDog = await _dogsService.GetByNameAsync(command.Name);

                if (existingDog.Success)
                    return Result<CreateDogResponse>.CreateFailed(ErrorModel.NameAlreadyExists);

                var result = await _dogsService.AddDogAsync(_mapper.Map<Dog>(command));

                if (!result.Success)
                    return Result<CreateDogResponse>.CreateFailed(ErrorModel.DogIsNotCreated);

                var dogResponse = _mapper.Map<CreateDogResponse>(result.Data);

                return Result<CreateDogResponse>.CreateSuccess(dogResponse);                   
            }
            catch (Exception e)
            {
                return Result<CreateDogResponse>.CreateFailed(e.Message);
            }
        }
    }
}