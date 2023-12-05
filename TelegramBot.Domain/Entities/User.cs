using TelegramBot.Domain.Commons;

namespace TelegramBot.Domain.Entities;
public class User:Auditable
{
    public string FullName { get; set; }
    public long TelegramId { get; set; }
    public string PhoneNumber { get; set; }
    public int VerificationStep {  get; set; }
    public ICollection<Order> Orders { get; set; }
}
