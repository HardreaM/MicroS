using MassTransit;

namespace Saga.Events.Interfaces;

public interface IPaymentCompletedEvent : CorrelatedBy<Guid>
{
}