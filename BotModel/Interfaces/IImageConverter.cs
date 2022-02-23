using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IImageConverter
    {
        void StartConvert(string Extension, MessageEventArgs e);
    }
}
