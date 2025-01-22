using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LookGenerator.WebAPI.ExceptionHandlers ;

    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence: {time}",
                exception.Message, DateTime.UtcNow);

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException =>
                    (
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                        ),
                ValidationException =>
                    (
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                        ),
                BadRequestException =>
                    (
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
                        ),
                NotFoundException =>
                    (
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status404NotFound
                        ),
                UnauthorizedException =>
                    (
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized
                        ),
                AccessForbiddenException =>
                    (
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden
                        ),
                _ =>
                    (
                        // Fallback for unhandled exceptions
                        exception.Message,
                            exception.GetType().Name,
                            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
                        )
                };

            // Build the ProblemDetails object
            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };

            // Add trace identifier for debugging
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            // If it’s a ValidationException, add the validation errors
            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
            }

            // Write to response as JSON
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

            return true;
        }
    }