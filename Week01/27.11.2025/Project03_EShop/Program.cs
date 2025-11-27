using Microsoft.OpenApi;
using Project03_EShop.Middleware;
using Project03_EShop.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
       Title="E-Shop Stok/Sipariş Yönetim API",
       Version="v1",
       Description="Yowa Academy Asp.NET Core Web API Eğitimi Ada Sınıfı - E-Shop Stok ve Sipariş Yönetim Sistemi. Ürün stok takibi, sipariş yönetimi ve müşteri işlemleri için RESTful API",
       Contact=new OpenApiContact
       {
           Name="Ada",
           Email="ada@yowaacademy.com",
           Url=new Uri("https://yowaacademy.com")
       },
       License=new OpenApiLicense
       {
           Name="MIT Licence",
           Url=new Uri("https://opensource.org/license/mit")
       }
    });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if(File.Exists(xmlPath))
    {
        opt.IncludeXmlComments(xmlPath);
    }
});

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


