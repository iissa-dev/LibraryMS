using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMS.Api.Exceptions;

public sealed class GlobalExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
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
}