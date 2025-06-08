using Microsoft.EntityFrameworkCore;
using UniLetters.WebApi.Data;
using UniLetters.WebApi.Domain;

namespace UniLetters.WebApi.Services;

public sealed class StudentService(UniLettersDbContext context)
{
    public Task<List<Student>> GetAllAsync(CancellationToken token)
    {
        return context.Students
            .Include(x => x.Grades)
            .ToListAsync(token);
    }
    
    public Task<Student?> GetAsync(string am, CancellationToken token)
    {
        return context.Students
            .Include(s => s.Grades)
            .ThenInclude(g => g.Course)
            .FirstOrDefaultAsync(s => s.Am == am, token);
    }
}