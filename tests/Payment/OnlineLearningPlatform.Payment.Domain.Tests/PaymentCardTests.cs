using OnlineLearningPlatform.Payment.Domain.Exceptions;
using OnlineLearningPlatform.Payment.Domain.ValueObjects;
using Xunit;

namespace OnlineLearningPlatform.Payment.Domain.Tests;

public class PaymentCardTests
{
    [Theory]
    [InlineData("3497 7965 8312 797")]
    [InlineData("4024-0071-6540-1778")]
    [InlineData("5530016454538418")]
    public void Create_ValidCardNumber_CreatesSuccessfully(string cardNumber)
    {
        var card = new PaymentCard(cardNumber);
        Assert.NotNull(card);
        Assert.Equal(cardNumber.Replace(" ", "").Replace("-", ""), card.Number);
    }

    [Theory]
    [InlineData("12345")]                 
    [InlineData("123456789012345678901")] 
    [InlineData("4024-0071-ABCD-1778")]   
    [InlineData("4024-0071-6540-1779")]   
    [InlineData("")]  
    public void Create_InvalidCardNumber_ThrowsInvalidCardNumberException(string cardNumber)
    {
        Assert.Throws<InvalidCardNumberException>(() => new PaymentCard(cardNumber));
    }

    [Theory]
    [InlineData("4024-0071-6540-1778", "Visa")]
    [InlineData("5530016454538418", "MasterCard")]
    [InlineData("3497 7965 8312 797", "American Express")]
    public void Issuer_ValidCardNumber_ReturnsCorrectIssuer(string cardNumber, string expectedIssuer)
    {
        var card = new PaymentCard(cardNumber);
        Assert.Equal(expectedIssuer, card.Issuer);
    }
    
    [Fact]
    public void Create_UnsupportedCard_ThrowsUnsupportedCardIssuerException()
    {
        Assert.Throws<UnsupportedCardIssuerException>(() => new PaymentCard("0000000000000000"));
    }
}