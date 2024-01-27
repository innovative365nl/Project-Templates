using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureBase.Infrastructure.Common.Composer;

public static class RegisterDependencies
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}