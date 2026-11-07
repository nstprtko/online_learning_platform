using OnlineLearningPlatform.Enrollment.Application.Interfaces;
using EnrollmentEntity = OnlineLearningPlatform.Enrollment.Domain.Enrollments.Enrollment;

namespace OnlineLearningPlatform.Enrollment.Application.Enrollments;

public sealed class EnrollStudentService
{
    private readonly IEnrollmentRepository _repository;

    public EnrollStudentService(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<EnrollmentEntity> ExecuteAsync(
        Guid studentId,
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var enrollment = new EnrollmentEntity(studentId, courseId);
        await _repository.AddAsync(enrollment, cancellationToken);
        return enrollment;
    }
}