using BotModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    /// <summary>
    /// Класс представляющий клиент телеграмм бота
    /// </summary>
    [Obsolete]
    public class TelegramBot : ITelegramBot
    {

        #region Поля

        private string _tokenPath = "token.ini";

        private string _token, _content;

        private ITelegramBotClient  _client;

        #endregion

        #region Свойства

        /// <summary>
        /// Инстанс бота
        /// </summary>
        public ITelegramBotClient  Client
        {
            get
            {
                if (_client == null)
                {
                    if (_token == default)
                    {
                        SetToken(_tokenPath, ref _token);
                    }
                    _client = new TelegramBotClient(_token);
                }
                return _client;
            }
        }

        #endregion

        #region Методы

        private void SetToken(
            string tokenPath,
            ref string token)
        {
            try
            {
                if (File.Exists(_tokenPath))
                {
                    using StreamReader sr = new StreamReader(tokenPath);
                    token = sr.ReadToEnd().ToString();
                }
                else
                {
                    throw new FileNotFoundException($"{tokenPath} not  exists!");
                }
            }
            catch (FileNotFoundException e)
            {
                Environment.Exit(0);
            }
        }

        public void OnContentMessageReactions(MessageEventArgs e, string Content)
        {
            _content = Content;
            Client.SendTextMessageAsync(
                e.Message.Chat.Id,
                "Список файлов доступных к скачиванию:\n\n" + _content);
        }

        public void OnMessageReactions(MessageEventArgs e, byte Code)
        {
            switch (Code)
            {
                case 1:
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
                case 2:
                    Client.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        $"Введите название файла который " +
                        $"хотите получить. Регистр букв учитывается");
                    break;
                case 3:
                    Client.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        "Невозможно распознать запрос");
                    break;
                case 4:
                    SendDefaultMessage(e);
                    break;
                case 5:
                    Client.SendTextMessageAsync(
                        e.Message.Chat.Id,
                        "Невозможно распознать запрос");
                    break;
                default:
                    SendDefaultMessage(e);
                    break;
            }
        }

        private void SendDefaultMessage(MessageEventArgs e)
        {
            Client.SendTextMessageAsync(
                e.Message.Chat.Id,
                "Воспользуйтесь командой /start");
        }

        #endregion
    }
}
