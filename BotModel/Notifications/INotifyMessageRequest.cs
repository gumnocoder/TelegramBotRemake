using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void MessageEventRequestHandler(MessageEventArgs e, byte Code);

    /// <summary>
    /// Событие запроса ответа от бота
    /// </summary>
    public interface INotifyMessageRequest
    {
        [Obsolete]
        event MessageEventRequestHandler MessageRequest;
    }
}
