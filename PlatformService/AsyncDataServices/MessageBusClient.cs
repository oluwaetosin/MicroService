using System.Text;
using System.Text.Json;
using PlatformService.DTOs;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var factory =  new ConnectionFactory() {
               
                HostName = _configuration.GetValue<string>("RabbitMQHost"),
                Port = int.Parse(_configuration.GetValue<string>("RabbitMQPort"))
            };

            try
            {
                _connection = factory.CreateConnection();

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine("Connected To mesage Bus");
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Could not connect to the message bus: {ex.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"Connection shutdown: {e.ToString()}");
        }

        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);

            if(_connection.IsOpen)
            {
                Console.WriteLine($"---> RabbitMq Connection Open, sending message");

                SendMessage(message);
            }
            else
            {
               Console.WriteLine($"---> RabbitMq Connection is closed, not sending message"); 
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger", routingKey: "", basicProperties: null, body: body);

            Console.WriteLine("----> RbbitMq message sendt");
        }

        public void Dispose()
        {
            if(_channel.IsOpen)
            {
                _channel.Close();
                 _connection.Close();
            }
        }
    }

}