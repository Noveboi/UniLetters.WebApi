using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Endpoints.Extensions;

namespace UniLetters.WebApi.Endpoints;

public record GetStudentRequest([property: RouteParam] string Am);

public class GetStudentEndpoint(UniLettersDbContext dbContext) : Endpoint<GetStudentRequest, StudentCoursesDto>
{
    public override void Configure()
    {
        Get("/students/{am}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetStudentRequest req, CancellationToken ct)
    {
        var student = await dbContext.Students
            .Include(s => s.Grades)
            .ThenInclude(g => g.Course)
            .FirstOrDefaultAsync(s => s.Am == req.Am, ct);

        if (student is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var studentCoursesDto = student.ToDtoWithCourses();
        
        await SendAsync(studentCoursesDto, cancellation: ct);
    }
}