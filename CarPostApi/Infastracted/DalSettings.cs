using Microsoft.Extensions.Configuration;

namespace Infastracted;

public class DalSettings
{
    public string ConnectionString { get; }

    public DalSettings(IConfiguration configuration)
    {
        ConnectionString = configuration.GetSection("Dal").GetValue<string>("ConnectionString");
    }
}