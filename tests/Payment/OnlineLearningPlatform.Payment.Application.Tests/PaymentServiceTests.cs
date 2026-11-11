using System;
using Moq;
using Xunit;
using OnlineLearningPlatform.Payment.Application.Contracts;
using OnlineLearningPlatform.Payment.Domain.Aggregates;
using OnlineLearningPlatform.Payment.Domain.ValueObjects;
using OnlineLearningPlatform.Payment.Domain.Exceptions;

namespace OnlineLearningPlatform.Payment.Application.Tests;

public class PaymentServiceTests
{
    private readonly Mock<IPaymentRepository> _repositoryMock;
    private readonly PaymentService _paymentService;

    public PaymentServiceTests()
    {
        _repositoryMock = new Mock<IPaymentRepository>();
        
        _paymentService = new PaymentService(_repositoryMock.Object);
    }

    [Fact]
    public void InitiatePayment_ValidData_SavesToRepositoryAndReturnsId()
    {
        var studentId = Guid.NewGuid();
        var amount = 100.00m;
        var validCard = "4024-0071-6540-1778";

        var paymentId = _paymentService.InitiatePayment(studentId, amount, validCard);

        Assert.NotEqual(Guid.Empty, paymentId);
        _repositoryMock.Verify(repo => repo.Add(It.IsAny<PaymentPurchase>()), Times.Once);
    }

    [Fact]
    public void ProcessPayment_ExistingPayment_UpdatesStatusAndSaves()
    {
        var card = new PaymentCard("4024-0071-6540-1778");
        var payment = new PaymentPurchase(Guid.NewGuid(), 50.0m, card);
        _repositoryMock.Setup(r => r.GetById(payment.Id)).Returns(payment);

        _paymentService.ProcessPayment(payment.Id);

        Assert.Equal("Completed", payment.Status);
        _repositoryMock.Verify(repo => repo.Update(payment), Times.Once);
    }

    [Fact]
    public void GetPaymentSummary_ExistingPayment_ReturnsCorrectDto()
    {
        var card = new PaymentCard("4024-0071-6540-1778");
        var payment = new PaymentPurchase(Guid.NewGuid(), 75.50m, card);
        _repositoryMock.Setup(r => r.GetById(payment.Id)).Returns(payment);

        var result = _paymentService.GetPaymentSummary(payment.Id);

        Assert.NotNull(result);
        Assert.Equal(payment.Id, result.Id);
        Assert.Equal(75.50m, result.Amount);
        Assert.Equal("Visa", result.CardIssuer);
        Assert.Equal("Initiated", result.Status);
    }
    
    [Fact]
    public void InitiatePayment_InvalidCard_ThrowsDomainException_DoesNotSave()
    {
        var studentId = Guid.NewGuid();
        var amount = 100.00m;
        var invalidCard = "12345"; 

        Assert.Throws<InvalidCardNumberException>(() => 
            _paymentService.InitiatePayment(studentId, amount, invalidCard));
        
        _repositoryMock.Verify(repo => repo.Add(It.IsAny<PaymentPurchase>()), Times.Never);
    }
}