using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    public delegate void ImageConvertFinishHandler(string FileName, MessageEventArgs e);
    public interface INotifyImageConversion
    {
        event ImageConvertFinishHandler ImageConverted;
    }
}
