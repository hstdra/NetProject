using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using NetProject.Domain.Core;
using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Database;
using NetProject.Infrastructure.Domain.Repositories;

namespace NetProject.Infrastructure.Domain;

public static class DomainScExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
            options
                .UseInMemoryDatabase("NetProject")
                .ConfigureWarnings(_ => _.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        });

        services.AddScoped<IUnitOfWork>(sp => new UnitOfWork((sp.GetRequiredService<AppDbContext>())));
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IStoryRepository, StoryRepository>();

        return services;
    }
}