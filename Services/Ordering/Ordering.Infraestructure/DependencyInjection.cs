using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ordering.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraEstructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DataBase");

        return services;
    }
}
