using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions;

public static class DependencyInjection
{
    public static void AddCommon(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
    }
}