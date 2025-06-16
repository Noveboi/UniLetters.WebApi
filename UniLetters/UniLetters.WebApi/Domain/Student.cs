namespace UniLetters.WebApi.Domain;

public class Student
{
    public Student() { }
    public Student(string am, string name)
    {
        Am = am;
        Name = name;
    }

    public string Am { get; private init; } = null!;
    public string Name { get; private init; } = null!;
    public List<Grade> Grades { get; private init; } = [];
}