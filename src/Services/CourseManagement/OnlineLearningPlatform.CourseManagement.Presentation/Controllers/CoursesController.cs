using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.CourseManagement.Application.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Presentation.Contracts;

namespace OnlineLearningPlatform.CourseManagement.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CoursesController : ControllerBase
{
    private readonly CreateCourseService _createCourseService;
    private readonly SetCourseDescriptionService _setDescriptionService;
    private readonly AddCourseContentService _addContentService;
    private readonly PublishCourseService _publishCourseService;

    public CoursesController(
        CreateCourseService createCourseService,
        SetCourseDescriptionService setDescriptionService,
        AddCourseContentService addContentService,
        PublishCourseService publishCourseService)
    {
        _createCourseService = createCourseService;
        _setDescriptionService = setDescriptionService;
        _addContentService = addContentService;
        _publishCourseService = publishCourseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        Course course = await _createCourseService.ExecuteAsync(
            request.Title, cancellationToken);

        return Created($"/api/courses/{course.Id}", new
        {
            course.Id,
            course.Title,
            course.Description,
            course.IsPublished
        });
    }

    [HttpPut("{id:guid}/description")]
    public async Task<IActionResult> SetDescription(
        Guid id,
        [FromBody] SetCourseDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        await _setDescriptionService.ExecuteAsync(
            id, request.Description, cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:guid}/content")]
    public async Task<IActionResult> AddContent(
        Guid id,
        [FromBody] AddCourseContentRequest request,
        CancellationToken cancellationToken)
    {
        await _addContentService.ExecuteAsync(
            id, request.Title, cancellationToken);

        return NoContent();
    }

    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> Publish(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _publishCourseService.ExecuteAsync(id, cancellationToken);

        return NoContent();
    }
}