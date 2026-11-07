using System.Collections.Concurrent;
using OnlineLearningPlatform.Enrollment.Application.Interfaces;
using EnrollmentEntity = OnlineLearningPlatform.Enrollment.Domain.Enrollments.Enrollment;

namespace OnlineLearningPlatform.Enrollment.Presentation.Infrastructure;

public sealed class InMemoryEnrollmentRepository : IEnrollmentRepository
{
    private readonly ConcurrentDictionary<Guid, EnrollmentEntity> _enrollments = new();

    public Task AddAsync(EnrollmentEntity enrollment, CancellationToken cancellationToken = default)
    {
        _enrollments[enrollment.Id] = enrollment;
        return Task.CompletedTask;
    }

    public Task<EnrollmentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _enrollments.TryGetValue(id, out var enrollment);
        return Task.FromResult(enrollment);
    }

    public Task UpdateAsync(EnrollmentEntity enrollment, CancellationToken cancellationToken = default)
    {
        _enrollments[enrollment.Id] = enrollment;
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<EnrollmentEntity>> GetByStudentIdAsync(
        Guid studentId,
        CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<EnrollmentEntity> result = _enrollments.Values
            .Where(e => e.StudentId == studentId)
            .ToList();

        return Task.FromResult(result);
    }
}