using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ImageDownloadFinishEventHandler(MessageEventArgs e);
    public interface INotifyImageDownloadFinish
    {
        [Obsolete]
        event ImageDownloadFinishEventHandler ImageDownloadFinish;
    }
}
