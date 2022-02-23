using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void MessageEventRequestHandler(MessageEventArgs e, byte Code);
    public interface INotifyMessageRequest
    {
        [Obsolete]
        event MessageEventRequestHandler MessageRequest;
    }
}
