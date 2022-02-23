using BotModel;
using BotModel.Interfaces;
using System;
using static BotModel.TextMessageListener;
using System.Diagnostics;
using Telegram.Bot.Args;
using BotModel.Notifications;

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

            
            

                IMessageListener _textMessageListener = new TextMessageListener();
            IMessageSender _filesOnServerInfoSender = new FilesOnServerInfoSender();
            ((INotifyMessageRequest)_textMessageListener).MessageRequest += bot.OnMessageReactions;
            ((INotifyListRequest)_filesOnServerInfoSender).ListRequest += bot.OnContentMessageReactions;
            ((INotifyInfoRequest)_textMessageListener).InfoRequest += _filesOnServerInfoSender.Send;


            IMessageListener _imageMessageListener = new ImageMessageListener((ITextMessageListener)_textMessageListener);
           // INotifyImageMessageReieved _messageRecieved = (INotifyImageMessageReieved)_imageMessageListener;
           // ((ITextMessageListener)_textMessageListener).SetImageMessageListener((IImageMessageListener)_imageMessageListener);

            IImageConverter _imageConverter = 
                new ImageConverter(
                    (IImageMessageListener)_imageMessageListener, 
                    (IFileRequester)_textMessageListener, 
                    _saver);

            //((IFileConverterStarter)_textMessageListener).SetImageConverter(_imageConverter);

            //INotifyImageConvertersion _convertEvent = (INotifyImageConvertersion)_imageConverter;
            ((INotifyImageConversion)_imageConverter).ImageConverted += 
                new ArchivationTool(new FilesOnServerInfoSender()).StartCompressing;

            ((INotifyExtensionChoosen)_textMessageListener).ExtensionChoosen +=
                _imageConverter.StartConvert;

            bot.Client.StartReceiving();
            bot.Client.OnMessage += _imageMessageListener.Listen;
            bot.Client.OnMessage += _textMessageListener.Listen;

            //_imageMessageListener.OnImageMessageReieved += new FileOnRequestSender().SendKeyboard;
            ((INotifyImageMessageReieved)_imageMessageListener).ImageMessageReieved += _userImageSaver.StartSave;
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
