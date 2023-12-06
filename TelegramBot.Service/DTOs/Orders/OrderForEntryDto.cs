
using TelegramBot.Service.DTOs.OrderItems;
using TelegramBot.Service.DTOs.User;

namespace TelegramBot.Service.DTOs.Orders;
public class OrderForEntryDto
{
    public long BotUserId { get; set; }
    public UserForResultDto User { get; set; }
    public decimal Total { get; set; }
    public ICollection<OrderItemForResultDto> Items { get; set; }
}
