using FluentResults;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Services;

public sealed class LettersService
{
    public static Result<byte[]> CreateLetter(Student student, int letterId)
    {
        if (!Styles.TryGetValue(letterId, out var style))
        {
            return Result.Fail($"Letter ID '{letterId}' is not valid. Try {string.Join(", ", Styles.Keys)}");
        }
        
        var doc = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(14));
                
                page.Header()
                    .Element(style)
                    .Text("Letter of Reference")
                    .FontSize(48)
                    .Bold();
                
                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.Item().Text(text =>
                        {
                            text.DefaultTextStyle(s => s.FontSize(20));
                            text.Span("Concerning the student: ");
                            text.Span(student.Name).SemiBold().Underline();
                            text.Span(" with ID: ");
                            text.Span(student.Am).SemiBold();
                        });

                        x.Item().Text($"I have had the pleasure of working with {student.Name} during their time at " +
                                      "University of Piraeus, where they have consistently demonstrated strong academic performance, " +
                                      "dedication, and a genuine enthusiasm for learning. In my experience, they " +
                                      "have shown the ability to grasp complex concepts quickly and apply them thoughtfully, " +
                                      $"both in individual assignments and group projects. {student.Name} stands out for their " +
                                      "analytical skills, professionalism, and collaborative spirit, making them an " +
                                      "excellent candidate for further academic or professional opportunities.");
                    });
            });
        });
        
        return doc.GeneratePdf();
    }

    private static readonly Dictionary<int, Func<IContainer, IContainer>> Styles = new()
    {
        [1] = container => container.DefaultTextStyle(x => x.FontColor(Colors.Indigo.Medium)),
        [2] = container => container.DefaultTextStyle(x => x.FontColor(Colors.Green.Medium))
    };
}