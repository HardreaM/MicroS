using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProfileConnectionLib.ConnectionServices;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace ProfileConnectionLib;

public static class ProfileConnectionStartup
{
    public static void AddProfileService(this IServiceCollection services, IConfiguration configuration)
    {
        var profileConnectionSettings = new ProfileConnectionSettings(configuration);
        services.AddTransient(_ => profileConnectionSettings);
        services.AddScoped<IProfileConnectionServcie, ProfileConnectionService>();
    }
}