using TelegramBot.Domain.Commons;

namespace TelegramBot.Domain.Entities;
public class OrderItem:Auditable
{
    public long OrderId { get; set; }
    public Order Order { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
