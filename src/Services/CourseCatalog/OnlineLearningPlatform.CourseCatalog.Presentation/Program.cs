using OnlineLearningPlatform.CourseCatalog.Application.Catalog;
using OnlineLearningPlatform.CourseCatalog.Application.Interfaces;
using OnlineLearningPlatform.CourseCatalog.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<ICourseListingRepository, InMemoryCourseListingRepository>();

builder.Services.AddScoped<CreateCourseListingService>();
builder.Services.AddScoped<SearchCoursesService>();
builder.Services.AddScoped<ToggleCourseFavouriteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();