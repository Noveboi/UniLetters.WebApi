namespace UniLetters.WebApi.Endpoints.Dto;

public record CourseWithGradeDto(Guid CourseId, string Name, int Semester, double Grade);