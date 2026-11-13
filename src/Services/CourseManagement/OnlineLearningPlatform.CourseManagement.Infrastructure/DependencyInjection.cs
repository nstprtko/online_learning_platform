
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineLearningPlatform.CourseManagement.Application.Interfaces;
using OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence;
using OnlineLearningPlatform.CourseManagement.Infrastructure.Repositories;
using OnlineLearningPlatform.CourseManagement.Infrastructure.Persistence.Seeders;

namespace OnlineLearningPlatform.CourseManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = configuration["Persistence:Provider"];

        var connectionString =
            configuration["ConnectionStrings:DefaultConnection"];

        services.AddDbContext<CourseManagementDbContext>(options =>
        {
            if (string.Equals(
                provider,
                "InMemory",
                StringComparison.OrdinalIgnoreCase))
            {
                options.UseInMemoryDatabase("OnlineLearningPlatform");
            }
            else if (string.Equals(
                provider,
                "SqlServer",
                StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new InvalidOperationException(
                        "SQL Server connection string is missing.");
                }

                options.UseSqlServer(connectionString);
            }
            else
            {
                throw new InvalidOperationException(
                    $"Unsupported persistence provider: {provider}");
            }
        });

        services.AddScoped<ICourseRepository, SqlCourseRepository>();
        services.AddScoped<CourseManagementSeeder>();

        return services;
    }
}
