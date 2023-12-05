using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;
public partial class BotUpdateHandler
{
    public async Task HandleMessageAsync(ITelegramBotClient botclient, Message? message, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(message, nameof(message));

        var from = message.From;

        _logger.LogInformation($"Received message from {from.FirstName}");

        var handler = message.Type switch
        {
            MessageType.Text => HandleTextMessageAsync(botclient, message, cancellationToken),
            _ => HandleUnknownMessageAsync(botclient, message, cancellationToken)
        };

        await handler;
    }

    private Task HandleUnknownMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        _logger.LogInformation($"Received message type {message.Type}");

        return Task.CompletedTask;
    }

    private async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        //var user = //reposUser
        var from = message.From;

        _logger.LogInformation($"From:{from.FirstName} {message.Text}");

        if (message.Text == "/start")
        {
            if(message.From.Id)
            await client.SendTextMessageAsync(chatId: message.Chat.Id,
                text: "Thank you for joining our bot.Please,choose a language!",
                replyToMessageId: message.MessageId,
                cancellationToken: token);


        }
        else
        {
            ReplyKeyboardMarkup replyKeyboardMarkup = new([
                KeyboardButton.WithRequestContact("Tel yuborish")
            ]);
            await client.SendTextMessageAsync(chatId:from.Id,
                text:"Avval tel raqamini yubor suka!",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: token);
        }
    }
}
