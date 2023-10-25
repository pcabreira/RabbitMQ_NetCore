using EasyNetQ;
using EasyNetQ.Customers.API.Bus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var bus = RabbitHutch.CreateBus("host=localhost");

builder.Services.AddSingleton<IBusService, EasyNetQService>(
    o => new EasyNetQService(bus)
    );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

