
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infraestructure.Interceptors;
using Ordering.Infrastructure.Data.Interceptors;



namespace Ordering.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraEstructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispachDomainEventsInterceptor>();

        services.AddDbContext<ApplicaionDbContext>((sp, options) =>
        {
            options.AddInterceptors(
                sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicacionDbContext, ApplicaionDbContext>();

        return services;
    }
}

