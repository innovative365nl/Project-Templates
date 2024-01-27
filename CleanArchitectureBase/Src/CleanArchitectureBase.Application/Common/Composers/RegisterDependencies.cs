using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureBase.Application.Common.Composers;

public static class RegisterDependencies
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        return services;
    }
}