using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ImageMessageReсievedHandler(MessageEventArgs e);

    [Obsolete]
    public interface INotifyImageMessageReieved
    {
        public event ImageMessageReсievedHandler ImageMessageReieved;
    }
}
