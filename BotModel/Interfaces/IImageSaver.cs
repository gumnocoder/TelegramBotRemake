using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Набор методов загрузчика изображений из полученных сообщений
    /// </summary>
    [Obsolete]
    public interface IImageSaver
    {
        /// <summary>
        /// Метод загрузки изображения из сообщения
        /// </summary>
        /// <param name="e"></param>
        void StartSave(MessageEventArgs e);
    }
}
