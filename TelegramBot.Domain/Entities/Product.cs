using TelegramBot.Domain.Commons;

namespace TelegramBot.Domain.Entities;
public class Product:Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Provider { get; set; }
}
