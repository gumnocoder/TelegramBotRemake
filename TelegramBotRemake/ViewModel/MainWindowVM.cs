using BotModel;
using BotModel.Interfaces;
using System;
using static BotModel.TextMessageListener;
using System.Diagnostics;
using Telegram.Bot.Args;

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
            ISave _saver = new SaveTo();
            ITelegramBot bot = new TelegramBot();
                // ImageSaver.ImageConverted +=
                IImageSaver _userImageSaver = new ImageDownloader(bot.Client);
            IImageDownloader _imageDownloader = (IImageDownloader)_userImageSaver;
            IImageSaver _imageConverter = new ImageSaver(_saver);
            IImageConverter _convertEvent = (IImageConverter)_imageConverter;
            _convertEvent.ImageConverted += new ArchivationTool(new FilesOnServerInfoSender(bot.Client)).StartCompressing;
                IMessageSender fileSender = new FileOnRequestSender(bot.Client);
                IKeyboardable keyboardSender = (IKeyboardable)fileSender;

            IMessageListener _imageMessageListener = new ImageMessageListener(ref firstMessageFlag);
            INotifyImageMessageReieved _messageRecieved = (INotifyImageMessageReieved)_imageMessageListener;

                IMessageListener _textMessageListener = new TextMessageListener(
                    _imageConverter,
                    bot.Client,
                    new FileOnRequestSender(bot.Client, ref flagToGetFile, ref firstMessageFlag),
                    new FilesOnServerInfoSender(bot.Client));

            bot.Client.StartReceiving();
            bot.Client.OnMessage += _imageMessageListener.Listen;
            bot.Client.OnMessage += _textMessageListener.Listen;

            //_imageMessageListener.OnImageMessageReieved += new FileOnRequestSender().SendKeyboard;
            _messageRecieved.ImageMessageReieved += _userImageSaver.StartSave;
            keyboardSender.FilenameExtensionChoosen += OnImage;
            //FileOnRequestSender.OnFilenameExtensionChoosen += _imageCompressor.StartSave;
            _imageDownloader.OnImageDownloadFinish += keyboardSender.SendKeyboard; //_imageCompressor.StartSave;

        }
        public void OnImage(MessageEventArgs e)
        {
            Debug.WriteLine("MAIN on image");
        }
    }
}
