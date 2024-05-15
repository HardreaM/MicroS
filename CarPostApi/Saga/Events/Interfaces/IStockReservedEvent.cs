using MassTransit;

namespace Saga.Events.Interfaces;

public interface IStockReservedEvent : CorrelatedBy<Guid>
{
    List<OrderItem> OrderItemList { get; set; }
}