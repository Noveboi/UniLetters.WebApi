using Microsoft.EntityFrameworkCore;

namespace UniLetters.WebApi.Data;

public class UniLettersDbContext(DbContextOptions<UniLettersDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniLettersDbContext).Assembly);
    }
}