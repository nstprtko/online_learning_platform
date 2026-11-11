using System.Linq;
using System.Text.RegularExpressions;
using OnlineLearningPlatform.Payment.Domain.Exceptions;

namespace OnlineLearningPlatform.Payment.Domain.ValueObjects;

public class PaymentCard
{
    public string Number { get; private set; }
    public string Issuer { get; private set; }

    public PaymentCard(string rawCardNumber)
    {
        if (!ValidateCard(rawCardNumber))
        {
            throw new InvalidCardNumberException(rawCardNumber ?? "null");
        }

        var issuer = GetCardType(rawCardNumber);
        if (issuer == "Unknown")
        {
            throw new UnsupportedCardIssuerException();
        }

        Number = rawCardNumber.Replace(" ", "").Replace("-", "");
        Issuer = issuer;
    }

    private bool ValidateCard(string cardNumber)
    {
        if (string.IsNullOrWhiteSpace(cardNumber)) return false;
        string cleaned = cardNumber.Replace(" ", "").Replace("-", "");
        if (cleaned.Length < 13 || cleaned.Length > 19) return false;
        if (!cleaned.All(char.IsDigit)) return false;

        int sum = 0;
        bool alternate = false;
        for (int i = cleaned.Length - 1; i >= 0; i--)
        {
            int digit = cleaned[i] - '0';
            if (alternate)
            {
                digit *= 2;
                if (digit > 9) digit -= 9;
            }
            sum += digit;
            alternate = !alternate;
        }
        return (sum % 10 == 0);
    }

    private string GetCardType(string cardNumber)
    {
        string cleaned = cardNumber.Replace(" ", "").Replace("-", "");
        if (Regex.IsMatch(cleaned, @"^4(\d{12}|\d{15}|\d{18})$")) return "Visa";
        if (Regex.IsMatch(cleaned, @"^(5[1-5]\d{14}|2(2[2-9][1-9]|2[3-9]\d{2}|[3-6]\d{3}|7([01]\d{2}|20\d))\d{10})$")) return "MasterCard";
        if (Regex.IsMatch(cleaned, @"^3[47]\d{13}$")) return "American Express";
        return "Unknown";
    }
}