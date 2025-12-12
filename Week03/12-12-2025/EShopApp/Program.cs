using System.Text;
using EShopApp.Data;
using EShopApp.Middleware;
using EShopApp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection"));
});

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Apply Migrations BEFORE middleware
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<DatabaseConnectionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointDataSources) =>
    {
        var sb = new StringBuilder();
        var endpoints = endpointDataSources.SelectMany(eds => eds.Endpoints);
        foreach (var endpoint in endpoints)
        {
            if (endpoint is RouteEndpoint routeEndpoint)
            {
                sb.AppendLine($"{routeEndpoint.RoutePattern.RawText}");
            }
        }
        return sb.ToString();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
