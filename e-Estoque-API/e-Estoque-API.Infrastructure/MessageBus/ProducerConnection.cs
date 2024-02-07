using RabbitMQ.Client;

namespace e_Estoque_API.Infrastructure.MessageBus;

public class ProducerConnection
{
    public IConnection Connection { get; private set; }

    public ProducerConnection(IConnection connection)
    {
        Connection = connection;
    }
}