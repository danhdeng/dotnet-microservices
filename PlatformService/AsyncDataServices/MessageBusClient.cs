using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MesssageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MesssageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            Console.WriteLine($"HostName: {_configuration["RabbitMQHost"]}, Port: {int.Parse(_configuration["RabbitMQPort"])}");
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine($"---> Connected to Message Bus");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---> Cannot connect the RabbitMQ with Error: {ex.Message}");
            }
        }
        public void PublishNewPlatform(PlatformPublishDto platformPublishDtoObj)
        {
            var message = JsonSerializer.Serialize(platformPublishDtoObj);
            if (_connection.IsOpen)
            {
                Console.WriteLine($"---> RabbitMQ connection is openned, message sending");
                SendMessage(message);
            }
            else
            {
                Console.WriteLine($"---> RabbitMQ connection is closed, message not send");

            }
        }
        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposing");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "trigger",
                                  routingKey: "",
                                basicProperties: null,
                                body: body);
            Console.WriteLine($"we has sent messsge: {message}");
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"---> RabbitMQ connection shutdown");
        }
    }
}