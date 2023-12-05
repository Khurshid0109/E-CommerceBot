using TelegramBot.Domain.Commons;

namespace TelegramBot.Domain.Entities;
public class Order:Auditable
{
    public long BotUserId { get; set; }
    public User User { get; set; }
    public decimal Total { get; set; }
    public ICollection<OrderItem> Items { get; set; }
}
