using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IMessageListener
    {
        void Listen(object sender, MessageEventArgs e);
    }
}
