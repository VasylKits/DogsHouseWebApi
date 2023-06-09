using Shared.Common.Constants;
using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations.Generics;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Features.Dogs.Commands.Edit;

public class EditDogCommand : IRequest<IResult<EditDogResponse>>
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Color { get; set; }
    [Required]
    [JsonPropertyName("tail_length")]
    public int TailLength { get; set; }
    [Required]
    public int Weight { get; set; }

    internal class EditDogCommandHandler : IRequestHandler<EditDogCommand, IResult<EditDogResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IDogsService _dogsService;

        public EditDogCommandHandler(
            IMapper mapper,
            IDogsService dogsService)
        {
            _mapper = mapper;
            _dogsService = dogsService;
        }

        public async Task<IResult<EditDogResponse>> Handle(EditDogCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existingDog = await _dogsService.GetByNameAsync(command.Name);

                if (existingDog.Success && existingDog.Data.Id != command.Id)
                    return Result<EditDogResponse>.CreateFailed(ErrorModel.NameAlreadyExists);

                var dogResult = await _dogsService.GetByIdAsync(command.Id);

                if (!dogResult.Success)
                    return Result<EditDogResponse>.CreateFailed(ErrorModel.DogNotFound);               

                var dog = await _dogsService.UpdateDogAsync(_mapper.Map<Dog>(command));

                if (!dog.Success)
                    return Result<EditDogResponse>.CreateFailed(ErrorModel.DogIsNotUpdated);

                var dogResponse = _mapper.Map<EditDogResponse>(dog.Data);

                return Result<EditDogResponse>.CreateSuccess(dogResponse);
            }
            catch (Exception e)
            {
                return Result<EditDogResponse>.CreateFailed(e.Message);
            }
        }
    }
}