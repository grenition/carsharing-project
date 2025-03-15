using AppHost.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApiServices();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApiServices();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
