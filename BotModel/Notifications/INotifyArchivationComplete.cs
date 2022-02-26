using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ArchivationCompleteEventHandler(string FileName, MessageEventArgs e);

    /// <summary>
    /// Событие окончания архивации данных
    /// </summary>
    public interface INotifyArchivationComplete
    {
        /// <summary>
        /// Событие окончания архивации данных
        /// </summary>
        [Obsolete]
        event ArchivationCompleteEventHandler ArchivationComplete;
    }
}
