using MassTransit;
using SelfServicePortal.API.DTO;

namespace SelfServicePortal.API
{
    public static class ServiceExtensions
    {
        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitmqConfig = configuration.GetSection("RabbitMq");
            var url = rabbitmqConfig["url"];
            var password = rabbitmqConfig["password"];
            var username = rabbitmqConfig["username"];
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.AddConsumer<PublishedEmployeeDTO>();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(url), h =>
                    {
                        h.Username(username);
                        h.Password(password);

                    });

                    configurator.ConfigureEndpoints(context);
                });
            });
        }
    }
}
