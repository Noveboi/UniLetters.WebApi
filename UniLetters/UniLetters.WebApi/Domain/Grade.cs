namespace UniLetters.WebApi.Domain;

public class Grade
{
    private Grade() { }
    public Grade(string studentId, Guid courseId, double value)
    {
        if (value is < 0 or > 10)
            throw new ArgumentOutOfRangeException(nameof(value), "Grade must be between 0 and 10");
        
        StudentId = studentId;
        CourseId = courseId;
        Value = value;
    }
    
    public double Value { get; private init; }
    
    public string StudentId { get; private init; } = null!;
    public Student Student { get; private init; } = null!;
    
    public Guid CourseId { get; private init; }
    public Course Course { get; private init; } = null!;
}