namespace OnlineLearningPlatform.Payment.Domain.Exceptions;

public class UnsupportedCardIssuerException : DomainException
{
    public UnsupportedCardIssuerException() 
        : base("unsupported_card_issuer", "The card issuer could not be identified or is not supported.")
    {
    }
}