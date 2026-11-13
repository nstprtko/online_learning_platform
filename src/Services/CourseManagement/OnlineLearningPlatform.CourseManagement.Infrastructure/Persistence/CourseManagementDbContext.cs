
using Microsoft.EntityFrameworkCore;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;

namespace OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence;

internal sealed class CourseManagementDbContext : DbContext
{
    public CourseManagementDbContext(
        DbContextOptions<CourseManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Course> Courses => Set<Course>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CourseManagementDbContext).Assembly);
    }
}

