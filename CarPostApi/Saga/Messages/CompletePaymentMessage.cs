using Saga.Events;
using Saga.Messages.Interfaces;

namespace Saga.Messages;

public class CompletePaymentMessage : ICompletePaymentMessage
{
    public Guid CorrelationId { get; set; }
    public string CustomerId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItemList { get; set; }

}