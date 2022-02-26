using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ListRequestEventHandler(MessageEventArgs e, string List);

    /// <summary>
    /// Событие запроса каталога файлов на сервере 
    /// с передачей каталога в string подписчику
    /// </summary>
    public interface INotifyListRequest
    {
        /// <summary>
        /// Запрос списка файлов с пробросом списка в string
        /// </summary>
        [Obsolete]
        event ListRequestEventHandler ListRequest;
    }
}
