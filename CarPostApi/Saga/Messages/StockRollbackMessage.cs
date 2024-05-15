using Saga.Events;
using Saga.Messages.Interfaces;

namespace Saga.Messages;

public class StockRollbackMessage : IStockRollBackMessage
{
    public List<OrderItem> OrderItemList { get; set; }
}