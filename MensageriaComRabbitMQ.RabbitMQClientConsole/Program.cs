// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

const string EXCHANGE = "person";

var person = new Person("Teste", "123.456.789-10", new DateTime(1992, 1, 1));

var connectionFactory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = connectionFactory.CreateConnection("rabbitmq");

var channel = connection.CreateModel();

var json = JsonSerializer.Serialize(person);
var byteArray = Encoding.UTF8.GetBytes(json);

channel.BasicPublish(EXCHANGE, "hr.person-created", null, byteArray);

Console.WriteLine($"Message published: {json}");

var consumerChannel = connection.CreateModel();

var consumer = new EventingBasicConsumer(consumerChannel);

consumer.Received += (sender, eventArgs) =>
{
    var contentArray = eventArgs.Body.ToArray();
    var contentString = Encoding.UTF8.GetString(contentArray);

    var message = JsonSerializer.Deserialize<Person>(contentString);

    Console.WriteLine($"Message received: {contentString}");

    consumerChannel.BasicAck(eventArgs.DeliveryTag, false);
};

consumerChannel.BasicConsume("person-created", false, consumer);

Console.ReadLine();

class Person
{
    public Person(string fullName, string document, DateTime birthDate)
    {
        FullName = fullName;
        Document = document;
        BirthDate = birthDate;
    }

    public string FullName { get; set; }
    public string Document { get; set; }
    public DateTime BirthDate { get; set; }
}