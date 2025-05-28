using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Data;

public class UniLettersDbContext(DbContextOptions<UniLettersDbContext> options) : DbContext(options)
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniLettersDbContext).Assembly);
    }
}