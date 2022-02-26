using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Diagnostics;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BotModel
{
    /// <summary>
    /// Класс выполняющий сканирование сообщений с изображением
    /// </summary>
    [Obsolete]
    public class ImageMessageListener : 
        IMessageListener, 
        INotifyImageMessageReieved, 
        IImageMessageListener
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="messageListener"></param>
        public ImageMessageListener(ITextMessageListener messageListener)
        {
            _messageListener = messageListener;
        }

        bool _inputImageExists = true;
        ITextMessageListener _messageListener;

        public bool InputImageExists 
        { get => _inputImageExists; set => _inputImageExists = value; }

        public event ImageMessageReсievedHandler ImageMessageReieved;
        public void Listen(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Photo)
            {
                _messageListener.FirstMessageFlag = false;
                _inputImageExists = true;
                ImageMessageReieved(e);
            }
        }
    }
}
