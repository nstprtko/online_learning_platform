using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Enrollment.Application.Enrollments;
using OnlineLearningPlatform.Enrollment.Domain.Exceptions;
using OnlineLearningPlatform.Enrollment.Presentation.Contracts;

namespace OnlineLearningPlatform.Enrollment.Presentation.Controllers;

[ApiController]
[Route("api/enrollments")]
public sealed class EnrollmentsController : ControllerBase
{
    private readonly EnrollStudentService _enrollService;
    private readonly CancelEnrollmentService _cancelService;
    private readonly CheckCourseAccessService _accessService;

    public EnrollmentsController(
        EnrollStudentService enrollService,
        CancelEnrollmentService cancelService,
        CheckCourseAccessService accessService)
    {
        _enrollService = enrollService;
        _cancelService = cancelService;
        _accessService = accessService;
    }

    [HttpPost]
    public async Task<IActionResult> Enroll(
        EnrollStudentRequest request,
        CancellationToken cancellationToken)
    {
        var enrollment = await _enrollService.ExecuteAsync(
            request.StudentId, request.CourseId, cancellationToken);

        return Created($"/api/enrollments/{enrollment.Id}", enrollment);
    }

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _cancelService.ExecuteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (EnrollmentNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet("access")]
    public async Task<IActionResult> CheckAccess(
        [FromQuery] Guid studentId,
        [FromQuery] Guid courseId,
        CancellationToken cancellationToken)
    {
        var hasAccess = await _accessService.ExecuteAsync(
            studentId, courseId, cancellationToken);

        return Ok(new { hasAccess });
    }
}