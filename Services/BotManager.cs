using BotModel;
using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
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
        IImageConverter _imageConverter;

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
        public IImageConverter ImageConverter 
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

            FileSender = new FileOnRequestSender(Bot.Client);
            FilesOnServerInfoSender = new FilesOnServerInfoSender();
            TextMessageListener = new TextMessageListener();
            ImageMessageListener = new ImageMessageListener(
                (ITextMessageListener)TextMessageListener);

            UserImageSaver = new ImageDownloader(Bot.Client);
            ImageConverter = new ImageConverter
                ((IImageMessageListener)_imageMessageListener,
                (IFileRequester)TextMessageListener, 
                Saver);

            KeyboardSender = (IKeyboardable)FileSender;

            ArchivationTool = new ArchivationTool(FilesOnServerInfoSender);
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
            //((INotifyFilenameExtensionChoosen)_keyboardSender).FilenameExtensionChoosen += OnImage;
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
            //((INotifyFilenameExtensionChoosen)_keyboardSender).FilenameExtensionChoosen -= OnImage;
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
