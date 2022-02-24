using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ImageConvertFinishHandler(string FileName, MessageEventArgs e);

    /// <summary>
    /// Событие окончания конвертации изображения
    /// </summary>
    [Obsolete]
    public interface INotifyImageConversion
    {
        event ImageConvertFinishHandler ImageConverted;
    }
}
