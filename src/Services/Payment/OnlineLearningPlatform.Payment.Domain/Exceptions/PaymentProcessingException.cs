namespace OnlineLearningPlatform.Payment.Domain.Exceptions;

public class PaymentProcessingException : DomainException
{
    public Guid PaymentId { get; }

    public PaymentProcessingException(Guid paymentId, string reason) 
        : base("payment_processing_failed", $"Payment {paymentId} failed: {reason}")
    {
        PaymentId = paymentId;
    }
}