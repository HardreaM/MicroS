using System.Collections.Concurrent;
using System.Text;
using Core.HttpLogic.Services;
using Core.HttpLogic.Services.HttpBase.HttpRequest;
using Core.HttpLogic.Services.HttpBase.HttpResponse;
using Core.HttpLogic.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProfileConnectionLib.ConnectionServices;

public interface IRabbitRequestService : IHttpRequestService
{
}

public class RabbitRpcClientService : IRabbitRequestService, IDisposable
{
    private readonly string queueName;
    private readonly IConnection connection;
    private readonly IModel channel;
    private readonly string replyQueueName;
    private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();

    public RabbitRpcClientService(ProfileConnectionSettings connectionSettings)
    {
        var factory = new ConnectionFactory { HostName = connectionSettings.ClientName };
        queueName = connectionSettings.RabbitQueue;
        connection = factory.CreateConnection();
        channel = connection.CreateModel();
        // declare a server-named queue
        replyQueueName = channel.QueueDeclare().QueueName;
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                return;
            var body = ea.Body.ToArray();
            var response = Encoding.UTF8.GetString(body);

            tcs.TrySetResult(response);
        };

        channel.BasicConsume(consumer: consumer,
            queue: replyQueueName,
            autoAck: true);
    }

    public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData,
        HttpConnectionData connectionData = default)
    {
        IBasicProperties props = channel.CreateBasicProperties();
        props.Headers = new Dictionary<string, object>();
        props.Headers.Add("action", requestData.HeaderDictionary["action"]);

        var message = JsonConvert.SerializeObject(requestData.Body);

        var res = await CallAsync(message, props);

        return new HttpResponse<TResponse>
        {
            Body = JsonConvert.DeserializeObject<TResponse>(res)
        };
    }

    private Task<string> CallAsync(string message, IBasicProperties props,
        CancellationToken cancellationToken = default)
    {
        var correlationId = Guid.NewGuid().ToString();
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueueName;
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var tcs = new TaskCompletionSource<string>();
        callbackMapper.TryAdd(correlationId, tcs);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: queueName,
            basicProperties: props,
            body: messageBytes);

        cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));
        return tcs.Task;
    }

    public void Dispose()
    {
        channel.Close();
        connection.Close();
    }
}