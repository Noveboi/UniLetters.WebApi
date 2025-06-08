using Microsoft.AspNetCore.Http.Features;
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

    public static CourseWithGradeDto ToDto(this Grade grade)
    {
        if (grade.Course == null)
        {
            throw new InvalidOperationException("Grade's course is null! Make sure to join!");
        }

        return new CourseWithGradeDto(
            CourseId: grade.CourseId,
            Name: grade.Course.Name,
            Semester: grade.Course.Semester,
            Grade: grade.Value);
    }
    
    public static CourseWithGradeDto ToDto(this Course course, double grade)
    {
        return new CourseWithGradeDto(
            CourseId: course.Id,
            Name: course.Name,
            Semester: course.Semester,
            Grade: grade);
    }
    
    public static StudentCoursesDto ToDtoWithCourses(this Student student)
    {
        return new StudentCoursesDto(
            Student: student.ToDto(),
            Courses: student.Grades.Select(x => x.ToDto()));
    }
}