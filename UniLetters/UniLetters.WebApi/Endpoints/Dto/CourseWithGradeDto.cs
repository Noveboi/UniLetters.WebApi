namespace UniLetters.WebApi.Endpoints.Dto;

public record CourseWithGradeDto(Guid Id, string Name,  int Semester, double Grade);