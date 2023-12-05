using TelegramBot.Data.DbContexts;
using TelegramBot.Domain.Entities;
using TelegramBot.Data.IRepositories;

namespace TelegramBot.Data.Repositories;
public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(DataContext context) : base(context)
    {
    }
}
