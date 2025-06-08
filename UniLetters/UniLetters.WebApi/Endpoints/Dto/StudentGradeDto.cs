namespace UniLetters.WebApi.Endpoints.Dto;

public record StudentGradeDto(Guid CourseId, string Name, int Semester, double Grade);