using MassTransit.Customers.API.Bus;
using MassTransit.Customers.API.Models;
using MessagingEvents.Shared;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.Customers.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly IBusService _bus;

        public CustomersController(IBusService bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerInputModel model)
        {
            var @event = new CustomerCreated(model.Id, model.FullName, model.Email, model.PhoneNumber, model.BirthDate);

            await _bus.Publish(@event);

            return NoContent();
        }
    }
}

