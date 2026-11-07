using Moq;
using OnlineLearningPlatform.CourseManagement.Application.Courses;
using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Application.Tests.Courses;

public class CourseApplicationServiceTests
{
    private readonly Mock<ICourseRepository> _repositoryMock = new();

    [Fact]
    public async Task CreateCourse_ShouldSaveCourseInRepository()
    {
        var service = new CreateCourseService(_repositoryMock.Object);

        var course = await service.ExecuteAsync("C# Fundamentals");

        Assert.Equal("C# Fundamentals", course.Title);

        _repositoryMock.Verify(
            repository => repository.AddAsync(
                It.IsAny<Course>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task CreateCourse_WithInvalidTitle_ShouldNotSaveCourse()
    {
        var service = new CreateCourseService(_repositoryMock.Object);

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.ExecuteAsync(" "));

        _repositoryMock.Verify(
            repository => repository.AddAsync(
                It.IsAny<Course>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task PublishCourse_ShouldUpdateCourseInRepository()
    {
        var course = new Course("C# Fundamentals");
        course.SetDescription("An introductory C# course.");
        course.AddContent(new CourseContent("Introduction"));

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                course.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        var service = new PublishCourseService(_repositoryMock.Object);

        await service.ExecuteAsync(course.Id);

        Assert.True(course.IsPublished);

        _repositoryMock.Verify(
            repository => repository.UpdateAsync(
                course,
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task PublishCourse_WhenCourseDoesNotExist_ShouldThrowException()
    {
        var courseId = Guid.NewGuid();

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                courseId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Course?)null);

        var service = new PublishCourseService(_repositoryMock.Object);

        await Assert.ThrowsAsync<CourseNotFoundException>(
            () => service.ExecuteAsync(courseId));

        _repositoryMock.Verify(
            repository => repository.UpdateAsync(
                It.IsAny<Course>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task PublishCourse_WhenCourseIsIncomplete_ShouldNotUpdateRepository()
    {
        var course = new Course("C# Fundamentals");

        _repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                course.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(course);

        var service = new PublishCourseService(_repositoryMock.Object);

        await Assert.ThrowsAsync<CourseCannotBePublishedException>(
            () => service.ExecuteAsync(course.Id));

        _repositoryMock.Verify(
            repository => repository.UpdateAsync(
                It.IsAny<Course>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }
}