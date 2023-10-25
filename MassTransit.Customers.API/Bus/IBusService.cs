using System;
namespace MassTransit.Customers.API.Bus
{
    public interface IBusService
    {
        Task Publish<T>(T message);
    }
}

