using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// для реализации в классах выполняющих 
    /// ответ на полученные сообщения
    /// </summary>
    [Obsolete]
    public interface IMessageSender
    {
        /// <summary>
        /// Метод отправки ответа 
        /// </summary>
        /// <param name="e"></param>
        void Send(MessageEventArgs e);

        /// <summary>
        /// перегрузка метода отправки ответа 
        /// с дополнительным аттрибутом string
        /// </summary>
        /// <param name="attribute"></param>
        /// <param name="e"></param>
        void Send(string attribute, MessageEventArgs e);
    }
}
