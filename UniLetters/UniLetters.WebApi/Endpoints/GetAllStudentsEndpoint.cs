using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Endpoints.Extensions;

namespace UniLetters.WebApi.Endpoints;

public class GetAllStudentsEndpoint(UniLettersDbContext dbContext) 
    : EndpointWithoutRequest<IEnumerable<StudentDto>>
{
    public override void Configure()
    {
        Get("/students");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var students = await dbContext.Students
            .Include(s => s.Grades)
            .ToListAsync(ct);
        
        await SendAsync(
            response: students.Select(student => student.ToDto()), 
            cancellation: ct);
    }
}