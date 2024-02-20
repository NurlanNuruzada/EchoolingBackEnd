using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Echooling.Persistance.Configurations;
public class CourseCategoryConfiguration : IEntityTypeConfiguration<CourseCategories>
{
    public void Configure(EntityTypeBuilder<CourseCategories> builder)
    {
        builder.Property(x => x.Category).IsRequired(true).HasMaxLength(100);
    }
}

