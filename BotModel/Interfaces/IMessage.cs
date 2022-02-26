using System;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Набор свойств для реализации в классах 
    /// стенографирующих полученные сообщения
    /// </summary>
    public interface IMessage
    {

        /// <summary>
        /// Дата получения сообщения
        /// </summary>
        DateTime Time { get; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        string MessageText { get; }

        /// <summary>
        /// Отправитель сообщения
        /// </summary>
        string Sender { get; set; }
    }
}
