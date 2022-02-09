using System;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;

namespace BotModel
{
    public class TelegramBot
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public TelegramBot()
        {
            try
            {
                if (File.Exists(_tokenPath))
                {
                    using StreamReader sr = new StreamReader(_tokenPath);
                    _token = sr.ReadToEnd().ToString();
                }
                else
                {
                    throw new FileNotFoundException($"{_tokenPath} not  exists!");
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.WriteLine(e.Message);
                Environment.Exit(0);
            }
            Debug.WriteLine($"{_token} loaded!");
        }

        #region Поля

        private static string _tokenPath = "token.ini";

        private static string _token;

        private static TelegramBotClient _client;

        #endregion

        #region Свойства

        /// <summary>
        /// Инстанс бота
        /// </summary>
        public static TelegramBotClient Client
        {
            get
            {
                if (_client == null)
                {
                    _client = new(_token);
                }
                return _client;
            }
        }
        #endregion
    }
}
