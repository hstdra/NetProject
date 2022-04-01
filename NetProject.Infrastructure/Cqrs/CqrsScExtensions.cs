using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetProject.Infrastructure.Cqrs.Commands;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.Infrastructure.Cqrs;

public static class CqrsScExtensions
{
    public static IServiceCollection AddCqrs(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(assembly);
        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<ICommandBus, CommandBus>();

        return services;
    }
}