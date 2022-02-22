using BotModel.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class TextMessageListener : IMessageListener, IFileRequester, ITextMessageListener, IFileConverterStarter
    {
        public TextMessageListener(
            ITelegramBotClient Client,
            IMessageSender FileSender,
            IMessageSender InfoSender)
        {
            _fileSender = FileSender;
            _infoSender = InfoSender;
            this.Client = Client;
        }

        bool _firstMessageFlag = true;
        bool _flagToGetFile = false;
        string _outputFilenameExtension;
        IMessageSender _fileSender, _infoSender;
        IImageMessageListener _messageListener;
        ITelegramBotClient Client;
        IImageSaver _imageConverter;

        public bool FlagToGetFile
        { get => _flagToGetFile; set => _flagToGetFile = value; }
        public string OutputFilenameExtension 
        { get => _outputFilenameExtension; set => _outputFilenameExtension = value; }
        public bool FirstMessageFlag 
        { get => _firstMessageFlag; set => _firstMessageFlag = value; }
        public IImageMessageListener MessageListener 
        { get => _messageListener; set => _messageListener = value; }
        public IImageSaver ImageConverter { get => _imageConverter; set => _imageConverter = value; }

        public void SetImageConverter(IImageSaver _imageConverter)
        {
            ImageConverter = _imageConverter;
        }
        public void SetImageMessageListener(IImageMessageListener MessageListener)
        {
            _messageListener = MessageListener;
        }
        public void Listen(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;

            switch (text)
            {
                case "/start":
                    _firstMessageFlag = false;
                    Client.SendTextMessageAsync(e.Message.Chat.Id.ToString(),
                        "Добро пожаловать в конвертер изображений!\n\n" +
                        "Отправьте изображение, которое хотите сохранить в другой формат, " +
                        "выберите конечный формат и бот отправит Вам " +
                        "заархивированный файл в нужном формате. " +
                        "Доступные форматы JPEG, PNG, BMP и GIF." +
                        "Обратите внимание что фотографии необходимо " +
                        "присылать по одной, в противном случае будет " +
                        "конвертировано только последнее изображение.\n\n" +
                        "Список команд:\n\n" +
                        "/start инструкция по использованию\n" +
                        "/getdir список файлов для скачивания\n" +
                        "/getfile отправляет запрос на отправку файла с сервера\n\n" +
                        "Внимание, регистр букв учитывается!");
                    break;
                case "/getdir":
                    _firstMessageFlag = false;
                    _infoSender.Send(e);
                    break;
                case "/getfile":
                    _firstMessageFlag = false;
                    Client.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        $"Введите название файла который " +
                        $"хотите получить. Регистр букв учитывается");
                    _flagToGetFile = true;
                    break;
                default:
                    if (_flagToGetFile)
                    {
                        _firstMessageFlag = false;

                        if (text != default)
                        {
                            _fileSender.Send(text, e);
                        }
                        else
                        {
                            Client.SendTextMessageAsync(
                                e.Message.Chat.Id,
                                "Невозможно распознать запрос");
                        }
                    }
                    else if (_firstMessageFlag)
                    {
                        Client.SendTextMessageAsync(
                            e.Message.Chat.Id,
                            "Воспользуйтесь командой /start");
                        _firstMessageFlag = false;
                    }
                    break;
            }

            if (_messageListener.InputImageExists)
            {
                //IImageSaver _imageCompressor = new ImageSaver(/*ref outputFilenameExtension, ref inputImageExists, */new SaveTo());

                switch (text)
                {
                    case "BMP":
                        _outputFilenameExtension = ".bmp";
                        break;
                    case "PNG":
                        _outputFilenameExtension = ".png";
                        break;
                    case "GIF":
                        _outputFilenameExtension = ".gif";
                        break;
                    case "TIFF":
                        _outputFilenameExtension = ".tiff";
                        break;
                }
                _imageConverter.StartSave(e);
            }
        }
    }

    public interface IFileConverterStarter
    {
        IImageSaver ImageConverter { get; set; }

        void SetImageConverter(IImageSaver _imageConverter);

    }
}
