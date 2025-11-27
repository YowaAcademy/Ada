using System;

namespace Project03_EShop.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // İstek gelmeden hemen önce EShop API isteklerini logla
        _logger.LogInformation(
            "E-Shop API İsteği: {Method} {Path} - {Time}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            DateTime.UtcNow
        );
        await _next(httpContext);
        _logger.LogInformation(
            "E-Shop API İsteği: {StatusCode} - {Time}",
            httpContext.Response.StatusCode,
            DateTime.UtcNow
        );
    }
}
