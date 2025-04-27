using FastEndpoints;

namespace UniLetters.WebApi.Endpoints;

internal sealed class TestingEndpoint : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct) => await SendAsync("Hello!", cancellation: ct);
}