using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echooling.Persistance.Configurations;

public class EventCategoryConfigurations : IEntityTypeConfiguration<EventCategoryies>
{
    public void Configure(EntityTypeBuilder<EventCategoryies> builder)
    {
        builder.Property(x => x.Category).IsRequired(true).HasMaxLength(100);
    }
}