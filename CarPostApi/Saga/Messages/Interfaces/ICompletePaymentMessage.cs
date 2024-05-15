using MassTransit;
using Saga.Events;

namespace Saga.Messages.Interfaces;

public interface ICompletePaymentMessage : CorrelatedBy<Guid>
{
    public string CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItemList { get; set; }

}