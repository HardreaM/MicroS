using Saga.Events;

namespace Saga.Messages.Interfaces;

public interface IUpdateUserMessage
{
    public int OrderId { get; set; }
    public string CustomerId { get; set; }
    public string PaymentAccountId { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderItem> OrderItemList { get; set; }
}