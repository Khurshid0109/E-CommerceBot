
namespace TelegramBot.Service.Exceptions;
public class BotException:Exception
{
    public int StatusCode {  get; set; }
    public BotException(int code,string message):base(message)
    {
        StatusCode = code;
    }
}
