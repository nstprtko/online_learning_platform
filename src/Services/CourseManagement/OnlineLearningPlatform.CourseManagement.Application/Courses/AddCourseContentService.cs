using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class AddCourseContentService
{
    private readonly ICourseRepository _courseRepository;

    public AddCourseContentService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task ExecuteAsync(
        Guid courseId,
        string title,
        CancellationToken cancellationToken = default)
    {
        var course = await _courseRepository.GetByIdAsync(courseId, cancellationToken);

        if (course is null)
            throw new CourseNotFoundException(courseId);

        course.AddContent(new CourseContent(title));

        await _courseRepository.UpdateAsync(course, cancellationToken);
    }
}