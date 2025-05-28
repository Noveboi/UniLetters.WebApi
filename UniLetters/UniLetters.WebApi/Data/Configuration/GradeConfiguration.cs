using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Data.Configuration;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasKey(x => new { x.StudentId, x.CourseId });

        builder.HasOne(x => x.Student).WithMany(x => x.Grades).HasForeignKey(x => x.StudentId);
        builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId);
    }
}