using e_Estoque_API.Infrastructure.Extensions;
using e_Estoque_API.Infrastructure.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace e_Estoque_API.Infrastructure.Configuration
{
    public static class MessageBusConfig
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionFactory = new ConnectionFactory();

            var rabbitMqSection = configuration.GetSection("RabbitMq");
            services.Configure<RabbitMq>(rabbitMqSection);
            var rabbitMq = rabbitMqSection.Get<RabbitMq>();

            if (rabbitMq == null)
            {
                throw new ArgumentNullException("RabbitMq configuration is missing");
            }

            connectionFactory.HostName = rabbitMq.Host;
            connectionFactory.Port = rabbitMq.Port;
            connectionFactory.UserName = rabbitMq.Username;
            connectionFactory.Password = rabbitMq.Password;

            var connection = connectionFactory.CreateConnection("e-estoque-producer");

            services.AddSingleton(new ProducerConnection(connection));
            services.AddSingleton<IMessageBusClient, RabbitMqClient>();

            return services;
        }
    }
}
