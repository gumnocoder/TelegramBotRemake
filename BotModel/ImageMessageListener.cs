using BotModel.Interfaces;
using System;
using System.Diagnostics;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BotModel
{
    public delegate void ImageMessageReсievedHandler(MessageEventArgs e);

    [Obsolete]
    public class ImageMessageListener : IMessageListener, INotifyImageMessageReieved, IImageMessageListener
    {
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
            Debug.WriteLine($"");
            Debug.WriteLine($"inputImageExists {_inputImageExists}");
            if (e.Message.Type == MessageType.Photo)
            {
                _messageListener.FirstMessageFlag = false;
                _inputImageExists = true;
                Debug.WriteLine($"inputImageExists {_inputImageExists}");
                ImageMessageReieved(e);
            }
        }
    }

    public interface INotifyImageMessageReieved
    {
        public event ImageMessageReсievedHandler ImageMessageReieved;
    }
}
