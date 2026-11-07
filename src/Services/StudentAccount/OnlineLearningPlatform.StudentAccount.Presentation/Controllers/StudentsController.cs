using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.StudentAccount.Application.Students;
using OnlineLearningPlatform.StudentAccount.Domain.Exceptions;
using OnlineLearningPlatform.StudentAccount.Presentation.Contracts;

namespace OnlineLearningPlatform.StudentAccount.Presentation.Controllers;

[ApiController]
[Route("api/students")]
public sealed class StudentsController : ControllerBase
{
    private readonly RegisterStudentService _registerService;
    private readonly UpdateStudentProfileService _updateService;

    public StudentsController(
        RegisterStudentService registerService,
        UpdateStudentProfileService updateService)
    {
        _registerService = registerService;
        _updateService = updateService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterStudentRequest request,
        CancellationToken cancellationToken)
    {
        var student = await _registerService.ExecuteAsync(
            request.Name, request.Email, cancellationToken);

        return Created($"/api/students/{student.Id}", student);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProfile(
        Guid id,
        UpdateStudentProfileRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _updateService.ExecuteAsync(
                id, request.Name, request.Email, cancellationToken);

            return NoContent();
        }
        catch (StudentNotFoundException)
        {
            return NotFound();
        }
    }
}