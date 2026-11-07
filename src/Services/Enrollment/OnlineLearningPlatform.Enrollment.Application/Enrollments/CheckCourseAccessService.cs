using OnlineLearningPlatform.Enrollment.Application.Interfaces;

namespace OnlineLearningPlatform.Enrollment.Application.Enrollments;

public sealed class CheckCourseAccessService
{
    private readonly IEnrollmentRepository _repository;

    public CheckCourseAccessService(IEnrollmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(
        Guid studentId,
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var enrollments = await _repository.GetByStudentIdAsync(
            studentId, cancellationToken);

        return enrollments.Any(e => e.CourseId == courseId && e.CanAccessCourse());
    }
}