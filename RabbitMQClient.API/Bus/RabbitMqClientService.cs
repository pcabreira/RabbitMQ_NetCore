using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMQClient.Customers.API.Bus
{
    public class RabbitMqClientService : IBusService
    {
        private readonly IModel _channel;
        const string EXCHANGE = "customer";

        public RabbitMqClientService()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection("rabbitmq-client-publisher");

            _channel = connection.CreateModel();
        }

        public void Publish<T>(string routingKey, T message)
        {
            var json = JsonSerializer.Serialize(message);

            var byteArray = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(EXCHANGE, routingKey, null, byteArray);
        }
    }
}
