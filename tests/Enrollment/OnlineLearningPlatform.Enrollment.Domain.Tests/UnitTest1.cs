using EnrollmentEntity = OnlineLearningPlatform.Enrollment.Domain.Enrollments.Enrollment;

namespace OnlineLearningPlatform.Enrollment.Domain.Tests;

public class EnrollmentTests
{
    [Fact]
    public void Constructor_CreatesActiveEnrollment()
    {
        var enrollment = new EnrollmentEntity(Guid.NewGuid(), Guid.NewGuid());

        Assert.True(enrollment.IsActive);
        Assert.True(enrollment.CanAccessCourse());
    }

    [Fact]
    public void Cancel_DisablesCourseAccess()
    {
        var enrollment = new EnrollmentEntity(Guid.NewGuid(), Guid.NewGuid());

        enrollment.Cancel();

        Assert.False(enrollment.IsActive);
        Assert.False(enrollment.CanAccessCourse());
    }

    [Fact]
    public void Cancel_WhenAlreadyCancelled_Throws()
    {
        var enrollment = new EnrollmentEntity(Guid.NewGuid(), Guid.NewGuid());
        enrollment.Cancel();

        Assert.Throws<InvalidOperationException>(() => enrollment.Cancel());
    }

    [Fact]
    public void Constructor_RejectsEmptyStudentId()
    {
        Assert.Throws<ArgumentException>(
            () => new EnrollmentEntity(Guid.Empty, Guid.NewGuid()));
    }
}
