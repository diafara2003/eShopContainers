
using BuildingBlocks.Exceptions;
using HealthChecks.UI.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API;

public static class DependencyInjection
{

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!);

        return services;
    }

    public static WebApplication UserApiServices(this WebApplication app)
    {
        
        app.MapCarter();

        app.UseExceptionHandler(op => { });

        app.UseHealthChecks("/health",
            new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {

                ResponseWriter= UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }


}
