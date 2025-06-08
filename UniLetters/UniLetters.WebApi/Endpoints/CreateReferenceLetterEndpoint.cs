using FastEndpoints;
using UniLetters.WebApi.Endpoints.Dto;
using UniLetters.WebApi.Services;

namespace UniLetters.WebApi.Endpoints;

public record CreateReferenceLetterRequest([property: RouteParam] string Am, [property: RouteParam] int LetterId);

public class CreateReferenceLetterEndpoint : Endpoint<CreateReferenceLetterRequest, StudentCoursesDto>
{
    private readonly LettersService _lettersService;

    public CreateReferenceLetterEndpoint(LettersService lettersService)
    {
        _lettersService = lettersService;
    }

    public override void Configure()
    {
        Get("/students/{am}/letters/{letterId:int}");
        AllowAnonymous();
    }

    public override Task HandleAsync(CreateReferenceLetterRequest req, CancellationToken ct)
    {
        var referenceLetter = _lettersService.CreateLetterAsync(req.LetterId, ct);
        return referenceLetter;
    }
}