using BotModel.Interfaces;
using System;
using System.Diagnostics;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace BotModel
{
    public delegate void ImageMessageReсievedHandler(MessageEventArgs e);

    [Obsolete]
    public class ImageMessageListener : IMessageListener, INotifyImageMessageReieved
    {
        public ImageMessageListener(ref bool FirstMessageFlag)
        {
            _firstMessageFlag = FirstMessageFlag;
        }

        public static bool inputImageExists = true;
        bool _firstMessageFlag;

        
        public event ImageMessageReсievedHandler ImageMessageReieved;
        public void Listen(object sender, MessageEventArgs e)
        {
            Debug.WriteLine($"");
            Debug.WriteLine($"inputImageExists {inputImageExists}");
            if (e.Message.Type == MessageType.Photo)
            {
                TextMessageListener.firstMessageFlag = false;
                inputImageExists = true;
                Debug.WriteLine($"inputImageExists {inputImageExists}");
                ImageMessageReieved(e);
            }
        }
    }

    public interface INotifyImageMessageReieved
    {
        public event ImageMessageReсievedHandler ImageMessageReieved;
    }
}
