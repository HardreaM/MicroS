using Saga.Events.Interfaces;

namespace Saga.Events;

public class PaymentCompletedEvent : IPaymentCompletedEvent
{
    public Guid CorrelationId { get; set; }
}