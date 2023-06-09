using Shared.Common.Models.Result.Abstractions.Generics;
using Shared.Common.Models.Result.Implementations.Generics;
using Microsoft.AspNetCore.Mvc;
using Shared.Common.Models.Result.Abstractions;

namespace Infrastructure.Persistence.Extensions;

public static class ActionResultExtension
{
    public static IActionResult ToActionResult<T>(this IResult<T> result) =>
        result.Success
            ? new OkObjectResult(result.Data)
            : new BadRequestObjectResult(result.ErrorInfo);

    public static IActionResult ToActionResult(this IResult result) =>
        result.Success
            ? new OkResult()
            : new BadRequestObjectResult(result.ErrorInfo);
}