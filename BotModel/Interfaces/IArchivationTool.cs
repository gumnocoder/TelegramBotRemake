using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete]
    public interface IArchivationTool
    {
        void StartCompressing(string fileName, MessageEventArgs e);
    }
}
