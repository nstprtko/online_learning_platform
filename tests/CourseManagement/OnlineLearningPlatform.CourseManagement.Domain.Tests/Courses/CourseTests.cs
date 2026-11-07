using Xunit;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

namespace OnlineLearningPlatform.CourseManagement.Domain.Tests.Courses;

public class CourseTests
{
    [Fact]
    public void PublishCourse_WithDescriptionAndContent_ShouldPublishCourse()
    {
        var course = new Course("C# Fundamentals");

        course.SetDescription("An introductory C# course.");
        course.AddContent(new CourseContent("Introduction"));

        course.Publish();

        Assert.True(course.IsPublished);
    }

    [Fact]
    public void PublishCourse_WithoutDescription_ShouldThrowException()
    {
        var course = new Course("C# Fundamentals");
        course.AddContent(new CourseContent("Introduction"));

        Assert.Throws<CourseCannotBePublishedException>(
            () => course.Publish());
    }

    [Fact]
    public void PublishCourse_WithoutContent_ShouldThrowException()
    {
        var course = new Course("C# Fundamentals");
        course.SetDescription("An introductory C# course.");

        Assert.Throws<CourseCannotBePublishedException>(
            () => course.Publish());
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void PublishCourse_WithEmptyDescription_ShouldThrowException(
        string description)
    {
        var course = new Course("C# Fundamentals");
        course.SetDescription(description);
        course.AddContent(new CourseContent("Introduction"));

        Assert.Throws<CourseCannotBePublishedException>(
            () => course.Publish());
    }

    [Fact]
    public void AddContent_ShouldIncreaseContentCount()
    {
        var course = new Course("C# Fundamentals");

        course.AddContent(new CourseContent("Introduction"));

        Assert.Single(course.Content);
    }

    [Fact]
    public void NewCourse_ShouldNotBePublished()
    {
        var course = new Course("C# Fundamentals");

        Assert.False(course.IsPublished);
    }
    [Fact]
public void PublishCourse_WhenAlreadyPublished_ShouldThrowException()
{
    var course = new Course("C# Fundamentals");

    course.SetDescription("An introductory C# course.");
    course.AddContent(new CourseContent("Introduction"));
    course.Publish();

    Assert.Throws<CourseCannotBePublishedException>(
        () => course.Publish());
}
}

