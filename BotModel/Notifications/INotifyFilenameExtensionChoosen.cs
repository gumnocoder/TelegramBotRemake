using System;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void FilenameExtensionChoosenHandler(MessageEventArgs e, string Filename, Telegram.Bot.Types.Message mess, ITelegramBotClient Client);

    /// <summary>
    /// Событие назначения конечного расширения
    /// без передачи расширения подписчику
    /// </summary>
    [Obsolete]
    public interface INotifyFilenameExtensionChoosen
    {
        /// <summary>
        /// Событие назначения конечного расширения
        /// без передачи расширения подписчику
        /// </summary>
        event FilenameExtensionChoosenHandler FilenameExtensionChoosen;
    }
}
