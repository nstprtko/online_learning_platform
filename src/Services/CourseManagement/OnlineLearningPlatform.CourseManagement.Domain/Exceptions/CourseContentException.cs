namespace OnlineLearningPlatform.CourseManagement.Domain.Exceptions;

public sealed class CourseContentException : DomainException
{
    public CourseContentException(string reason)
        : base($"Invalid course content: {reason}")
    {
    }
}