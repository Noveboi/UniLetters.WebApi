namespace UniLetters.WebApi.Services;

public class LettersService
{
    public Task<byte[]> CreateLetterAsync(int letterId, CancellationToken token)
    {
        return Task.FromResult(new byte[] { });
    }
}