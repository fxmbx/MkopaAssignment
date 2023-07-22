using SmsService.Api.Middleware;

namespace SmsService.Api.Extension;
public static class MiddlewareExtension
{

    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder builder)
    => builder.UseMiddleware<RequestLoggerMiddleware>();

}