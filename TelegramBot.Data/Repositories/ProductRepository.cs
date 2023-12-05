using TelegramBot.Data.DbContexts;
using TelegramBot.Domain.Entities;
using TelegramBot.Data.IRepositories;

namespace TelegramBot.Data.Repositories;
public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(DataContext context) : base(context)
    {
    }
}
