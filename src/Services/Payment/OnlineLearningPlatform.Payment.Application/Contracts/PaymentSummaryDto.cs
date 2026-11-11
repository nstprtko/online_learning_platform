using System;

namespace OnlineLearningPlatform.Payment.Application.Contracts;

public record PaymentSummaryDto(Guid Id, Guid StudentId, decimal Amount, string Status, string CardIssuer);