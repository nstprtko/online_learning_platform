namespace OnlineLearningPlatform.Enrollment.Domain.Enrollments;

public sealed class Enrollment
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public Guid CourseId { get; private set; }
    public bool IsActive { get; private set; }

    public Enrollment(Guid studentId, Guid courseId)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("Student ID cannot be empty.");

        if (courseId == Guid.Empty)
            throw new ArgumentException("Course ID cannot be empty.");

        Id = Guid.NewGuid();
        StudentId = studentId;
        CourseId = courseId;
        IsActive = true;
    }

    public void Cancel()
    {
        if (!IsActive)
            throw new InvalidOperationException("Enrollment is already cancelled.");

        IsActive = false;
    }

    public bool CanAccessCourse() => IsActive;
}