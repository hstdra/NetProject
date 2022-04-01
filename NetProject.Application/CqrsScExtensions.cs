using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetProject.Infrastructure.Cqrs;

namespace NetProject.Application;

public static class CqrsScExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCqrs(Assembly.GetExecutingAssembly());

        return services;
    }
}