using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Api.Common.Exceptions;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        LogException(httpContext, exception);
        var problem = exception switch
        {
            ValidationException ex => new ValidationProblemDetails(
                ex.Errors
                    .GroupBy(g => g.PropertyName)
                    .ToDictionary(g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    )
            )
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
            },
            UnauthorizedAccessException ex => new ProblemDetails
            {
                Title = "Unauthorized",
                Detail = ex.Message,
                Status = StatusCodes.Status401Unauthorized
            },
            _ => new ProblemDetails
            {
                Title = "Server Internal Error",
                Detail = "An unhandled exception has occurred.",
                Status = StatusCodes.Status500InternalServerError,
            }
        };

        httpContext.Response.StatusCode = problem.Status!.Value;
        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problem,
        });

        return true;
    }

    private void LogException(HttpContext httpContext, Exception exception)
    {
        var requestPath = httpContext.Request.Path;
        var requestMethod = httpContext.Request.Method;

        if (exception is ValidationException validationException)
        {
            logger.LogWarning(
            "Validation failed for {RequestMethod} {RequestPath}. Total errors: {ErrorCount}",
            requestMethod,
            requestPath,
            validationException.Errors.Count());
        }
        else
        {
            logger.LogError(exception,
            "An unhandled exception occurred while processing {RequestMethod} {RequestPath}: {ErrorMessage}",
            requestMethod,
            requestPath,
            exception.Message);
        }
    }
}