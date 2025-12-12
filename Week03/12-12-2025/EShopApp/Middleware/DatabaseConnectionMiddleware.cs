using System;
using EShopApp.Data;

namespace EShopApp.Middleware;

public class DatabaseConnectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<DatabaseConnectionMiddleware> _logger;

    public DatabaseConnectionMiddleware(RequestDelegate next, ILogger<DatabaseConnectionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        try
        {
            if (!await dbContext.Database.CanConnectAsync())
            {
                _logger.LogError("Veritabanına bağlanılamadı, daha sonra yeniden deneyiniz!");
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                context.Response.ContentType = "application/json";
                var response = new
                {
                    IsSuccess = false,
                    Error = "Sunucu geçici olarak kullanılamıyor. Lütfen daha sonra yeniden deneyin"
                };
                await context.Response.WriteAsJsonAsync(response);
                return;
            }
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Middleware Hatası: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var response = new
            {
                IsSuccess = false,
                Error = ex.Message
            };
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
