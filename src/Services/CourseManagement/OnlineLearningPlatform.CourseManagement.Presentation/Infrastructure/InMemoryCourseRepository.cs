using System.Collections.Concurrent;
using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;

namespace OnlineLearningPlatform.CourseManagement.Presentation.Infrastructure;

public sealed class InMemoryCourseRepository : ICourseRepository
{
    private readonly ConcurrentDictionary<Guid, Course> _courses = new();

    public Task AddAsync(Course course, CancellationToken cancellationToken = default)
    {
        _courses[course.Id] = course;
        return Task.CompletedTask;
    }

    public Task<Course?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        _courses.TryGetValue(courseId, out var course);
        return Task.FromResult(course);
    }

    public Task UpdateAsync(Course course, CancellationToken cancellationToken = default)
    {
        _courses[course.Id] = course;
        return Task.CompletedTask;
    }
}