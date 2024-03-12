using Hyland.Healthcare.Shared.Types.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        return result switch
        {
            SuccessResult<T> successResult => new OkObjectResult(successResult.Value),
            EmptyContentResult => new NoContentResult(),
            ProblemDetailsResult<T> problemDetailsResult => new ObjectResult(problemDetailsResult.ProblemDetails)
            {
                StatusCode = (int)problemDetailsResult.StatusCode
            },
            _ => throw new InvalidOperationException($"Unknown {nameof(Result<T>)} type.")
        };
    }
}
