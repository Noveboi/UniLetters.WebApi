namespace UniLetters.WebApi.Domain;

public class Course
{
    private Course() => Id = Guid.CreateVersion7();
    public Course(string name, int semester) : this()
    {
        Name = name;
        Semester = semester;
    }
    
    public Guid Id { get; private init; }
    public string Name { get; private init; } = null!;
    public int Semester { get; private init; }
}