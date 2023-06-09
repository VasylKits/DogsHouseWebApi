using MediatR;
using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations.Generics;

namespace Application.Features.Dogs.Queries.Ping;

public record PingRequest : IRequest<IResult<string>>;

internal class PingRequestHandler : IRequestHandler<PingRequest, IResult<string>>
{
    public async Task<IResult<string>> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return Result<string>.CreateSuccess("Dogs house service. Version 1.0.1");
    }
}
