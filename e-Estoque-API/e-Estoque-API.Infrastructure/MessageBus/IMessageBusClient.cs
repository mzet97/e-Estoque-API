namespace e_Estoque_API.Infrastructure.MessageBus
{
    public interface IMessageBusClient
    {
        void Publish(object message, string routingKey, string exchange);
    }
}
