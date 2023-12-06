
using TelegramBot.Service.DTOs.Products;

namespace TelegramBot.Service.DTOs.OrderItems;
public class OrderItemForCreatDto
{
    public long OrderId { get; set; }
    public OrderItemForResultDto Order { get; set; }
    public long ProductId { get; set; }
    public ProductForResultDto Product { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
