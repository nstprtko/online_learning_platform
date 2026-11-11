using System;
using OnlineLearningPlatform.Payment.Domain.ValueObjects;
using OnlineLearningPlatform.Payment.Domain.Exceptions;

namespace OnlineLearningPlatform.Payment.Domain.Aggregates;

public class PaymentPurchase
{
    public Guid Id { get; private set; }
    public Guid StudentId { get; private set; }
    public decimal Amount { get; private set; }
    public PaymentCard CardDetails { get; private set; }
    public string Status { get; private set; }

    public PaymentPurchase(Guid studentId, decimal amount, PaymentCard cardDetails)
    {
        if (amount <= 0) 
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }
        
        Id = Guid.NewGuid();
        StudentId = studentId;
        Amount = amount;
        CardDetails = cardDetails ?? throw new ArgumentNullException(nameof(cardDetails));
        Status = "Initiated";
    }

    public void ProcessPayment()
    {
        if (Status != "Initiated") 
        {
            throw new PaymentProcessingException(Id, "Payment has already been processed.");
        }
        Status = "Completed";
    }
}