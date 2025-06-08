namespace UniLetters.WebApi.Endpoints.Dto;

public record StudentGradesDto(StudentDto Student, IEnumerable<StudentGradeDto> Grades);