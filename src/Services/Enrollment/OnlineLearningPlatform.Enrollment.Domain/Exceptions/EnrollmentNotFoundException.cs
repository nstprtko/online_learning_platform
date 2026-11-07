namespace OnlineLearningPlatform.Enrollment.Domain.Exceptions;

public sealed class EnrollmentNotFoundException : Exception
{
    public EnrollmentNotFoundException(Guid enrollmentId)
        : base($"Enrollment with ID '{enrollmentId}' was not found.")
    {
    }
}