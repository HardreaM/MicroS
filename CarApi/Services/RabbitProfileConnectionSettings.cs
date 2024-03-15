using Microsoft.Extensions.Configuration;

namespace Services;

public class RabbitProfileConnectionSettings
{
    public string ClientName { get; }
    public string QueueName { get; }

    public RabbitProfileConnectionSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection("Rabbit");
        QueueName = section.GetValue<string>("QueueName");
        ClientName = section.GetValue<string>("ClientName");
    }
}