using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args); // 1.Aşama

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1",new OpenApiInfo
    {
        Title="Sample WebAPI Project",
        Version="v1",
        Description="Bu proje WebAPI yapısını anlamak için oluşturuldu.",
        Contact=new OpenApiContact
        {
            Name="Samet Ece",
            Email="info@sametece.com"
        }
    });
});



var app = builder.Build();// 2.Aşama


app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run(); //3.Aşama