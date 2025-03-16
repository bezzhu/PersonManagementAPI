using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred.");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, response) = ex switch
        {
            ArgumentException _ => (HttpStatusCode.BadRequest, new ProblemDetails
            {
                Title = "Bad Request",
                Detail = ex.Message,
                Status = (int)HttpStatusCode.BadRequest
            }),
            KeyNotFoundException _ => (HttpStatusCode.NotFound, new ProblemDetails
            {
                Title = "Not Found",
                Detail = ex.Message,
                Status = (int)HttpStatusCode.NotFound
            }),
            _ => (HttpStatusCode.InternalServerError, new ProblemDetails
            {
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Status = (int)HttpStatusCode.InternalServerError
            })
        };

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
