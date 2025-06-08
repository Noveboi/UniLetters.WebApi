namespace UniLetters.WebApi.Endpoints.Dto;

public record StudentCoursesDto(StudentDto Student, IEnumerable<CourseWithGradeDto> Courses);