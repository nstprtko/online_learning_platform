using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OnlineLearningPlatform.Payment.Application;
using OnlineLearningPlatform.Payment.Application.Contracts;
using OnlineLearningPlatform.Payment.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddSingleton<IPaymentRepository, InMemoryPaymentRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();