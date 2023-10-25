namespace EasyNetQ.Customers.API.Bus
{
    public class EasyNetQService : IBusService
    {
        private readonly IAdvancedBus _bus;
        private const string EXCHANGE = "person";

        public EasyNetQService(IBus bus)
        {
            _bus = bus.Advanced;
        }

        public void Publish<T>(string routingKey, T message)
        {
            var exchange = _bus.ExchangeDeclare(EXCHANGE, "topic");

            _bus.Publish(exchange, routingKey, true, new Message<T>(message));
        }
    }
}
