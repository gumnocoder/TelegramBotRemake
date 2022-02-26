using BotModel;
using BotModel.Interfaces;
using BotModel.Notifications;
using Services;
using Services.Commands;
using System;
using System.Diagnostics;
using Telegram.Bot.Args;

namespace TelegramBotRemake.ViewModel
{
    [Obsolete]
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    internal class MainWindowVM : BaseVM
    {
        IBotManager _botManager;
        bool _botActivityFlag;
        string _indicator = "Включить";
        public MainWindowVM()
        {
            _botManager = new BotManager();
            _botActivityFlag = false;
            #region
            /*            ISave _saver = new SaveTo();
                        ITelegramBot bot = new TelegramBot();

                        IMessageSender _fileSender = new FileOnRequestSender(bot.Client);
                        IMessageSender _filesOnServerInfoSender = new FilesOnServerInfoSender();
                        IMessageListener _textMessageListener = new TextMessageListener();
                        IMessageListener _imageMessageListener = 
                            new ImageMessageListener((ITextMessageListener)_textMessageListener);

                        IImageSaver _userImageSaver = new ImageDownloader(bot.Client);
                        IImageConverter _imageConverter = 
                            new ImageConverter((IImageMessageListener)_imageMessageListener, (IFileRequester)_textMessageListener, _saver);

                        IKeyboardable _keyboardSender = (IKeyboardable)_fileSender;

                        ((INotifyMessageRequest)_textMessageListener).MessageRequest += bot.OnMessageReactions;
                        ((INotifyListRequest)_filesOnServerInfoSender).ListRequest += bot.OnContentMessageReactions;
                        ((INotifyInfoRequest)_textMessageListener).InfoRequest += _filesOnServerInfoSender.Send;

                        bot.Client.StartReceiving();
                        bot.Client.OnMessage += _imageMessageListener.Listen;
                        bot.Client.OnMessage += _textMessageListener.Listen;
                        ((INotifyImageConversion)_imageConverter).ImageConverted +=
                            new ArchivationTool(new FilesOnServerInfoSender()).StartCompressing;
                        ((INotifyImageMessageReieved)_imageMessageListener).ImageMessageReieved += _userImageSaver.StartSave;
                        ((INotifyFilenameExtensionChoosen)_keyboardSender).FilenameExtensionChoosen += OnImage;
                        ((INotifyExtensionChoosen)_textMessageListener).ExtensionChoosen += _imageConverter.StartConvert;
                        ((INotifyFileRequest)_textMessageListener).FileRequest += _fileSender.Send;
                        ((INotifyImageDownloadFinish)_userImageSaver).ImageDownloadFinish += _keyboardSender.SendKeyboard;*/

            // ImageSaver.ImageConverted +=







            // INotifyImageMessageReieved _messageRecieved = (INotifyImageMessageReieved)_imageMessageListener;
            // ((ITextMessageListener)_textMessageListener).SetImageMessageListener((IImageMessageListener)_imageMessageListener);



            //((IFileConverterStarter)_textMessageListener).SetImageConverter(_imageConverter);

            //INotifyImageConvertersion _convertEvent = (INotifyImageConvertersion)_imageConverter;



            //_imageMessageListener.OnImageMessageReieved += new FileOnRequestSender().SendKeyboard;

            //FileOnRequestSender.OnFilenameExtensionChoosen += _imageCompressor.StartSave;


            //_imageCompressor.StartSave;
            #endregion
        }

        public string Indicator
        {
            get => _indicator;
            set
            {
                _indicator = value;
                OnPropertyChanged();
            }
        }
        private RelayCommand _botSwitcher;
        public RelayCommand BotSwitcher =>
            _botSwitcher ??= new(BotSwitcherCommand);

        private void BotSwitcherCommand(object sender)
        {
            if (!_botActivityFlag)
            {
                _botManager.StartBot();
                Indicator = "Выключить";
            }
            else
            {
                _botManager.StopBot();
                Indicator = "Включить";
            }
            _botActivityFlag = !_botActivityFlag;
        }
    }
}
