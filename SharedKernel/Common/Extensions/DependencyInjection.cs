using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions;

public static class DependencyInjection
{
    private static void UseCommon(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    }
}