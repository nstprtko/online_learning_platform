using OnlineLearningPlatform.Enrollment.Application.Interfaces;
using OnlineLearningPlatform.Enrollment.Domain.Exceptions;

namespace OnlineLearningPlatform.Enrollment.Application.Enrollments;

public sealed class CancelEnrollmentService
{
    private readonly IEnrollmentRepository _repository;

    public CancelEnrollmentService(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        Guid enrollmentId,
        CancellationToken cancellationToken = default)
    {
        var enrollment = await _repository.GetByIdAsync(enrollmentId, cancellationToken);

        if (enrollment is null)
            throw new EnrollmentNotFoundException(enrollmentId);

        enrollment.Cancel();
        await _repository.UpdateAsync(enrollment, cancellationToken);
    }
}