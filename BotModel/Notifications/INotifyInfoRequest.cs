using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void InfoRequestEventHandler(MessageEventArgs e);

    /// <summary>
    /// Событие запроса каталога файлов на сервере
    /// </summary>
    [Obsolete]
    public interface INotifyInfoRequest
    {
        /// <summary>
        /// Запрос списка файлов
        /// </summary>
        event InfoRequestEventHandler InfoRequest;
    }
}
