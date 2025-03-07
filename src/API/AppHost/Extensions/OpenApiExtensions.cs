namespace AppHost.Extensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static IApplicationBuilder MapOpenApiServices(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });

        return app;
    }
}
