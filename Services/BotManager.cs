using BotModel;
using BotModel.Interfaces;
using BotModel.Notifications;
using System;

namespace Services
{
    /// <summary>
    /// Представляет рычаги управления ботом
    /// </summary>
    [Obsolete]
    public class BotManager : ISubscribeManager, IInversionDependencies, IBotManager
    {
        public BotManager()
        {
            InvertDependencies();
        }

        ISave _saver;
        ITelegramBot _bot;

        IMessageSender _fileSender, _filesOnServerInfoSender;
        IMessageListener _textMessageListener, _imageMessageListener;

        IImageSaver _userImageSaver;
        IBotImageConverter _imageConverter;

        IKeyboardable _keyboardSender;

        IArchivationTool _archivationTool;

        public ISave Saver
        { get => _saver; set => _saver = value; }
        public ITelegramBot Bot
        { get =>_bot; set => _bot = value; }
        public IMessageSender FileSender 
        { get => _fileSender; set => _fileSender = value; }
        public IMessageSender FilesOnServerInfoSender
        { get => _filesOnServerInfoSender; set => _filesOnServerInfoSender = value; }
        public IMessageListener TextMessageListener 
        { get => _textMessageListener; set => _textMessageListener = value; }
        public IMessageListener ImageMessageListener 
        { get => _imageMessageListener; set => _imageMessageListener = value; }
        public IImageSaver UserImageSaver 
        { get => _userImageSaver; set => _userImageSaver = value; }
        public IBotImageConverter ImageConverter 
        { get => _imageConverter; set => _imageConverter = value; }
        public IKeyboardable KeyboardSender
        { get => _keyboardSender; set => _keyboardSender = value; }
        public IArchivationTool ArchivationTool 
        { get => _archivationTool; set => _archivationTool = value; }

        public void StartBot()
        {
            Bot.Client.StartReceiving();
            Subscibe();
        }

        public void InvertDependencies()
        {
            Saver = new SaveTo();
            Bot = new TelegramBot();

            FilesOnServerInfoSender = new FilesOnServerInfoSender();
            TextMessageListener = new TextMessageListener();

            FileSender = new FileOnRequestSender(
                Bot.Client,
                ((ITextMessageListener)TextMessageListener), 
                ((IFileRequester)TextMessageListener));

            ImageMessageListener = new ImageMessageListener(
                (ITextMessageListener)TextMessageListener);

            UserImageSaver = new ImageDownloader(Bot.Client);

            ImageConverter = new BotImageConverter
                ((IImageMessageListener)_imageMessageListener,
                (IFileRequester)TextMessageListener, 
                Saver);

            KeyboardSender = (IKeyboardable)FileSender;

            ArchivationTool = new ArchivationTool();
        }
        public void Subscibe()
        {
            Bot.Client.OnMessage += _imageMessageListener.Listen;
            Bot.Client.OnMessage += _textMessageListener.Listen;

            ((INotifyMessageRequest)TextMessageListener).MessageRequest += Bot.OnMessageReactions;
            ((INotifyListRequest)FilesOnServerInfoSender).ListRequest += Bot.OnContentMessageReactions; 
            ((INotifyInfoRequest)TextMessageListener).InfoRequest += FilesOnServerInfoSender.Send; 
            ((INotifyImageConversion)ImageConverter).ImageConverted += ArchivationTool.StartCompressing;
            ((INotifyImageMessageReieved)ImageMessageListener).ImageMessageReieved += UserImageSaver.StartSave;
            ((INotifyFilenameExtensionChoosen)_keyboardSender).FilenameExtensionChoosen += ImageConverter.GetParams;
            ((INotifyArchivationComplete)ArchivationTool).ArchivationComplete += FileSender.Send;
            ((INotifyExtensionChoosen)TextMessageListener).ExtensionChoosen += ImageConverter.StartConvert;
            ((INotifyFileRequest)TextMessageListener).FileRequest += FileSender.Send;
            ((INotifyImageDownloadFinish)UserImageSaver).ImageDownloadFinish += KeyboardSender.SendKeyboard;
        }

        public void Unsubscribe()
        {
            Bot.Client.OnMessage -= _imageMessageListener.Listen;
            Bot.Client.OnMessage -= _textMessageListener.Listen;

            ((INotifyMessageRequest)TextMessageListener).MessageRequest -= Bot.OnMessageReactions;
            ((INotifyListRequest)FilesOnServerInfoSender).ListRequest -= Bot.OnContentMessageReactions;
            ((INotifyInfoRequest)TextMessageListener).InfoRequest -= FilesOnServerInfoSender.Send;
            ((INotifyImageConversion)ImageConverter).ImageConverted -= ArchivationTool.StartCompressing;
            ((INotifyImageMessageReieved)ImageMessageListener).ImageMessageReieved -= UserImageSaver.StartSave;
            ((INotifyFilenameExtensionChoosen)_keyboardSender).FilenameExtensionChoosen -= ImageConverter.GetParams;
            ((INotifyArchivationComplete)ArchivationTool).ArchivationComplete -= FileSender.Send;
            ((INotifyExtensionChoosen)TextMessageListener).ExtensionChoosen -= ImageConverter.StartConvert;
            ((INotifyFileRequest)TextMessageListener).FileRequest -= FileSender.Send;
            ((INotifyImageDownloadFinish)UserImageSaver).ImageDownloadFinish -= KeyboardSender.SendKeyboard;
        }

        public void StopBot()
        {
            Unsubscribe();
            Bot.Client.StopReceiving();
        }
    }
}
