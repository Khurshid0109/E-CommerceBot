using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TelegramBot.Extentions;
using TelegramBot.Data.DbContexts;
using TelegramBot.Service.Helpers;
using TelegramBot.Service.Mappers;
using TelegramBot.Services;

var builder = WebApplication.CreateBuilder(args);

var token = builder.Configuration.GetValue("BotToken", string.Empty);

builder.Services.AddSingleton(p => new TelegramBotClient(token));

builder.Services.AddSingleton<IUpdateHandler,BotUpdateHandler>();

builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.CustomExtention();

//registration of Database
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

WebHostEnvironment.WebRootPath = Path.GetFullPath("wwwroot");


//AutoMapper registration
builder.Services.AddAutoMapper(typeof(MapperProfile));

var app = builder.Build();

//var supportedCultures = new[] { "uz-Uz", "en-Us", "ru-Ru" };
//var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
//    .AddSupportedCultures(supportedCultures)
//    .AddSupportedUICultures(supportedCultures);

//app.UseRequestLocalization(localizationOptions);

app.Run();