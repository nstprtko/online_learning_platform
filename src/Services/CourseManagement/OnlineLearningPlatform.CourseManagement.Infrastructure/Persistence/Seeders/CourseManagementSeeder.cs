
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence;

namespace OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence.Seeders;

public sealed class CourseManagementSeeder
{
    private readonly IServiceProvider _serviceProvider;

    public CourseManagementSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task SeedAsync(
        CancellationToken cancellationToken = default)
    {
        var context = _serviceProvider
            .GetRequiredService<CourseManagementDbContext>();

        if (context.Database.ProviderName ==
            "Microsoft.EntityFrameworkCore.SqlServer")
        {
            await context.Database.MigrateAsync(cancellationToken);
        }
        else
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        if (await context.Courses.AnyAsync(cancellationToken))
            return;

        // Course 1: A complete course that can be published.
        var publishedCourse = new Course("Introduction to C#");

        publishedCourse.SetDescription(
            "Learn the fundamentals of C# programming.");

        publishedCourse.AddContent(
            new CourseContent("Variables and data types"));

        publishedCourse.AddContent(
            new CourseContent("Classes and objects"));

        publishedCourse.Publish();

        // Course 2: An incomplete draft for testing validation.
        var draftCourse = new Course("Draft course");

        context.Courses.AddRange(publishedCourse, draftCourse);

        await context.SaveChangesAsync(cancellationToken);
    }
}
