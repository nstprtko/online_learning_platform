using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.CourseCatalog.Application.Catalog;
using OnlineLearningPlatform.CourseCatalog.Presentation.Contracts;

namespace OnlineLearningPlatform.CourseCatalog.Presentation.Controllers;

[ApiController]
[Route("api/catalog")]
public sealed class CourseCatalogController : ControllerBase
{
    private readonly CreateCourseListingService _createService;
    private readonly SearchCoursesService _searchService;
    private readonly ToggleCourseFavouriteService _favouriteService;

    public CourseCatalogController(
        CreateCourseListingService createService,
        SearchCoursesService searchService,
        ToggleCourseFavouriteService favouriteService)
    {
        _createService = createService;
        _searchService = searchService;
        _favouriteService = favouriteService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateCourseListingRequest request,
        CancellationToken cancellationToken)
    {
        var listing = await _createService.ExecuteAsync(
            request.CourseId,
            request.Title,
            request.Description,
            cancellationToken);

        return Created($"/api/catalog/{listing.Id}", listing);
    }

    [HttpGet]
    public async Task<IActionResult> Search(
        [FromQuery] string searchTerm = "",
        CancellationToken cancellationToken = default)
    {
        var listings = await _searchService.ExecuteAsync(
            searchTerm, cancellationToken);

        return Ok(listings);
    }

    [HttpPost("{id:guid}/favourite")]
    public async Task<IActionResult> ToggleFavourite(
        Guid id,
        CancellationToken cancellationToken)
    {
        try
        {
            await _favouriteService.ExecuteAsync(id, cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}