
using TelegramBot.Service.DTOs.Orders;

namespace TelegramBot.Service.DTOs.User;
public class UserForResultDto
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public long TelegramId { get; set; }
    public string PhoneNumber { get; set; }
    public int VerificationStep { get; set; }
    public ICollection<OrderForResultDto> Orders { get; set; }
}
