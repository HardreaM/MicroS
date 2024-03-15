using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ProfileConnectionLib.ConnectionServices;

public static class RabbitProfileConnectionStartup
{
    public static IServiceCollection AddRabbitProfileConnectionService(this IServiceCollection services)
    {
        services.TryAddTransient<IRabbitRequestService, RabbitRpcClientService>();

        return services;
    }
}