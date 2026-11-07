using EnrollmentEntity = OnlineLearningPlatform.Enrollment.Domain.Enrollments.Enrollment;

namespace OnlineLearningPlatform.Enrollment.Application.Interfaces;

public interface IEnrollmentRepository
{
    Task AddAsync(EnrollmentEntity enrollment, CancellationToken cancellationToken = default);
    Task<EnrollmentEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdateAsync(EnrollmentEntity enrollment, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<EnrollmentEntity>> GetByStudentIdAsync(Guid studentId, CancellationToken cancellationToken = default);
}