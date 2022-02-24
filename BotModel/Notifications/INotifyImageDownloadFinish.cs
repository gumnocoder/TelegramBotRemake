using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ImageDownloadFinishEventHandler(MessageEventArgs e);

    /// <summary>
    /// Событие окончания скачивания изображения
    /// отправленного пользователем
    /// </summary>
    public interface INotifyImageDownloadFinish
    {
        [Obsolete]
        event ImageDownloadFinishEventHandler ImageDownloadFinish;
    }
}
