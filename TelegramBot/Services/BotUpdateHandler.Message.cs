using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Service.DTOs.User;
using TelegramBot.Service.Interfaces;

namespace TelegramBot.Services;
public partial class BotUpdateHandler
{
    private async Task HandleTextMessageAsync(ITelegramBotClient client, Message message, CancellationToken token)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        var from = message.From;

        _logger.LogInformation($"From:{from.FirstName} => {message.Text}");
        var user = await userService.RetrieveByIdAsync(from.Id);

        if (message.Text == "/start")
        {

            if (user is not null)
            {
                await client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: "Botimizdan foydalanayotganingizdan xursandmiz." +
                    "Keling yaxshisi xaridni boshlaymiz!",
                    replyToMessageId: message.MessageId,
                    cancellationToken: token);
            }
            else
            {
                await client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: "Botimizdan foydalanish uchun raqamingizni tasdiqang.",
                    replyToMessageId: message.MessageId,
                    cancellationToken: token);

                await RequestPhoneNumberAsync(client, message, token);
            }
        }
        else if (message.Text == "xurshid")
        {
            await userService.SetStage(from.Id, 5);
        }
        else if (user is not null && user.VerificationStep >= 1)
        {
            switch (message.Text)
            {
                case "👜Buyurtma berish.":

                    await SendServiceTypeReplyKeyboard(client, message, token);
                    break;

                case "🚖Yetkazib berish.":
                    await RequestLocationAsync(client, message, token);
                    break;

                case "☎️Biz bilan aloqa.":

                    await client.SendTextMessageAsync(chatId: message.Chat.Id,
                   text: "Sizda qandaydir tushunmovchilik,savol yoki takliflar bo'lsa iltimos ushbu raqam bilan bizga bo'g'laning!" +
                   "☎️ +998930758239",
                   replyToMessageId: message.MessageId,
                   cancellationToken: token);

                    break;

                case "ℹ️Ma'lumot.":

                    await client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: "Biz erkin tadbirkor sifatida quyida berib o'tilgan xizmatlarni sizga taklif etamiz.",
                    replyToMessageId: message.MessageId,
                    cancellationToken: token);

                    break;

                case "📕Fikr bildirish.":
                    await RateReplyKeyboard(client, message, token);
                    break;

                case "⚙️Sozlamalar.":
                    break;

                case "⬅️Ortga.":
                    await SendReplyKeyboard(client, message, token);

                    break;

                case "⭐️":
                case "⭐️⭐️":
                case "⭐️⭐️⭐️":
                    await client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: "Xizmatimizdan qoniqmaganingizdan afsusdamiz." +
                    "Xizmatlarni yaxshilashga harakat qilamiz",
                    replyToMessageId: message.MessageId,
                    cancellationToken: token);

                    await SendReplyKeyboard(client, message, token);
                    break;

                case "⭐️⭐️⭐️⭐️":
                case "⭐️⭐️️⭐️⭐️":
                    await client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: "Xizmatlarimiz sizga yoqqanidan xursandmiz!",
                    replyToMessageId: message.MessageId,
                    cancellationToken: token);

                    await SendReplyKeyboard(client, message, token);
                    break;

                case "🙎🏻Olib ketish.":
                    await SendBranchLocationKeyboard(client, message, token);
                    break;

                case "Yunusobod":
                    await client.SendLocationAsync(
                        chatId: message.Chat.Id,
                        latitude: 40.7128,
                        longitude: -74.0060,
                        cancellationToken: token);

                    break;

                case "Novza":

                    await client.SendLocationAsync(
                          chatId: message.Chat.Id,
                          latitude: 40.7128,
                          longitude: -74.0060,
                          cancellationToken: token);

                    break;

                default:
                    await client.SendTextMessageAsync(chatId: message.Chat.Id,
                      text: "Keling birgalikda xaridlarni amalga oshiramiz!",
                      replyToMessageId: message.MessageId,
                      cancellationToken: token);

                    await SendReplyKeyboard(client, message, token);
                    break;
            }
        }
        else
        {
            await client.SendTextMessageAsync(chatId: message.Chat.Id,
                    text: "Botimizdan foydalanish uchun raqamingizni tasdiqang.",
                    replyToMessageId: message.MessageId,
                    cancellationToken: token);

            await RequestPhoneNumberAsync(client, message, token);
        }
    }
    //Request button for phone number
    private async Task RequestPhoneNumberAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup RequestReplyKeyboard = new([
           KeyboardButton.WithRequestContact("📱 Raqamni jo'natish"),
        ])
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: "Telefon raqamingizni ulashing!",
            replyMarkup: RequestReplyKeyboard,
            cancellationToken: cancellationToken);
    }
    //Request button for location
    private async Task RequestLocationAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        ReplyKeyboardMarkup RequestReplyKeyboard = new(
            new[]
            {
                new KeyboardButton[] { KeyboardButton.WithRequestLocation("Manzilni jo'natish.") },
                new KeyboardButton[] { "⬅️Ortga." }
            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
            text: "Siz turgan manzilni ulashing!",
            replyMarkup: RequestReplyKeyboard,
            cancellationToken: cancellationToken);
    }
    // Handle phone number and Create user
    private async Task SendContact(ITelegramBotClient botClient, Message message, Contact contact, CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

        if (contact.UserId != message.From.Id)
        {
            await botClient.SendTextMessageAsync(message.From.Id, "Iltimos pastdagi tugma orqali telefon raqamingizni ulashing!");
            return;
        }

        var phoneNumber = contact.PhoneNumber;

        // Check if user exists with phone number
        var existingUser = await userService.RetrieveByPhoneNumber(phoneNumber);

        if (existingUser != null)
        {
            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                   text: "Botimizdan foydalanayotganingizdan xursandmiz.",
                   replyToMessageId: message.MessageId,
                   cancellationToken: cancellationToken);
        }
        else
        {
            // User doesn't exist, create new user
            var newUser = new UserForEntryDto
            {
                TelegramId = message.From.Id,
                FullName = message.From.FirstName,
                PhoneNumber = phoneNumber,
                VerificationStep = 1
            };

            await userService.AddAsync(newUser);

            await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                  text: "Botimizdan foydalanayotganingizdan xursandmiz.",
                  replyToMessageId: message.MessageId,
                  cancellationToken: cancellationToken);

            await SendReplyKeyboard(botClient, message, cancellationToken);
        }
    }

    // Handle user's location
    private async Task SendLocation(ITelegramBotClient client, Message message, Location location, CancellationToken cancellation)
    {
        await client.SendTextMessageAsync(chatId: message.Chat.Id,
                 text: "Manzilingiz qabul qilindi.",
                 replyToMessageId: message.MessageId,
                 cancellationToken: cancellation);
    }

    //Request buttons for main menu
    private async Task SendReplyKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                        new KeyboardButton[] { "👜Buyurtma berish." },
                        new KeyboardButton[] { "📕Fikr bildirish.", "☎️Biz bilan aloqa." },
                        new KeyboardButton[] { "ℹ️Ma'lumot.", "⚙️Sozlamalar." },
            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: "Quyidagi xizmatlardan birini tanlang.",
           replyMarkup: replyKeyboardMarkup,
           cancellationToken: cancellationToken);
    }

    private async Task RateReplyKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                        new KeyboardButton[] { "⭐️", "⭐️⭐️" },
                        new KeyboardButton[] { "⭐️⭐️⭐️","⭐️⭐️⭐️⭐️" },
                        new KeyboardButton[] { "⭐️⭐️⭐⭐️⭐️" }

            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: "Xizmatlarimizni baholang.",
           replyMarkup: replyKeyboardMarkup,
           cancellationToken: cancellationToken);
    }

    //Request for service type 
    private async Task SendServiceTypeReplyKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                        new KeyboardButton[] { "🚖Yetkazib berish.", "🙎🏻Olib ketish." },
                        new KeyboardButton[] { "⬅️Ortga." },

            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: "Xizmat ko'rsatish turini tanlang.",
           replyMarkup: replyKeyboardMarkup,
           cancellationToken: cancellationToken);
    }

    //Request for our nearer branch
    private async Task SendBranchLocationKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                        new KeyboardButton[] { "Yunusobod", "Novza" },
                        new KeyboardButton[] { "⬅️Ortga." },
            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: "O'zingizga yaqin hududni tanlang.",
           replyMarkup: replyKeyboardMarkup,
           cancellationToken: cancellationToken);
    }

    private async Task SendSettingsKeyboard(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {

        ReplyKeyboardMarkup replyKeyboardMarkup = new(
            new[]
            {
                        new KeyboardButton[] { "📱Telefon raqamni o'zgartirish.", "🙎🏻Ismni o'zgartirish." },
                        new KeyboardButton[] { "⬅️Ortga." },
            })
        {
            ResizeKeyboard = true
        };

        await botClient.SendTextMessageAsync(
           chatId: message.Chat.Id,
           text: "O'zingizga yaqin hududni tanlang.",
           replyMarkup: replyKeyboardMarkup,
           cancellationToken: cancellationToken);
    }
}
