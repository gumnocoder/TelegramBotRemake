using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void FileRequestEventHanlder(string FileName, MessageEventArgs e);
    
    /// <summary>
    /// Событие оповещения о запросе файла с сервера 
    /// </summary>
    public interface INotifyFileRequest
    {
        /// <summary>
        /// Запрос файла
        /// </summary>
        [Obsolete]
        event FileRequestEventHanlder FileRequest;
    }
}
