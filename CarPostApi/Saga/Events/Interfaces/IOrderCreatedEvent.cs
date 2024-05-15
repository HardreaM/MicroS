using MassTransit;

namespace Saga.Events.Interfaces;

public interface IOrderCreatedEvent : CorrelatedBy<Guid>
{
    List<OrderItem> OrderItemList { get; set; }
}