using Saga.Events.Interfaces;

namespace Saga.Events;

public class StockReservationFailedEvent : IStockReservationFailedEvent
{
    public Guid CorrelationId { get; set; }
    public string ErrorMessage { get; set; }
}