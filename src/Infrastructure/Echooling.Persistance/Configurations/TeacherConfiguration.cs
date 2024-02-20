using Ecooling.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Echooling.Persistance.Configurations;
public class TeacherConfiguration : IEntityTypeConfiguration<teacherDetails>
{
    public void Configure(EntityTypeBuilder<teacherDetails> builder)
    {
        builder.Property(x => x.hobbies).IsRequired(false).HasMaxLength(200);
        builder.Property(x => x.faculty).IsRequired(false).HasMaxLength(200);
        builder.Property(x => x.Facebook).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.linkedin).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.instagram).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.profession).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.twitter).IsRequired(false).HasMaxLength(100);
        builder.Property(x => x.totalStudentCount).IsRequired(true).HasMaxLength(10);
        builder.Property(x => x.TotalExperianceHours).IsRequired(true).HasMaxLength(20);
        builder.Property(x => x.totalOnlineCourseCount).IsRequired(true).HasMaxLength(10);
    }
}
