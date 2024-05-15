using Microsoft.Extensions.Configuration;

namespace ProfileConnectionLib;

public class ProfileConnectionSettings
{
    public string ConnectionType { get; }
    public string ClientName { get; }
    public string RabbitQueue { get; }
    public int Port { get; }

    public ProfileConnectionSettings(IConfiguration configuration)
    {
        var section = configuration.GetSection("ProfileConnection");
        ConnectionType = section.GetValue<string>("ConnectionType");
        ClientName = section.GetValue<string>("ClientName");
        RabbitQueue = section.GetValue<string>("RabbitQueue");
        Port = section.GetValue<int>("Port");
    }
}