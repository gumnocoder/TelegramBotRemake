using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IArchivationTool
    {
        delegate void  ArchivationCompleteHandler(string FileName, MessageEventArgs e);

        event ArchivationCompleteHandler OnArchivationComplete;
        void StartCompressing(MessageEventArgs e);
    }
}
