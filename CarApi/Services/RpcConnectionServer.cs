using System.Text;
using Logic.Cars.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.DtoModels.UserNameLists;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Services;

public class RpcConnectionServer : BackgroundService
{
    private IModel _channel;
    private IConnection _connection;
    private readonly IUserLogicManager _userLogic;
    private readonly string _hostname;
    private readonly string _queueName;

    public RpcConnectionServer(IServiceScopeFactory serviceScopeFactory, RabbitProfileConnectionSettings settings)
    {
        var scope = serviceScopeFactory.CreateScope();
        _hostname = settings.ClientName;
        _queueName = settings.QueueName;
        _userLogic = scope.ServiceProvider.GetRequiredService<IUserLogicManager>();
        InitializeRabbitMqListener();
    }

    private void InitializeRabbitMqListener()
    {
        var factory = new ConnectionFactory
        {
            HostName = _hostname,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        _channel.BasicConsume(queue: _queueName,
            autoAck: false,
            consumer: consumer);

        consumer.Received += async (model, ea) =>
        {
            string response = string.Empty;

            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = _channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                response = await HandleMessage(message, ea.BasicProperties.Headers);
            }
            catch (Exception e)
            {
                response = string.Empty;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                _channel.BasicPublish(exchange: string.Empty,
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes);
                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        };

        return Task.CompletedTask;
    }

    private async Task<string> HandleMessage(string message, IDictionary<string, object> headers)
    {
        var action = Encoding.UTF8.GetString((byte[])headers["action"]);
        
        if (action == "checkUser")
        {
            var request = JsonConvert.DeserializeObject<CheckUserExistProfileApiRequest>(message);
            var user = await _userLogic.GetUserInfoAsync(request.UserId);
            return JsonConvert.SerializeObject(user);
        }
        else if (action == "getUsersList")
        {
            var request = JsonConvert.DeserializeObject<UserNameListProfileApiRequest>(message);
            var users = await _userLogic.GetUsersWithId(request.guids);
            return JsonConvert.SerializeObject(users);
        }

        return "Task.CompletedTask";
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}