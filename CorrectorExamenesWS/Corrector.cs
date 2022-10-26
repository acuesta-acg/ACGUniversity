using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrectorExamenesWS
{
    public class Corrector : BackgroundService
    {
        private readonly ILogger<Corrector> _logger;
        private IConnection _connection;
        private IModel _channel;

        public Corrector(ILogger<Corrector> logger)
        {
            _logger = logger;
            _logger.LogInformation("Iniciado el cliente: {time}", DateTimeOffset.Now);

            var cf = new ConnectionFactory() { HostName = "localhost" };

            _connection = cf.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare("hola", true, false, false, null);

        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

                _logger.LogInformation("{time} recibido mensaje {content}", DateTimeOffset.Now, content);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
 
            _channel.BasicConsume("hola", false, consumer);

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
