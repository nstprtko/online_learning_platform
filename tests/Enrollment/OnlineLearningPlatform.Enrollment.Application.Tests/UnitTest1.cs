using Moq;
using OnlineLearningPlatform.Enrollment.Application.Enrollments;
using OnlineLearningPlatform.Enrollment.Application.Interfaces;
using EnrollmentEntity = OnlineLearningPlatform.Enrollment.Domain.Enrollments.Enrollment;

namespace OnlineLearningPlatform.Enrollment.Application.Tests;

public class EnrollmentServiceTests
{
    [Fact]
    public async Task EnrollStudent_SavesEnrollmentToRepository()
    {
        var repository = new Mock<IEnrollmentRepository>();
        var service = new EnrollStudentService(repository.Object);
        var studentId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        var result = await service.ExecuteAsync(studentId, courseId);

        Assert.Equal(studentId, result.StudentId);
        Assert.Equal(courseId, result.CourseId);
        repository.Verify(
            r => r.AddAsync(
                It.Is<EnrollmentEntity>(e =>
                    e.StudentId == studentId && e.CourseId == courseId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
