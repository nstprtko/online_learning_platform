namespace OnlineLearningPlatform.Payment.Domain.Exceptions;

public class InvalidCardNumberException : DomainException
{
    public string CardNumber { get; }

    public InvalidCardNumberException(string cardNumber) 
        : base("invalid_card_number", $"The card number '{cardNumber}' fails checksum or length validation.")
    {
        CardNumber = cardNumber;
    }
}