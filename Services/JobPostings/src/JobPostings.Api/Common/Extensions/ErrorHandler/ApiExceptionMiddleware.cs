using System.Text.Json;

namespace JobPostings.Api.Common.Extensions.ErrorHandler;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public ApiExceptionMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _requestDelegate(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = ErrorDetailsFactory.Create(exception);
        return CreateResponse(context, error);
    }

    private static Task CreateResponse(HttpContext context, ErrorDetails error)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)error.Status;

        var result = JsonSerializer.Serialize(error, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(result);
    }
}