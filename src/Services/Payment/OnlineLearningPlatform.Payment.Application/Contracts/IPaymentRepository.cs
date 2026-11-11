using System;
using OnlineLearningPlatform.Payment.Domain.Aggregates;

namespace OnlineLearningPlatform.Payment.Application.Contracts;

public interface IPaymentRepository
{
    void Add(PaymentPurchase payment);
    PaymentPurchase GetById(Guid id);
    void Update(PaymentPurchase payment);
}