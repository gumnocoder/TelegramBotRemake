using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// для реализации в классах сканирующих
    /// текст полученного сообщения
    /// </summary>
    [Obsolete]
    public interface IMessageListener
    {
        /// <summary>
        /// Метод сканирования полученного сообщения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Listen(object sender, MessageEventArgs e);
    }
}
