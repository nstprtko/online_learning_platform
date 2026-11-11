using System;
using Microsoft.AspNetCore.Mvc;
using OnlineLearningPlatform.Payment.Application.Contracts;
using OnlineLearningPlatform.Payment.Domain.Exceptions;

namespace OnlineLearningPlatform.Payment.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public record InitiatePaymentRequest(Guid StudentId, decimal Amount, string CardNumber);

    [HttpPost]
    public IActionResult InitiatePayment([FromBody] InitiatePaymentRequest request)
    {
        try
        {
            var paymentId = _paymentService.InitiatePayment(request.StudentId, request.Amount, request.CardNumber);
            return CreatedAtAction(nameof(GetSummary), new { id = paymentId }, new { Id = paymentId }); // 201 Created
        }
        catch (DomainException ex)
        {
            return UnprocessableEntity(new { error = ex.Code, message = ex.Message }); // 422 card number is invalid
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message }); // 400 Bad Request
        }
    }

    [HttpPost("{id}/process")]
    public IActionResult ProcessPayment(Guid id)
    {
        try
        {
            _paymentService.ProcessPayment(id);
            return Ok(); // 200 OK
        }
        catch (DomainException ex)
        {
            return UnprocessableEntity(new { error = ex.Code, message = ex.Message });
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message }); // 404 Not Found
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetSummary(Guid id)
    {
        try
        {
            var summary = _paymentService.GetPaymentSummary(id);
            return Ok(summary); // 200 OK
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message }); // 404 Not Found
        }
    }
}