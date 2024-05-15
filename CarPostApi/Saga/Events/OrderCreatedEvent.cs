using Saga.Events.Interfaces;

namespace Saga.Events;

public class OrderCreatedEvent : IOrderCreatedEvent
{
    public Guid CorrelationId { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
}