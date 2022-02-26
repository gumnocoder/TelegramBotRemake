using System;
using System.Drawing;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Набор для реализации в классах использующий 
    /// ReplyKeyboardMarkup клавиатуры
    /// </summary>
    [Obsolete]
    public interface IKeyboardable
    {
        /// <summary>
        /// Клавиатура с опциями ответа боту
        /// </summary>
        ReplyKeyboardMarkup Keyboard { get; set; }

        /// <summary>
        /// Метод отправляющий клавиатуру с опциями
        /// </summary>
        /// <param name="e"></param>
        void SendKeyboard(MessageEventArgs e, string Filename, Telegram.Bot.Types.Message mess, ITelegramBotClient Client);
    }
}
