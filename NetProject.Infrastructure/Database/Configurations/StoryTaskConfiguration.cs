using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProject.Domain.StoryAggregate;

namespace NetProject.Infrastructure.Database.Configurations;

public class StoryTaskConfiguration : IEntityTypeConfiguration<StoryTask>
{
    public void Configure(EntityTypeBuilder<StoryTask> builder)
    {
        builder.ToTable("storyTasks");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.Name).HasColumnName("Name");
    }
}