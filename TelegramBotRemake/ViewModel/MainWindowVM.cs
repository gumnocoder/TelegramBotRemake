using BotModel;
using BotModel.Interfaces;
using System;
using static BotModel.TelegramBot;

namespace TelegramBotRemake.ViewModel
{
    [Obsolete]
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    internal class MainWindowVM
    {
        public MainWindowVM()
        {
            // ImageSaver.ImageConverted +=
            IImageSaver _userImageSaver = new ImageDownloader();

            IMessageListener _textMessageListener = new TextMessageListener(
                new FileOnRequestSender(ref TextMessageListener._flagToGetFile), 
                new FilesOnServerInfoSender());

            ImageMessageListener _imageMessageListener = new();

            Client.StartReceiving();
            Client.OnMessage += _imageMessageListener.Listen;
            Client.OnMessage += _textMessageListener.Listen;

            _imageMessageListener.OnImageMessageReieved += _userImageSaver.StartSave;
        }
    }
}
