namespace UniLetters.WebApi.Domain;

public class Course
{
    private Course() => Id = Guid.CreateVersion7();
    public Course(string name) : this()
    {
        Name = name;
    }
    
    public Guid Id { get; private init; }
    public string Name { get; private init; } = null!;
}