﻿using MessagingEvents.Shared;
using MessagingEvents.Shared.Services;
using Newtonsoft.Json;

namespace EasyNetQ.Marketing.API.Subscribers
{
    public class CustomerCreatedSubscriber : IHostedService
	{
        const string CUSTOMER_CREATED_QUEUE = "customer-created";

        private readonly IAdvancedBus _bus;

        public IServiceProvider Services { get; }

        public CustomerCreatedSubscriber(IServiceProvider services, IBus bus)
        {
            _bus = bus.Advanced;

            Services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var queue = _bus.QueueDeclare(CUSTOMER_CREATED_QUEUE);

            _bus.Consume<CustomerCreated>(queue, async (msg, info) =>
            {
                var json = JsonConvert.SerializeObject(msg.Body);

                await SendEmail(msg.Body);
                Console.WriteLine($"Message Received: {json}");
            });
        }

        public async Task SendEmail(CustomerCreated @event)
        {
            using (var scope = Services.CreateScope())
            {
                var service = scope.ServiceProvider.GetRequiredService<INotificationService>();

                await service.SendEmail(@event.Email, CUSTOMER_CREATED_QUEUE, new Dictionary<string, string> { { "name", @event.FullName } });
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

