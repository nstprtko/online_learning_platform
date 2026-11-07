using OnlineLearningPlatform.StudentAccount.Domain.Students;

namespace OnlineLearningPlatform.StudentAccount.Application.Interfaces;

public interface IStudentRepository
{
    Task AddAsync(Student student, CancellationToken cancellationToken = default);
    Task<Student?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Student student, CancellationToken cancellationToken = default);
}