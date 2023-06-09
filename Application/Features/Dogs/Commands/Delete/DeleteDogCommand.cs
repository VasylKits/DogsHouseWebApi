using Shared.Common.Constants;
using Shared.Common.Models.Result.Abstractions;
using Shared.Common.Models.Result.Implementations;
using Application.Interfaces;
using MediatR;

namespace Application.Features.Dogs.Commands.Delete;

public record DeleteDogCommand(int Id) : IRequest<IResult>;

internal class DeleteDogCommandHandler : IRequestHandler<DeleteDogCommand, IResult>
{
    private readonly IDogsService _dogsService;

    public DeleteDogCommandHandler(
        IDogsService dogsService)
    {
        _dogsService = dogsService;
    }

    public async Task<IResult> Handle(DeleteDogCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var dogResult = await _dogsService.GetByIdAsync(command.Id);

            if (!dogResult.Success)
                return Result.CreateFailed(ErrorModel.DogNotFound);

            var result = await _dogsService.DeleteDogAsync(command.Id);

            if (!result.Success)
                return Result.CreateFailed(ErrorModel.DogIsNotDeleted);

            return Result.CreateSuccess();
        }
        catch (Exception e)
        {
            return Result.CreateFailed(e.Message);
        }
    }
}