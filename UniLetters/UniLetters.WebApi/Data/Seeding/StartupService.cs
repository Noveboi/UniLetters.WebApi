
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data.Seeding;
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
            context.Students.AddRange([
                new Student("p21028", "Μαρια Δημητρουλη"),
                new Student("p21151", "Κωνσταντινος Σκλαβενιτης"),
                new Student("p21115", "Γιωργος Νικολαιδης"),
                ..FakeStudents.Get(15)]);
        }

        if (!await context.Courses.AnyAsync(ct))
        {
            logger.LogInformation("Adding courses");
            context.Courses.AddRange(
                new Course("Τεχνολογία Λογισμικού", 6),
                new Course("Ανάλυση Ι", 1),
                new Course("Επεξεργασία Σημάτων Φωνής και Ήχου", 8),
                new Course("Ανάλυση ΙΙ", 2),
                new Course("Αντικειμενοστρεφής Προγραμματισμός", 2),
                new Course("Εκπαιδευτικό Λογισμικό", 8),
                new Course("Αλληλεπίδραση Ανθρώπου - Υπολογιστή", 5),
                new Course("Πληροφοριακά Συστήματα", 5),
                new Course("Βάσεις Δεδομένων", 4),
                new Course("Αλγόριθμοι", 4),
                new Course("Πιθανότητες και Στατιστική", 3),
                new Course("Λειτουργικά Συστήματα", 3),
                new Course("Βιοπληροφορική", 6),
                new Course("Συστήματα Πολυμέσων", 6),
                new Course("Τεχνητή Νοημοσύνη και Έμπειρα Συστήματα", 6),
                new Course("Ανάλυση Εικόνας", 7),
                new Course("Εικονική Πραγματικότητα", 7),
                new Course("Σύγχρονα Θέματα Τεχνολογίας Λογισμικού", 7));
        }

        logger.LogInformation("Adding grades");
        var students = await context.Students.ToListAsync(ct);
        var courses = await context.Courses.ToListAsync(ct);

        foreach (var student in students)
        {
            foreach (var course in courses)
            {
                context.Set<Grade>().Add(new Grade(
                    am: student.Am,
                    courseId: course.Id,
                    value: double.Round(Random.Shared.NextDouble() * 10, 1)));
            }
        }

        await context.SaveChangesAsync(ct);
    }
} 