using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;

namespace OnlineLearningPlatform.CourseManagement.Application.Courses;

public sealed class CreateCourseService
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course> ExecuteAsync(
        string title,
        CancellationToken cancellationToken = default)
    {
        var course = new Course(title);

        await _courseRepository.AddAsync(course, cancellationToken);

        return course;
    }
}
