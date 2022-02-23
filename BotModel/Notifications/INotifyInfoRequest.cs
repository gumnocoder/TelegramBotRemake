using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void InfoRequestEventHandler(MessageEventArgs e);

    [Obsolete]
    public interface INotifyInfoRequest
    {
        event InfoRequestEventHandler InfoRequest;
    }
}
