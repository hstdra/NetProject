using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProject.Domain.StoryAggregate;

namespace NetProject.Infrastructure.Database.EntityConfigs;

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
        builder.ToTable("stories");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasColumnName("Id");
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(x => x.CreatorId).HasColumnName("CreatorId");
        builder.Property(x => x.OwnerIds).HasColumnName("OwnerIds")
            .HasConversion(
                x => JsonSerializer.Serialize(x, serializerOptions),
                x => JsonSerializer.Deserialize<List<Guid>>(x, serializerOptions),
                new ValueComparer<List<Guid>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                ));
    }
}