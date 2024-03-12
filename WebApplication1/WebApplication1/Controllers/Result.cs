#nullable enable
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;

namespace Hyland.Healthcare.Shared.Types.Models;

public abstract record Result<T>
{
    public required string SourceId { get; init; } = string.Empty;
    public required HttpStatusCode StatusCode { get; init; } = OK;
}

public record SuccessResult<T> : Result<T>
{
    public required T Value { get; init; }
}

public record ProblemDetailsResult<T> : Result<T>
{
    public string Context { get; private init; } = string.Empty;
    public ProblemDetails ProblemDetails { get; private init; } = new();
    
    private ProblemDetailsResult()
    {
        // Private constructor to prevent external instantiation
    }
    
    public static ProblemDetailsResult<T> Create(
        string sourceId, HttpStatusCode statusCode,
        ProblemDetails problemDetails,
        [CallerMemberName]
        string context = "")
    {
        problemDetails.Extensions["SourceId"] = sourceId;
        problemDetails.Extensions["Context"] = context;
        
        return new ProblemDetailsResult<T> {
            StatusCode = statusCode,
            ProblemDetails = problemDetails,
            SourceId = sourceId,
            Context = context
        };
    }
    
    public static ProblemDetailsResult<T> Create(
        (string SourceId, HttpStatusCode StatusCode) requiredParams,
        ProblemDetails problemDetails,
        [CallerMemberName]
        string context = "")
    {
        problemDetails.Extensions["SourceId"] = requiredParams.SourceId;
        problemDetails.Extensions["Context"] = context;
        
        return new ProblemDetailsResult<T> {
            StatusCode = requiredParams.StatusCode,
            ProblemDetails = problemDetails,
            SourceId = requiredParams.SourceId,
            Context = context
        };
    }
}

public record EmptyContentResult : Result<EmptyContent>;

public record EmptyContent;
