using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class PublishCourseService
{
    private readonly ICourseRepository _courseRepository;

    public PublishCourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task ExecuteAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        Course? course = await _courseRepository.GetByIdAsync(
            courseId,
            cancellationToken);

        if (course is null)
            throw new CourseNotFoundException(courseId);

        course.Publish();

        await _courseRepository.UpdateAsync(course, cancellationToken);
    }
}