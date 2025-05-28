using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Data.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Am);

        builder.Property(x => x.Am).IsUnicode(false).HasMaxLength(10);
        builder.Property(x => x.Name).HasMaxLength(128);
    }
}