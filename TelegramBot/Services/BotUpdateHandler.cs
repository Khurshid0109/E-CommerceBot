using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;

namespace TelegramBot.Services;
public partial class BotUpdateHandler : IUpdateHandler
{
    private readonly ILogger<BotUpdateHandler> _logger;

    public BotUpdateHandler(ILogger<BotUpdateHandler> logger)
    {
        _logger = logger;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Error occured with Telegram bot {exception}");

        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var handler = update switch
        {
            { Message : {Contact:{ } contact } message } => SendContect(botClient, message, contact, cancellationToken),
            { Message: { } message } => HandleMessageAsync(botClient, message, cancellationToken),
            { EditedMessage: { } message } => HandleEditedMessage(botClient, message, cancellationToken),
            _ => HandleUnknownMessage(botClient,update,cancellationToken)
        };

        try
        {
            await handler;
        }
        catch(Exception ex)
        {
            await HandlePollingErrorAsync(botClient,ex,cancellationToken);
        }
    }

    private async Task SendContect(ITelegramBotClient botClient, Message message, Contact contact, CancellationToken cancellationToken)
    {
        if (contact.UserId != message.From.Id)
            await botClient.SendTextMessageAsync(message.From.Id, "Pashol naxxuy!");
    }

    private  Task HandleUnknownMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Update type {update.Type} received!");

        return Task.CompletedTask;
    }

}
