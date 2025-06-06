namespace UniLetters.WebApi.Endpoints.Dto;

public record StudentCoursesDto(StudentDto Student, List<CourseWithGradeDto> Courses);