using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class SetCourseDescriptionService
{
    private readonly ICourseRepository _courseRepository;

    public SetCourseDescriptionService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task ExecuteAsync(
        Guid courseId,
        string description,
        CancellationToken cancellationToken = default)
    {
        var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);

        if (course is null)
            throw new CourseNotFoundException(courseId);

        course.SetDescription(description);

        await _courseRepository.UpdateAsync(course, cancellationToken);
    }
}