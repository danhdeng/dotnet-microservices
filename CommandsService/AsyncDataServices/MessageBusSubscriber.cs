using System;
using System.Threading;
using System.Threading.Tasks;
using CommandsService.EventProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;

namespace CommandsService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IEventProcessor _eventProcessor;
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        public MessageBusSubscriber(IConfiguration configuation, IEventProcessor eventProcessor)
        {
            _configuration = configuation;
            _eventProcessor = eventProcessor;
            InitializingRabbitMQ();
        }
        protected override Task ExecuteAsync(CancellationToken stopToken)
        {
            //Do your preparation (e.g. Start code) here
            while (!stopToken.IsCancellationRequested)
            {
                //await DoSomethingAsync();
            }
            return null;
            //Do your cleanup (e.g. Stop code) here
        }

        public override void Dispose()
        {
            Console.WriteLine("MessageBus Disposing");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        private void InitializingRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _queueName = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: _queueName,
                                   exchange: "trigger",
                                   routingKey: "");
                Console.WriteLine($"---> Listening on Message Bus ....");
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---> Cannot connect the RabbitMQ with Error: {ex.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"---> RabbitMQ connection shutdown");
        }
    }
}