using AppHost.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModules();
builder.Services.AddOpenApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApiServices();
}

app.UseHttpsRedirection();
app.MapModules();

app.Run();
