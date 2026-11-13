using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class DeleteCourseService
{
    private readonly ICourseRepository _courseRepository;

    public DeleteCourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task ExecuteAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var deleted = await _courseRepository.DeleteAsync(
            courseId, cancellationToken);

        if (!deleted)
            throw new CourseNotFoundException(courseId);
    }
}