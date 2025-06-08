using FastEndpoints;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Services;

namespace UniLetters.WebApi.Endpoints;

public record CreateReferenceLetterRequest([property: RouteParam] string Am, [property: RouteParam] int LetterId);

public class CreateReferenceLetterEndpoint : Endpoint<CreateReferenceLetterRequest, StudentCoursesDto>
{
    public override void Configure()
    {
        Get("/students/{am}/letters/{letterId}");
        AllowAnonymous();
    }

    public override Task HandleAsync(CreateReferenceLetterRequest req, CancellationToken ct)
    {
        // temp
        var referenceLetter= new LettersService().CreateLetterAsync(req.LetterId, ct);
        return referenceLetter;
    }
}