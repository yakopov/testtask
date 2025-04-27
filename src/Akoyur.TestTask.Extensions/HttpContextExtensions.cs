using Akoyur.TestTask.Helpers;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Akoyur.TestTask.Extensions;

/// <summary>
/// Extension methods for the HttpContext class.
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// Writes an object as a JSON response with the specified HTTP status code.
    /// </summary>
    /// <param name="httpContext">The HttpContext for the current HTTP request.</param>
    /// <param name="object">The object to serialize and send in the response.</param>
    /// <param name="statusCode">The HTTP status code to set for the response.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task WriteResponseAsync(this HttpContext httpContext, object @object, HttpStatusCode statusCode)
    {
        httpContext.Response.StatusCode = (int)statusCode;
        httpContext.Response.ContentType = "application/json";

        var json = JsonHelper.ToJson(@object);
        await httpContext.Response.WriteAsync(json);
    }
}
