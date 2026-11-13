
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.CourseManagement.Application.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Courses;
using OnlineLearningPlatform.CourseManagement.Domain.Exceptions;
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
    private readonly GetAllCoursesService _getAllCoursesService;
    private readonly GetCourseByIdService _getCourseByIdService;
    private readonly DeleteCourseService _deleteCourseService;

    public CoursesController(
        CreateCourseService createCourseService,
        SetCourseDescriptionService setDescriptionService,
        AddCourseContentService addContentService,
        PublishCourseService publishCourseService,
        GetAllCoursesService getAllCoursesService,
        GetCourseByIdService getCourseByIdService,
        DeleteCourseService deleteCourseService)
    {
        _createCourseService = createCourseService;
        _setDescriptionService = setDescriptionService;
        _addContentService = addContentService;
        _publishCourseService = publishCourseService;
        _getAllCoursesService = getAllCoursesService;
        _getCourseByIdService = getCourseByIdService;
        _deleteCourseService = deleteCourseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseRequest request,
        CancellationToken cancellationToken)
    {
        try
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
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var courses = await _getAllCoursesService.ExecuteAsync(
            cancellationToken);

        return Ok(courses.Select(course => new
        {
            course.Id,
            course.Title,
            course.Description,
            course.IsPublished,
            Content = course.Content.Select(item => new
            {
                item.Id,
                item.Title
            })
        }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            var course = await _getCourseByIdService.ExecuteAsync(
                id, cancellationToken);

            return Ok(new
            {
                course.Id,
                course.Title,
                course.Description,
                course.IsPublished,
                Content = course.Content.Select(item => new
                {
                    item.Id,
                    item.Title
                })
            });
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("{id:guid}/description")]
    public async Task<IActionResult> SetDescription(
        Guid id,
        [FromBody] SetCourseDescriptionRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _setDescriptionService.ExecuteAsync(
                id, request.Description, cancellationToken);

            return NoContent();
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpPost("{id:guid}/content")]
    public async Task<IActionResult> AddContent(
        Guid id,
        [FromBody] AddCourseContentRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _addContentService.ExecuteAsync(
                id, request.Title, cancellationToken);

            return NoContent();
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
        catch (CourseContentException)
        {
            return BadRequest();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    [HttpPost("{id:guid}/publish")]
    public async Task<IActionResult> Publish(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _publishCourseService.ExecuteAsync(id, cancellationToken);

            return NoContent();
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
        catch (CourseCannotBePublishedException)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _deleteCourseService.ExecuteAsync(id, cancellationToken);

            return NoContent();
        }
        catch (CourseNotFoundException)
        {
            return NotFound();
        }
    }
}
