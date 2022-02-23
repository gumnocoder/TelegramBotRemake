using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void ListRequestEventHandler(MessageEventArgs e, string List);
    public interface INotifyListRequest
    {
        [Obsolete]
        event ListRequestEventHandler ListRequest;
    }
}
