
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Data;

internal class StartupService(IServiceProvider sp, ILogger<StartupService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var scope = sp.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UniLettersDbContext>();

        logger.LogInformation("Deleting database");
        await context.Database.EnsureDeletedAsync(ct);
        logger.LogInformation("Database deleted!");

        logger.LogInformation("Creating database and applying migrations");
        await context.Database.MigrateAsync(ct);

        logger.LogInformation("Seeding data...");
        if (!await context.Students.AnyAsync(ct))
        {
            logger.LogInformation("Adding students");
            context.Students.AddRange(
                new Student("p21028", "Μαρια Δημητρουλη"),
                new Student("p21151", "Κωνσταντινος Σκλαβενιτης"),
                new Student("p21115", "Γιωργος Νικολαιδης"));
        }

        if (!await context.Courses.AnyAsync(ct))
        {
            logger.LogInformation("Adding courses");
            context.Courses.AddRange(
                new Course("Τεχνολογια Λογισμικου"),
                new Course("Αναλυση Ι"),
                new Course("Επεξεργασια Σηματων Φωνης και Ηχου"));
        }

        await context.SaveChangesAsync(ct);

        if (await context.Set<Grade>().AnyAsync(ct))
        {
            return;
        }

        logger.LogInformation("Adding grades ");
        var students = await context.Students.ToListAsync(ct);
        var courses = await context.Courses.ToListAsync(ct);

        foreach (var student in students)
        {
            foreach (var course in courses)
            {
                context.Set<Grade>().Add(new Grade(
                    studentId: student.Am,
                    courseId: course.Id,
                    value: double.Round(Random.Shared.NextDouble() * 10, 1)));
            }
        }
    }
} 