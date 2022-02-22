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
            
            
            
                IMessageSender fileSender = new FileOnRequestSender(bot.Client);
                IKeyboardable keyboardSender = (IKeyboardable)fileSender;

            
            

                IMessageListener _textMessageListener = new TextMessageListener(
                    bot.Client,
                    new FileOnRequestSender(bot.Client),
                    new FilesOnServerInfoSender(bot.Client));

            
            IMessageListener _imageMessageListener = new ImageMessageListener((ITextMessageListener)_textMessageListener);
            INotifyImageMessageReieved _messageRecieved = (INotifyImageMessageReieved)_imageMessageListener;
            ((ITextMessageListener)_textMessageListener).SetImageMessageListener((IImageMessageListener)_imageMessageListener);

            IImageSaver _imageConverter = 
                new ImageSaver(
                    (IImageMessageListener)_imageMessageListener, 
                    (IFileRequester)_textMessageListener, 
                    _saver);

            ((IFileConverterStarter)_textMessageListener).SetImageConverter(_imageConverter);

            IImageConverter _convertEvent = (IImageConverter)_imageConverter;
            _convertEvent.ImageConverted += new ArchivationTool(new FilesOnServerInfoSender(bot.Client)).StartCompressing;

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
