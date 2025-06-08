using QuestPDF.Companion;
using QuestPDF.Fluent;

namespace UniLetters.WebApi.Services;

public sealed class LettersService
{
    public async Task<byte[]> CreateLetterAsync(int letterId, CancellationToken token)
    {
        var doc = Document.Create(document =>
        {
            
        });

        await doc.ShowInCompanionAsync(cancellationToken: token);
        return [];
    }
}