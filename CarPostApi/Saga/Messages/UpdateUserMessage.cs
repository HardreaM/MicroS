using Saga.Events;
using Saga.Messages.Interfaces;

namespace Saga.Messages;

public class UpdateUserMessage : IUpdateUserMessage
{
    public UpdateUserMessage()
    {
        OrderItemList = new List<OrderItem>();
    }

    public int OrderId { get; set; }
    public string CustomerId { get; set; }
    public string PaymentAccountId { get; set; }
    public decimal TotalPrice { get; set; }

    public List<OrderItem> OrderItemList { get; set; }
}