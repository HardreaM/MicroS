using MassTransit;

namespace Saga.Events.Interfaces;

public interface IStockReservationFailedEvent : CorrelatedBy<Guid>
{
    string ErrorMessage { get; set; }
}