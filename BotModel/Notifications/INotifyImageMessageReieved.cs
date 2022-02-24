using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ImageMessageReсievedHandler(MessageEventArgs e);

    /// <summary>
    /// Событие получения нового сообщения содержащего изображение
    /// </summary>
    [Obsolete]
    public interface INotifyImageMessageReieved
    {
        public event ImageMessageReсievedHandler ImageMessageReieved;
    }
}
