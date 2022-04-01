using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;
using Newtonsoft.Json;

namespace NetProject.Infrastructure.Database.Configurations;

public class StoryConfiguration : IEntityTypeConfiguration<Story>
{
    public void Configure(EntityTypeBuilder<Story> builder)
    {
        builder.ToTable("stories");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).HasColumnName("name");
        builder.Property(x => x.CreatorId).HasColumnName("creator_id");
        builder.Property(x => x.OwnerIds).HasColumnName("owner_ids")
            .HasConversion(
                x => JsonConvert.SerializeObject(x),
                x => JsonConvert.DeserializeObject<List<Guid>>(x));
    }
}