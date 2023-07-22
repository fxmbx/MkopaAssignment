
using System.Net;

namespace SmsService.Api.Middleware;
public class RequestLoggerMiddleware
{
    private readonly ILoggerService _logger;

    private readonly RequestDelegate _next;

    public RequestLoggerMiddleware(RequestDelegate next, ILoggerService logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {

            await LogRequest(context);
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new ServiceResponse<object>
            {
                Data = null,
                Success = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message,
                Description = ex.StackTrace ?? ex.Source
            };
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }

    private async Task LogRequest(HttpContext context)
    {


        var requestBody = string.Empty;
        if (context.Request.ContentLength != null && context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, System.Text.Encoding.UTF8, true, 1024, true);
            requestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        _logger.LogInfo<string>($"Request: {context.Request.Method} {context.Request.Path}{context.Request.QueryString}");
        _logger.LogInfo<string>($"Request Body: {requestBody}");


    }
}
