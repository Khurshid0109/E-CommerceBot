using TelegramBot.Domain.Commons;
using TelegramBot.Domain.Enums;

namespace TelegramBot.Domain.Entities;
public class User:Auditable
{
    public string FullName { get; set; }
    public long TelegramId { get; set; }
    public string PhoneNumber { get; set; }
    public UserRole Role { get; set; }

    public ICollection<Order> Orders { get; set; }
}
