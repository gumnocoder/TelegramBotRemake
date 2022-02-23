using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void IExtensionChoosenHandler(string Extension, MessageEventArgs e);

    [Obsolete]
    public interface INotifyExtensionChoosen
    {
        public event IExtensionChoosenHandler ExtensionChoosen;
    }
}
