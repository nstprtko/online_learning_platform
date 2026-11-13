
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence;

namespace OnlineLearningPlatform.CourseManagement.Infrastructure.Repositories;

internal sealed class SqlCourseRepository : ICourseRepository
{
    private readonly CourseManagementDbContext _context;

    public SqlCourseRepository(CourseManagementDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Course course,
        CancellationToken cancellationToken = default)
    {
        await _context.Courses.AddAsync(course, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Course?> GetByIdAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .Include(course => course.Content)
            .FirstOrDefaultAsync(
                course => course.Id == courseId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Course>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Courses
            .AsNoTracking()
            .Include(course => course.Content)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Course course,
        CancellationToken cancellationToken = default)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(
        Guid courseId,
        CancellationToken cancellationToken = default)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(
                course => course.Id == courseId,
                cancellationToken);

        if (course is null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
