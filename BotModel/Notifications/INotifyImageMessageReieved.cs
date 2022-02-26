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
        /// <summary>
        /// Получение сообщения с изображением
        /// </summary>
        public event ImageMessageReсievedHandler ImageMessageReieved;
    }
}
