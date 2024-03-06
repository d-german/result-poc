using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public abstract record Result<T>
{
    public required string SourceId { get; init; } = string.Empty;
    public required HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
}

public record SuccessResult<T> : Result<T>
{
    public required T Value { get; init; }
}

public record ExceptionResult<T> : Result<T>
{
    public required Exception Exception { get; init; }
}

public record ProblemDetailsResult<T> : Result<T>
{
    public required ProblemDetails ProblemDetails { get; init; }
}

public record EmptyContentResult : Result<EmptyContent>;

public record EmptyContent;
