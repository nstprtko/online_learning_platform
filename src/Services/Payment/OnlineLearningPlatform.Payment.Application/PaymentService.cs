using System;
using OnlineLearningPlatform.Payment.Application.Contracts;
using OnlineLearningPlatform.Payment.Domain.Aggregates;
using OnlineLearningPlatform.Payment.Domain.ValueObjects;

namespace OnlineLearningPlatform.Payment.Application;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;

    public PaymentService(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public Guid InitiatePayment(Guid studentId, decimal amount, string cardNumber)
    {
        var card = new PaymentCard(cardNumber);
        
        var payment = new PaymentPurchase(studentId, amount, card);
        
        _repository.Add(payment);
        
        return payment.Id;
    }

    public void ProcessPayment(Guid paymentId)
    {
        var payment = _repository.GetById(paymentId) 
            ?? throw new Exception("Payment not found.");
        
        payment.ProcessPayment();
        
        _repository.Update(payment);
    }

    public PaymentSummaryDto GetPaymentSummary(Guid paymentId)
    {
        var payment = _repository.GetById(paymentId) 
            ?? throw new Exception("Payment not found.");

        return new PaymentSummaryDto(
            payment.Id,
            payment.StudentId,
            payment.Amount,
            payment.Status,
            payment.CardDetails.Issuer
        );
    }
}