using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Endpoints.Extensions;
using UniLetters.WebApi.Services;

namespace UniLetters.WebApi.Endpoints;

public record GetStudentRequest([property: RouteParam] string Am);

public class GetStudentEndpoint(StudentService service) : Endpoint<GetStudentRequest, StudentGradesDto>
{
    public override void Configure()
    {
        Get("/students/{am:am}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetStudentRequest req, CancellationToken ct)
    {
        var student = await service.GetAsync(req.Am, ct);

        if (student is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        var studentCoursesDto = student.ToDtoWithCourses();
        
        await SendAsync(studentCoursesDto, cancellation: ct);
    }
}