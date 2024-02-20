using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Echooling.Persistance.Configurations;
public class SliderConfiguration : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(x=>x.Title).IsRequired(true).HasMaxLength(20);
        builder.Property(x=>x.SeccondTile).IsRequired(true).HasMaxLength(50);
        builder.Property(x=>x.Description).IsRequired(true).HasMaxLength(250);

    }
}
