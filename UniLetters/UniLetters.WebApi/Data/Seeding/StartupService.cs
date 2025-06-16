using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Data.Seeding;

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

        var students = new List<Student>(FakeStudents.Get(15))
        {
            new("p21028", "Μαρια Δημητρουλη"),
            new("p21151", "Κωνσταντινος Σκλαβενιτης"),
            new("p21115", "Γιωργος Νικολαιδης")
        };

        var courses = new List<Course>
        {
            new("Τεχνολογία Λογισμικού", 6),
            new("Ανάλυση Ι", 1),
            new("Επεξεργασία Σημάτων Φωνής και Ήχου", 8),
            new("Ανάλυση ΙΙ", 2),
            new("Αντικειμενοστρεφής Προγραμματισμός", 2),
            new("Εκπαιδευτικό Λογισμικό", 8),
            new("Αλληλεπίδραση Ανθρώπου - Υπολογιστή", 5),
            new("Πληροφοριακά Συστήματα", 5),
            new("Βάσεις Δεδομένων", 4),
            new("Αλγόριθμοι", 4),
            new("Πιθανότητες και Στατιστική", 3),
            new("Λειτουργικά Συστήματα", 3),
            new("Βιοπληροφορική", 6),
            new("Συστήματα Πολυμέσων", 6),
            new("Τεχνητή Νοημοσύνη και Έμπειρα Συστήματα", 6),
            new("Ανάλυση Εικόνας", 7),
            new("Εικονική Πραγματικότητα", 7),
            new("Σύγχρονα Θέματα Τεχνολογίας Λογισμικού", 7)
        };
        
        if (!await context.Students.AnyAsync(ct))
        {
            logger.LogInformation("Adding students");
            context.Students.AddRange(students);
        }

        if (!await context.Courses.AnyAsync(ct))
        {
            logger.LogInformation("Adding courses");
            context.Courses.AddRange(courses);
        }

        logger.LogInformation("Adding grades");

        foreach (var student in students)
        {
            var proficiencyMultiplier = Random.Shared.NextDouble() * 1.5 + 0.5;
            var gradeProbability = Random.Shared.NextDouble();

            foreach (var course in courses.Where(_ => Random.Shared.NextDouble() <= gradeProbability))
            {
                var grade = Math.Min(10, proficiencyMultiplier * Random.Shared.NextDouble() * 10);
                
                context.Set<Grade>().Add(new Grade(
                    am: student.Am,
                    courseId: course.Id,
                    value: double.Round(grade, 1)));
            }
        }

        await context.SaveChangesAsync(ct);
        
        logger.LogInformation("Finished seeding database!");
    }
} 