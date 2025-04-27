using Akoyur.TestTask.ApiModels.Common;
using Akoyur.TestTask.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Akoyur.TestTask.WebAPI.Filters;

/// <summary>
/// A custom action filter that validates the model state before proceeding with the action execution.
/// </summary>
public class ValidationFilter : IAsyncActionFilter
{
    /// <summary>
    /// Validates the model state before executing the action. If the model state is invalid, 
    /// it returns a BadRequest response with validation errors.
    /// </summary>
    /// <param name="context">The context for the action execution.</param>
    /// <param name="next">The delegate to execute the next action in the pipeline.</param>
    /// <returns>Asynchronous task representing the completion of the action execution.</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            // Collect validation errors from the model state
            var errors = context.ModelState
                .Where(ms => ms.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key.ToCamelCase(), // Convert key to camel case
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                );

            // Prepare error response
            var result = new ErrorResponse("Validation failed.", errors!);
            await context.HttpContext.WriteResponseAsync(result, HttpStatusCode.BadRequest);

            return;
        }

        // Proceed with the action execution if the model state is valid
        await next();
    }
}
