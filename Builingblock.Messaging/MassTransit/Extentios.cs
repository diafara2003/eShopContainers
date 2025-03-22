using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Builingblock.Messaging.MassTransit;

public static class Extentios
{
    public static IServiceCollection AddMessageBroker
        (this IServiceCollection services, IConfiguration confguration, Assembly? assembly = null)
    {
        services.AddMassTransit(x =>
                {
                    x.SetKebabCaseEndpointNameFormatter();

                    if (assembly != null)
                        x.AddConsumers(assembly);

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(new Uri(confguration["MessageBroker:Host"]!),

                            host =>
                            {
                                host.Username(confguration["MessageBroker:Username"]!);
                                host.Password(confguration["MessageBroker:Password"]!);
                            });

                        cfg.ConfigureEndpoints(context);

                    });
                });

        return services;
    }
}
