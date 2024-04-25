using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace WonderFood.Application.Extensions;

public static class SettingsExtensions
{
    public static T CarregarSettings<T>(this IServiceCollection services) where T : class, new()
    {
        using ServiceProvider provider = services.BuildServiceProvider();
        return provider.GetService<IOptions<T>>()!.Value;
    }
} 