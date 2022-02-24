using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IKeyboardable
    {
        ReplyKeyboardMarkup Keyboard { get; set; }
        void SendKeyboard(MessageEventArgs e);
    }
}
