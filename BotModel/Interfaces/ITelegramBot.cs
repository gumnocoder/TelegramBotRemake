using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Контракт для классов предоставляющих экземляры клиента бота
    /// </summary>
    [Obsolete]
    public interface ITelegramBot
    {
        /// <summary>
        /// Клиент бота
        /// </summary>
        ITelegramBotClient Client { get; }
        /// <summary>
        /// Логика реакции на текстовые сообщения
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Code">код реакции</param>
        void OnMessageReactions(MessageEventArgs e, byte Code);

        /// <summary>
        /// Логика реакции на сообщения содержащие какой-либо контент
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Content">контент</param>
        void OnContentMessageReactions(MessageEventArgs e, string Content);

    }
}
