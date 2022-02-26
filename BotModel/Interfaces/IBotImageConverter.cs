using System;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Контракт конвертера изображений
    /// </summary>
    [Obsolete]
    public interface IBotImageConverter
    {
        /// <summary>
        /// Метод конвертирования изображения
        /// </summary>
        /// <param name="Extension">целевое расширение</param>
        /// <param name="e">аргументы сообщения</param>
        void StartConvert(string Extension, MessageEventArgs e);

        /// <summary>
        /// Принимает параметры для конвертирования
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Filename"></param>
        /// <param name="mess"></param>
        /// <param name="Client"></param>
        void GetParams(
            MessageEventArgs e,
            string Filename,
            Telegram.Bot.Types.Message mess,
            ITelegramBotClient Client);
    }
}
