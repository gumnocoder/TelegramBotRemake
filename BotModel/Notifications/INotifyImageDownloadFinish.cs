using System;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ImageDownloadFinishEventHandler(MessageEventArgs e, string Filename, Telegram.Bot.Types.Message mess, ITelegramBotClient Client);

    /// <summary>
    /// Событие окончания скачивания изображения
    /// отправленного пользователем
    /// </summary>
    public interface INotifyImageDownloadFinish
    {
        /// <summary>
        /// Окончание скачивания изображения
        /// </summary>
        [Obsolete]
        event ImageDownloadFinishEventHandler ImageDownloadFinish;
    }
}
