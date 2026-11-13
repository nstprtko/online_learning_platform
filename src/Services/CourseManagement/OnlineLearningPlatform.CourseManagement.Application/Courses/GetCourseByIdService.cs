using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class GetCourseByIdService
{
    private readonly ICourseRepository _courseRepository;

    public GetCourseByIdService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course> ExecuteAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var course = await _courseRepository.GetByIdAsync(
            courseId, cancellationToken);

        if (course is null)
            throw new CourseNotFoundException(courseId);

        return course;
    }
}