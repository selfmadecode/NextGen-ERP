using MassTransit;
using System.Reflection;

namespace Shared.EventBus;

public static class RegisterEventBus
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitmqSettings = configuration.GetSection("RabbitMq").Get<RabbitMqSetting>();

        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            // Add consumers from the entry assembly (assembly of the application)
            busConfigurator.AddConsumers(Assembly.GetEntryAssembly());

            // Configure MassTransit to use RabbitMQ as the message broker
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                // Set RabbitMQ host details
                configurator.Host(new Uri(rabbitmqSettings.Url), h =>
                {
                    h.Username(rabbitmqSettings.Username);
                    h.Password(rabbitmqSettings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}
