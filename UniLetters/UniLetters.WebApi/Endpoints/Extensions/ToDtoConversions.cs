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

    public static CourseWithGradeDto ToDto(this Course course, double grade)
    {
        return new CourseWithGradeDto(
            Id: course.Id,
            Name: course.Name,
            Semester: course.Semester,
            Grade: grade);
    }
    
    public static StudentCoursesDto ToDto(this StudentDto studentDto, List<CourseWithGradeDto> courses)
    {
        return new StudentCoursesDto(
            Student: studentDto,
            Courses: courses);
    }
}