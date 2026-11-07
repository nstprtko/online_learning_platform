using OnlineLearningPlatform.StudentAccount.Application.Interfaces;
using OnlineLearningPlatform.StudentAccount.Application.Students;
using OnlineLearningPlatform.StudentAccount.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IStudentRepository, InMemoryStudentRepository>();

builder.Services.AddScoped<RegisterStudentService>();
builder.Services.AddScoped<UpdateStudentProfileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();