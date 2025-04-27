using Akoyur.TestTask.ApiModels.Common;
using Akoyur.TestTask.Extensions;
using Akoyur.TestTask.Helpers;
using FluentValidation;
using System.Net;

namespace Akoyur.TestTask.WebAPI.Middlewares;

/// <summary>
/// Middleware to handle errors during the HTTP request processing. 
/// It catches validation exceptions and general exceptions, 
/// returning appropriate error responses.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the next middleware in the pipeline, and handles any exceptions thrown during request processing.
    /// </summary>
    /// <param name="context">The HTTP context for the current request.</param>
    /// <returns>Asynchronous task representing the middleware execution.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Execute the next middleware in the pipeline
            await _next(context);
        }
        catch (ValidationException ex)
        {
            // Handle validation exceptions and return validation errors
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key.ToCamelCase(), // Convert property names to camel case
                    g => g.Select(e => e.ErrorMessage).FirstOrDefault()
                );

            var result = new ErrorResponse("Validation failed.", errors!);
            await context.WriteResponseAsync(result, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            // Log unhandled exceptions and return a generic error message
            _logger.LogCritical(ex, "Unhandled exception occurred.");

            var result = new ErrorResponse(ex.Message);
            await context.WriteResponseAsync(result, HttpStatusCode.BadRequest);
        }
    }
}

