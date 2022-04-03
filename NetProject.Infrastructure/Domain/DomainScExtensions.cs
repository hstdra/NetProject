using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetProject.Domain.Core;
using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Database;
using NetProject.Infrastructure.Domain.Repositories;

namespace NetProject.Infrastructure.Domain;

public static class DomainScExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<AppDbContext>(options =>
        {
            // options
            //     .UseInMemoryDatabase("NetProject")
            //     .ConfigureWarnings(_ => _.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            var connection = configuration.GetSection("Database:ConnectionString").Value;
            options.UseMySql(connection, ServerVersion.AutoDetect(connection),
                x =>
                {
                    x.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
        });

        services.AddScoped<IUnitOfWork>(sp => new UnitOfWork((sp.GetRequiredService<AppDbContext>())));
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IStoryRepository, StoryRepository>();

        return services;
    }
}