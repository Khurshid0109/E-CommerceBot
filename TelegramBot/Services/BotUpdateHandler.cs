using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Polling;

namespace TelegramBot.Services;
public partial class BotUpdateHandler : IUpdateHandler
{
    private readonly ILogger<BotUpdateHandler> _logger;
    private readonly IServiceProvider _serviceProvider;
  
    public BotUpdateHandler(ILogger<BotUpdateHandler> logger ,
       IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
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
            { Message : {Contact:{ } contact } message } => SendContact(botClient, message, contact, cancellationToken),
            { Message: { Location: { } location } message }=>SendLocation(botClient,message,location,cancellationToken),
            { Message: { } message } => HandleTextMessageAsync(botClient, message, cancellationToken),
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


    private  Task HandleUnknownMessage(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Update type {update.Type} received!");

        return Task.CompletedTask;
    }

}
