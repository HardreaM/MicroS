using Saga.Events.Interfaces;

namespace Saga.Events;

public class OrderCompletedEvent : IOrderCompletedEvent
{
    public string CustomerId { get; set; }
    public int OrderId { get; set; }
}