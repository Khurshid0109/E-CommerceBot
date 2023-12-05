using TelegramBot.Data.DbContexts;
using TelegramBot.Domain.Entities;
using TelegramBot.Data.IRepositories;

namespace TelegramBot.Data.Repositories;
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    {
    }
}
