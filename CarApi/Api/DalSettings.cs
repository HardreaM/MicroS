namespace CarApi;

public class DalSettings
{
    public string ConnectionString { get; }

    public DalSettings(IConfiguration configuration)
    {
        ConnectionString = configuration.GetSection("Dal").GetValue<string>("ConnectionString");
    }
}