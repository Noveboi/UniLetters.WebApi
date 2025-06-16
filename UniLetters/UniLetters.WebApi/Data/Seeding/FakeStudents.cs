using Bogus;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Data.Seeding;

internal static class FakeStudents
{
    private static readonly Faker<Student> Faker = new Faker<Student>(locale: "el")
        .RuleFor(x => x.Am, f =>
        {
            var year = f.Date.Past(10).Year.ToString()[2..];
            var number = string.Join(string.Empty, f.Random.Digits(3));

            return $"p{year}{number}";
        })
        .RuleFor(x => x.Name, f => f.Person.FullName);

    public static List<Student> Get(int count) => Faker.Generate(count);
}