using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Endpoints.Extensions;
using UniLetters.WebApi.Services;

namespace UniLetters.WebApi.Endpoints;

public class GetAllStudentsEndpoint(StudentService service) : EndpointWithoutRequest<IEnumerable<StudentDto>>
{
    public override void Configure()
    {
        Get("/students");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var students = await service.GetAllAsync(ct);
        
        await SendAsync(
            response: students.Select(student => student.ToDto()), 
            cancellation: ct);
    }
}