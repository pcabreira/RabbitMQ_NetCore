namespace EasyNetQ.Customers.API.Bus
{
    public interface IBusService
    {
        void Publish<T>(string routingKey, T message);
    }
}
