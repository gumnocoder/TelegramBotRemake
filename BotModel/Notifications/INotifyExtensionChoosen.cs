using System;
using Telegram.Bot.Args;

namespace BotModel.Notifications
{
    [Obsolete]
    public delegate void IExtensionChoosenHandler(string Extension, MessageEventArgs e);

    /// <summary>
    /// Событие назначения конечного расширения 
    /// после конвертации изображения, с передачей расширения подписчику
    /// </summary>
    [Obsolete]
    public interface INotifyExtensionChoosen
    {
        public event IExtensionChoosenHandler ExtensionChoosen;
    }
}
