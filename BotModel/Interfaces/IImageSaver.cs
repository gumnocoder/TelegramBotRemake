using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IImageSaver
    {
        void StartSave(MessageEventArgs e);
    }
}
