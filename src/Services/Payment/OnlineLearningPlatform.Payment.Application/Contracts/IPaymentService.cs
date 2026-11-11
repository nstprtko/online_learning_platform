using System;

namespace OnlineLearningPlatform.Payment.Application.Contracts;

public interface IPaymentService
{
    Guid InitiatePayment(Guid studentId, decimal amount, string cardNumber);
    
    void ProcessPayment(Guid paymentId);
    
    PaymentSummaryDto GetPaymentSummary(Guid paymentId);
}