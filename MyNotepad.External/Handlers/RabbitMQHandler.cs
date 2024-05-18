using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyNotepad.Domain.Config;
using MyNotepad.External.Handlers.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MyNotepad.External.Handlers;
public class RabbitMQHandler : IRabbitMQHandler
{
    private readonly RabbitMQConfig _rabbitMQConfig;
    private readonly ILogger<RabbitMQHandler> _logger;

    private const int MaxRetries = 3;
    private int RetryCounter = 0;

    public RabbitMQHandler(IOptions<RabbitMQConfig> rabbitMQConfig, ILogger<RabbitMQHandler> logger)
    {
        _rabbitMQConfig = rabbitMQConfig.Value;
        _logger = logger;
    }

    public ConnectionFactory GetConnection()
    {
        var factory = new ConnectionFactory() 
        { 
            HostName = _rabbitMQConfig.HostName, 
            UserName = _rabbitMQConfig.UserName, 
            Password = _rabbitMQConfig.Password 
        };
        return factory;
    }

    public void SendMessage(string message, string queueName)
    {
        try
        {
            var factory = GetConnection();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);

                _logger.LogInformation($"[x] Sent: {message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"There was an error while trying to send the message to the host. Error: {ex.Message}");

            if (RetryCounter < MaxRetries)
            {
                RetryCounter++;
                _logger.LogInformation($"Trying to send message again (Attempt {RetryCounter} of {MaxRetries})");
                SendMessage(message, queueName);
            }

            RetryCounter = 0;
            _logger.LogError($"The application wasn't able to send the message to the RabbitMQ host. Error: {ex.Message}", ex);
        }
    }

    public string ReceiveMessage(Action<string> messageHandler, string queueName)
    {
        var factory = GetConnection();
        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                messageHandler(message);
            };

             var message = channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return message;
        }
    }
}

