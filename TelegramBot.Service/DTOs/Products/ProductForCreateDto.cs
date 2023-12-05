using Microsoft.AspNetCore.Http;

namespace TelegramBot.Service.DTOs.Products;
public class ProductForCreateDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
    public string Provider { get; set; }
}
