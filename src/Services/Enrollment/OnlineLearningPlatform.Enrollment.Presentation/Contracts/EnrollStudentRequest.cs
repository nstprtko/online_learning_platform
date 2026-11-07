namespace OnlineLearningPlatform.Enrollment.Presentation.Contracts;

public sealed record EnrollStudentRequest(Guid StudentId, Guid CourseId);