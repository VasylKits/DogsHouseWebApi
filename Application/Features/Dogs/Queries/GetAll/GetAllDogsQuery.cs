using Application.Interfaces;
using MediatR;
using Shared.Common.Interfaces;
using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations;
using Shared.Common.Models.Result.Implementations.Generics;

namespace Application.Features.Dogs.Queries.GetAll;

public class GetAllDogsQuery : IRequest<IResult<PaginatedList<GetDogsResponse>>>, IPagination, ISorting, ISearchFilter
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public string? Attribute { get; set; } = "id";
    public string Order { get; set; } = "asc";
}

internal class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, IResult<PaginatedList<GetDogsResponse>>>
{
    private readonly IDogsService _dogsService;

    public GetAllDogsQueryHandler(IDogsService dogsService)
    {
        _dogsService = dogsService;
    }

    public async Task<IResult<PaginatedList<GetDogsResponse>>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dogsService.GetAll(request);

            return result;
        }
        catch (Exception e)
        {
            return Result<PaginatedList<GetDogsResponse>>.CreateFailed(e.Message);
        }
    }
}