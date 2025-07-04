using Microsoft.AspNetCore.Http.Features;
using UniLetters.WebApi.Domain;
using UniLetters.WebApi.Endpoints.Dto;

namespace UniLetters.WebApi.Endpoints.Extensions;

public static class ToDtoConversions
{
    public static StudentDto ToDto(this Student student)
    {
        return new StudentDto(
            FullName: student.Name,
            Am: student.Am,
            Average: double.Round(student.Grades
                .Select(grade => grade.Value)
                .DefaultIfEmpty(0)
                .Average(), 2));
    }

    public static StudentGradeDto ToDto(this Grade grade)
    {
        if (grade.Course == null)
        {
            throw new InvalidOperationException("Grade's course is null! Make sure to join!");
        }

        return new StudentGradeDto(
            CourseId: grade.CourseId,
            Name: grade.Course.Name,
            Semester: grade.Course.Semester,
            Grade: grade.Value);
    }

    public static StudentGradesDto ToDtoWithCourses(this Student student)
    {
        return new StudentGradesDto(
            Student: student.ToDto(),
            Grades: student.Grades.Select(x => x.ToDto()));
    }
}