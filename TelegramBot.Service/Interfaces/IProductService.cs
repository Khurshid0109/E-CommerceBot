using TelegramBot.Service.DTOs.Products;

namespace TelegramBot.Service.Interfaces;
public interface IProductService
{
    Task<bool> RemoveAsync(long id);
    Task<ProductForResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync();
    Task<ProductForResultDto> AddAsync(ProductForCreateDto dto);
    Task<ProductForResultDto> ModifyAsync( ProductForUpdateDto dto);
}
