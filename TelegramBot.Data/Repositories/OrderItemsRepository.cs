using TelegramBot.Data.DbContexts;
using TelegramBot.Domain.Entities;
using TelegramBot.Data.IRepositories;

namespace TelegramBot.Data.Repositories;
public class OrderItemsRepository : Repository<OrderItem>, IOrderItemsRepository
{
    public OrderItemsRepository(DataContext context) : base(context)
    {
    }
}
