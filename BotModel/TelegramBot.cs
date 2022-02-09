using System;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;

namespace BotModel
{
    [Obsolete]
    public static class TelegramBot
    {
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

        private static void SetToken(
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
                Debug.WriteLine(e.Message);
                Environment.Exit(0);
            }
            Debug.WriteLine($"{token} loaded!");
        }

        #endregion
    }
}
