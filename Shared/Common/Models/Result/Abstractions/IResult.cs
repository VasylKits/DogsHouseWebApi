using Shared.Common.Models.Result.Implementations;

namespace Shared.Common.Models.Result.Abstractions;

public interface IResult
{
    bool Success { get; }

    ErrorInfo ErrorInfo { get; }
}