using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class GetAllCoursesService
{
    private readonly ICourseRepository _courseRepository;

    public GetAllCoursesService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public Task<IReadOnlyList<Course>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        return _courseRepository.GetAllAsync(cancellationToken);
    }
}