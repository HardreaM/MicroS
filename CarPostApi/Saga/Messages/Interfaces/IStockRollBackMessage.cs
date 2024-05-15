using Saga.Events;

namespace Saga.Messages.Interfaces;

public interface IStockRollBackMessage
{
    public List<OrderItem> OrderItemList { get; set; }
}