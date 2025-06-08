using FastEndpoints;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Services;

namespace UniLetters.WebApi.Endpoints;

public record CreateReferenceLetterRequest(
    [property: RouteParam] string Am,
    [property: RouteParam] int LetterId);

public class CreateReferenceLetterEndpoint(StudentService students) 
    : Endpoint<CreateReferenceLetterRequest, StudentGradesDto>
{
    public override void Configure()
    {
        Get("/students/{am:am}/letters/{letterId:int}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateReferenceLetterRequest req, CancellationToken ct)
    {
        if (await students.GetAsync(req.Am, ct) is not { } student)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var pdf = LettersService.CreateLetter(student, req.LetterId);
        
        await SendBytesAsync(
            bytes: pdf,
            fileName: $"{student.Am}_letter.pdf",
            contentType: "application/pdf",
            cancellation: ct);
    }
}