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
            .Where(s => s.Am == req.Am)
            .SingleOrDefaultAsync(ct);
        if (student is null)
            await SendNotFoundAsync();
        else
        {
            List<CourseWithGradeDto> courses = new List<CourseWithGradeDto>();
            var grades = student.Grades;
            foreach (var grade in grades)
            {
                var course = dbContext.Courses
                    .Where(c => c.Id == grade.CourseId)
                    .SingleOrDefaultAsync(ct)
                    .Result;
                var courseWithGradeDto = course!.ToDto(grade.Value);
                courses.Add(courseWithGradeDto);
            }
            var studentDto = student.ToDto();
            var studentCoursesDto = studentDto.ToDto(courses);
            await SendAsync(studentCoursesDto);
        }
    }
}