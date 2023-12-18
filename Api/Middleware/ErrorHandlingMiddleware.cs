using System.Net;
using System.Text.Json;

namespace Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        } catch (Exception ex)
        {
            await HandleExceptionAsync(context,ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { Message = "An error occured during processing your request" });
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(result);
    }
}
