using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Endpoints;

public class DevEndpoint(UniLettersDbContext context) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/dev/init");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Logger.LogInformation("Ensuring database exists");
        if (await context.Database.EnsureCreatedAsync(ct))
        {
            Logger.LogInformation("Created database!");
        }
        
        Logger.LogInformation("Applying migrations");
        await context.Database.MigrateAsync(ct);
        
        Logger.LogInformation("Seeding data...")
            ;
        if (!await context.Students.AnyAsync(ct))
        {
            Logger.LogInformation("Adding students");
            context.Students.AddRange(
                new Student("p21028", "Μαρια Δημητρουλη"),
                new Student("p21151", "Κωνσταντινος Σκλαβενιτης"),
                new Student("p21115", "Γιωργος Νικολαιδης"));
        }

        if (!await context.Courses.AnyAsync(ct))
        {
            Logger.LogInformation("Adding courses");
            context.Courses.AddRange(
                new Course("Τεχνολογια Λογισμικου"),
                new Course("Αναλυση Ι"),
                new Course("Επεξεργασια Σηματων Φωνης και Ηχου"));
        }

        await context.SaveChangesAsync(ct);

        if (await context.Set<Grade>().AnyAsync(ct))
        {
            await SendOkAsync(ct);
            return;
        }
        
        Logger.LogInformation("Adding grades ");
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

        await context.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}