using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramBot.Services;

public class BotBackgroundService : BackgroundService
{
    private readonly ILogger<BotBackgroundService> _logger;
    private readonly TelegramBotClient _client;
    private readonly IUpdateHandler _handler;

    public BotBackgroundService(
        ILogger<BotBackgroundService> logger,
        TelegramBotClient client,
        IUpdateHandler handler)
    {
        _logger = logger;
        _client = client;
        _handler = handler;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var bot = await _client.GetMeAsync(stoppingToken);

        _logger.LogInformation("Bot started succesfully:{bot.Username}", bot.Username);

        _client.StartReceiving(
            _handler.HandleUpdateAsync,
            _handler.HandlePollingErrorAsync,
            new ReceiverOptions()
        {
            ThrowPendingUpdates = true
        },stoppingToken);
    }
}
