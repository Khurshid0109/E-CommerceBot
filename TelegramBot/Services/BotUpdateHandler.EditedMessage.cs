using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services
{
    public partial class BotUpdateHandler
    {
        private async Task HandleEditedMessage(ITelegramBotClient botClient,Message? message,CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(message));

            var from = message.From;

            _logger.LogInformation($"Receive edited message from {from.FirstName} {message.Text}");
        }
    }
}
