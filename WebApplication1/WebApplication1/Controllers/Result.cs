#nullable enable
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using static System.Net.HttpStatusCode;

namespace WebApplication1.Controllers;

public abstract record Result<T>
{
    public required string SourceId { get; init; } = string.Empty;
    public required HttpStatusCode StatusCode { get; init; } = OK;
    public string CallerMemberName { get; init; } = string.Empty;
}

public record SuccessResult<T> : Result<T>
{
    public required T Value { get; init; }
}

public record ProblemDetailsResult<T> : Result<T>
{
    //public string Context { get; private init; } = string.Empty;
    public ProblemDetails ProblemDetails { get; private init; } = new();
    
    private ProblemDetailsResult()
    {
        // Private constructor to prevent external instantiation
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
            CallerMemberName = context
        };
    }
    
    public static ProblemDetailsResult<T> Create(
        (string SourceId, HttpStatusCode StatusCode, ProblemDetails ProblemDetails) problemDetailsParams,
        [CallerMemberName]
        string callerMemberName = "")
    {
        problemDetailsParams.ProblemDetails.Extensions[nameof(SourceId)] = problemDetailsParams.SourceId;
        problemDetailsParams.ProblemDetails.Extensions[nameof(CallerMemberName)] = callerMemberName;
        
        return new ProblemDetailsResult<T> {
            StatusCode = problemDetailsParams.StatusCode,
            ProblemDetails = problemDetailsParams.ProblemDetails,
            SourceId = problemDetailsParams.SourceId,
            CallerMemberName = callerMemberName
        };
    }
}

public record EmptyContentResult : Result<EmptyContent>;

public record EmptyContent;
