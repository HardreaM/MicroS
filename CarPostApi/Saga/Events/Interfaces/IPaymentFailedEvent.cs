using MassTransit;

namespace Saga.Events.Interfaces;

public interface IPaymentFailedEvent : CorrelatedBy<Guid>
{
    public string ErrorMessage { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
}