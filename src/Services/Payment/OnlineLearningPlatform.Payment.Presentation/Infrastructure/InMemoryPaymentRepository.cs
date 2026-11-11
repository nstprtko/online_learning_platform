using System;
using System.Collections.Concurrent;
using OnlineLearningPlatform.Payment.Application.Contracts;
using OnlineLearningPlatform.Payment.Domain.Aggregates;

namespace OnlineLearningPlatform.Payment.Presentation.Infrastructure;

public class InMemoryPaymentRepository : IPaymentRepository
{
    private readonly ConcurrentDictionary<Guid, PaymentPurchase> _database = new();

    public void Add(PaymentPurchase payment)
    {
        _database[payment.Id] = payment;
    }

    public PaymentPurchase GetById(Guid id)
    {
        _database.TryGetValue(id, out var payment);
        return payment; 
    }

    public void Update(PaymentPurchase payment)
    {
        _database[payment.Id] = payment;
    }
}