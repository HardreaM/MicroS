using Saga.Events.Interfaces;

namespace Saga.Events;

public class StockReservedEvent : IStockReservedEvent
{
    public Guid CorrelationId { get; set; }
    public List<OrderItem> OrderItemList { get; set; }

}