using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void FileRequestEventHanlder(string FileName, MessageEventArgs e);
    public interface INotifyFileRequest
    {
        [Obsolete]
        event FileRequestEventHanlder FileRequest;
    }
}
