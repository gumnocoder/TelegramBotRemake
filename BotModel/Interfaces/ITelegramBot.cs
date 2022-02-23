using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface ITelegramBot
    {
        ITelegramBotClient Client { get; }
        void OnMessageReactions(MessageEventArgs e, byte Code);

        void OnContentMessageReactions(MessageEventArgs e, string Content);

    }
}
