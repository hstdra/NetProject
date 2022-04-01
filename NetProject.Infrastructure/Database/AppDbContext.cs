using Microsoft.EntityFrameworkCore;
using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;

namespace NetProject.Infrastructure.Database;

public sealed class AppDbContext : DbContext
{
    public DbSet<Member> Members { get; set; }
    public DbSet<Story> Stories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}