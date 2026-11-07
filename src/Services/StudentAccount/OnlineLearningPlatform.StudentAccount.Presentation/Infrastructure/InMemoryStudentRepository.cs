using System.Collections.Concurrent;
using OnlineLearningPlatform.StudentAccount.Application.Interfaces;
using OnlineLearningPlatform.StudentAccount.Domain.Students;

namespace OnlineLearningPlatform.StudentAccount.Presentation.Infrastructure;

public sealed class InMemoryStudentRepository : IStudentRepository
{
    private readonly ConcurrentDictionary<Guid, Student> _students = new();

    public Task AddAsync(Student student, CancellationToken cancellationToken = default)
    {
        _students[student.Id] = student;
        return Task.CompletedTask;
    }

    public Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _students.TryGetValue(id, out var student);
        return Task.FromResult(student);
    }

    public Task UpdateAsync(Student student, CancellationToken cancellationToken = default)
    {
        _students[student.Id] = student;
        return Task.CompletedTask;
    }
}