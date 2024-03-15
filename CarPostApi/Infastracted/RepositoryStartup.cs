using Domain.Interfaces;
using Infastracted.Connections;
using Infastracted.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infastracted;

public static class RepositoryStartup
{
    public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var dalSettings = new DalSettings(configuration);
        services.AddTransient(_ => dalSettings);
        services.AddDbContext<CarPostInfoContext>(options => options.UseNpgsql(dalSettings.ConnectionString));
        services.AddScoped<IStorePost, PostRepository>();
    }
    
    public static void AddCheckUser(this IServiceCollection services)
    {
        services.AddScoped<ICheckUser, CheckUser>();
    }
}