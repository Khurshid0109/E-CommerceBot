
using TelegramBot.Service.DTOs.Orders;
using TelegramBot.Service.DTOs.Products;

namespace TelegramBot.Service.DTOs.OrderItems;
public class OrderItemForResultDto
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public OrderForResultDto Order { get; set; }
    public long ProductId { get; set; }
    public ProductForResultDto Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
