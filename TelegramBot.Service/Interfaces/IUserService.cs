
using TelegramBot.Service.DTOs.User;

namespace TelegramBot.Service.Interfaces;
public interface IUserService
{
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync();
    Task<UserForResultDto> AddAsync(UserForEntryDto dto);
    Task<UserForResultDto> ModifyAsync(UserForEntryDto dto);
    Task<UserForResultDto> RetrieveByPhoneNumber(string phoneNumber);
    Task<bool> SetStage(long id,int stage);
}
