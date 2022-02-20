using BotModel.Interfaces;
using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BotModel
{
    [Obsolete]
    public class ImageMessageListener : IMessageListener
    {
        static bool _inputImageExists = false;

        public delegate void ImageMessageReievedHandler(MessageEventArgs e);
        public event ImageMessageReievedHandler OnImageMessageReieved;

        public static bool InputImageExists
        {
            get => _inputImageExists;
            set => _inputImageExists = value;
        }
        public void Listen(object sender, MessageEventArgs e)
        {
            if (e.Message.Type == MessageType.Photo)
            {
                InputImageExists = true;
                OnImageMessageReieved(e);
            }
        }
    }
}
