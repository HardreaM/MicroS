using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Intefaces;

namespace Services;

public static class CreatePostStartup
{
    public static void AddCreatePost(this IServiceCollection services)
    {
        services.TryAddScoped<ICreatePost, CreatePost>();
    }
}