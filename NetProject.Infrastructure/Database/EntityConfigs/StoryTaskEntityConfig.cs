using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProject.Domain.StoryAggregate;

namespace NetProject.Infrastructure.Database.EntityConfigs;

public class StoryTaskEntityConfig : IEntityTypeConfiguration<StoryTask>
{
    public void Configure(EntityTypeBuilder<StoryTask> builder)
    {
        builder.ToTable("story_tasks");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.StoryId).HasColumnName("StoryId");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.IsDone).HasColumnName("IsDone");
    }
}