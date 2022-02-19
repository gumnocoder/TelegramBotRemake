using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IMessageSender
    {
        void Send(MessageEventArgs e);
    }
}
