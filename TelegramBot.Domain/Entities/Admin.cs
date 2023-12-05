using TelegramBot.Domain.Commons;

namespace TelegramBot.Domain.Entities;
public class Admin:Auditable
{
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public long TelegramId { get; set; }
}
