using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace BuildingBlocks.Exceptions;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error message {exceptionMessage}, Time of ocurrence {time}"
            , exception.Message, DateTime.UtcNow);

        (string Detail, string Title, int statusCode) details = exception switch
        {
            BadRequestException badRequestException => (badRequestException.Details, exception.GetType().Name, StatusCodes.Status400BadRequest),
            NotFoundException notFoundException => ("", exception.GetType().Name, StatusCodes.Status404NotFound),
            InternalServerException internalServerException =>
            (internalServerException.Details, exception.GetType().Name, StatusCodes.Status500InternalServerError),
            ValidationException validationException =>
            (validationException.Message, exception.GetType().Name, StatusCodes.Status400BadRequest),
            _ => ("", "Error", StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Title,
            Detail = details.Detail,
            Status = details.statusCode,
            Instance = httpContext.TraceIdentifier,
        };
        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validacionException)
        {
            problemDetails.Extensions.Add("errors", validacionException.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
        return true;
    }
}
