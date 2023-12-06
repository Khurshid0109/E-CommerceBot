using TelegramBot.Data.IRepositories;
using TelegramBot.Data.Repositories;
using TelegramBot.Service.Interfaces;
using TelegramBot.Service.Services;

namespace TelegramBot.Extentions;
public static class ServiceExtention
{
    public static void CustomExtention(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
    }
}
