using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace e_Estoque_API.Infrastructure.MessageBus
{
    public class RabbitMqClient : IMessageBusClient
    {
        private readonly IConnection _connection;

        public RabbitMqClient(ProducerConnection producerConnection)
        {
            _connection = producerConnection.Connection;
        }

        public void Publish(object message, string routingKey, string exchange)
        {
            var channel = _connection.CreateModel();

            JsonSerializerOptions settings = new(JsonSerializerDefaults.Web)
            {
                WriteIndented = false,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var payload = JsonSerializer.Serialize(message, settings);
            var body = Encoding.UTF8.GetBytes(payload);

            channel.ExchangeDeclare(exchange, "topic", true);

            channel.BasicPublish(exchange, routingKey, null, body);
        }
    }
}
