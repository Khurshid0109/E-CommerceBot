using TelegramBot.Data.DbContexts;
using TelegramBot.Domain.Entities;
using TelegramBot.Data.IRepositories;

namespace TelegramBot.Data.Repositories;
public class AdminRepository : Repository<Admin>, IAdminRepository
{
    public AdminRepository(DataContext context) : base(context)
    {
    }
}
