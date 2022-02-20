using BotModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using static BotModel.TelegramBot;

namespace BotModel
{
    [Obsolete]
    public class TextMessageListener : IMessageListener
    {
        public TextMessageListener(
            IMessageSender FileSender,
            IMessageSender InfoSender)
        {
            _fileSender = FileSender;
            _infoSender = InfoSender;
        }

        static bool _firstMessageFlag = true;
        static bool _flagToGetFile = false;
        static string _outputFilenameExtension;
        IMessageSender _fileSender, _infoSender;

        public static bool FirstMessageFlag
        {
            get => _firstMessageFlag;
            set => _firstMessageFlag = value;
        }

        public static bool FlagToGetFile
        {
            get => _flagToGetFile;
            set => _flagToGetFile = value;
        }

        public static string OutputFilenameExtension
        {
            get => _outputFilenameExtension;
            set => _outputFilenameExtension = value;
        }

        public void Listen(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;

            switch (text)
            {
                case "/start":
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
                    _infoSender.Send(e);
                    break;
                case "/getfile":
                    Client.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        $"Введите название файла который " +
                        $"хотите получить. Регистр букв учитывается");
                    FlagToGetFile = true;
                    break;
                default:
                    if (FlagToGetFile)
                    {
                        FirstMessageFlag = false;

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
                    else if (FirstMessageFlag)
                    {
                        Client.SendTextMessageAsync(
                            e.Message.Chat.Id,
                            "Воспользуйтесь командой /start");
                        FirstMessageFlag = false;
                    }
                    break;
            }

            if (ImageMessageListener.InputImageExists)
            {
                switch (text)
                {
                    case "BMP":
                        OutputFilenameExtension = ".bmp";
                        break;
                    case "PNG":
                        OutputFilenameExtension = ".png";
                        break;
                    case "GIF":
                        OutputFilenameExtension = ".gif";
                        break;
                    case "TIFF":
                        OutputFilenameExtension = ".tiff";
                        break;
                }
            }
        }
    }
}
