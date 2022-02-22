using BotModel.Interfaces;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class TextMessageListener : IMessageListener
    {
        public TextMessageListener(
            IImageSaver ImageSaver,
            ITelegramBotClient Client,
            IMessageSender FileSender,
            IMessageSender InfoSender)
        {
            _fileSender = FileSender;
            _infoSender = InfoSender;
            this.Client = Client;
            _imageSaver = ImageSaver;
        }

        public static bool firstMessageFlag = true;
        public static bool flagToGetFile = false;
        public static string outputFilenameExtension;
        IMessageSender _fileSender, _infoSender;
        ITelegramBotClient Client;
        IImageSaver _imageSaver;

        public void Listen(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;

            switch (text)
            {
                case "/start":
                    firstMessageFlag = false;
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
                    firstMessageFlag = false;
                    _infoSender.Send(e);
                    break;
                case "/getfile":
                    firstMessageFlag = false;
                    Client.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        $"Введите название файла который " +
                        $"хотите получить. Регистр букв учитывается");
                    flagToGetFile = true;
                    break;
                default:
                    if (flagToGetFile)
                    {
                        firstMessageFlag = false;

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
                    else if (firstMessageFlag)
                    {
                        Client.SendTextMessageAsync(
                            e.Message.Chat.Id,
                            "Воспользуйтесь командой /start");
                        firstMessageFlag = false;
                    }
                    break;
            }

            if (ImageMessageListener.inputImageExists)
            {
                //IImageSaver _imageCompressor = new ImageSaver(/*ref outputFilenameExtension, ref inputImageExists, */new SaveTo());

                switch (text)
                {
                    case "BMP":
                        outputFilenameExtension = ".bmp";
                        break;
                    case "PNG":
                        outputFilenameExtension = ".png";
                        break;
                    case "GIF":
                        outputFilenameExtension = ".gif";
                        break;
                    case "TIFF":
                        outputFilenameExtension = ".tiff";
                        break;
                }
                _imageSaver.StartSave(e);
            }
        }
    }
}
