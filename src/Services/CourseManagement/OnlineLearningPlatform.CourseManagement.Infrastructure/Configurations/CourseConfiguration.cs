
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;

namespace OnlineLearningPlatform.CourseManagement.Infrastructure.Configurations;

public sealed class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(course => course.Id);

        builder.Property(course => course.Id)
            .ValueGeneratedNever();

        builder.Property(course => course.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(course => course.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(course => course.IsPublished)
            .IsRequired();

        builder.OwnsMany(course => course.Content, content =>
        {
            content.ToTable("CourseContents");

            content.WithOwner()
                .HasForeignKey("CourseId");

            content.HasKey(item => item.Id);

            content.Property(item => item.Id)
                .ValueGeneratedNever();

            content.Property(item => item.Title)
                .IsRequired()
                .HasMaxLength(200);
        });

        builder.Navigation(course => course.Content)
            .HasField("_content")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
