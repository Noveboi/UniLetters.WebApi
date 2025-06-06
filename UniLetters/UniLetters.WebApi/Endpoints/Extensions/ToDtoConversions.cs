using UniLetters.WebApi.Domain;
using UniLetters.WebApi.Endpoints.Dto;

namespace UniLetters.WebApi.Endpoints.Extensions;

public static class ToDtoConversions
{
    public static StudentDto ToDto(this Student student)
    {
        return new StudentDto(
            Fullname: student.Name,
            Am: student.Am,
            Average: double.Round(student.Grades.Select(grade => grade.Value).Average(), 2));
    }
}