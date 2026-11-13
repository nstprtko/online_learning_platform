
using OnlineLearningPlatform.CourseManagement.Domain.Courses;

namespace OnlineLearningPlatform.CourseManagement.Application.Interfaces;

public interface ICourseRepository
{
    Task AddAsync(
        Course course,
        CancellationToken cancellationToken = default);

    Task<Course?> GetByIdAsync(
        Guid courseId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Course>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Course course,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(
        Guid courseId,
        CancellationToken cancellationToken = default);
}
