using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void FilenameExtensionChoosenHandler(MessageEventArgs e);

    /// <summary>
    /// Событие назначения конечного расширения
    /// без передачи расширения подписчику
    /// </summary>
    [Obsolete]
    public interface INotifyFilenameExtensionChoosen
    {
        event FilenameExtensionChoosenHandler FilenameExtensionChoosen;
    }
}
