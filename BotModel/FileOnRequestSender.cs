using BotModel.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using static BotModel.TelegramBot;

namespace BotModel
{
    [Obsolete]
    public class FileOnRequestSender : IMessageSender
    {
        public FileOnRequestSender(ref bool FlagToGetFile)
        { 
            _flagToGetFile = FlagToGetFile;
        }

        static ReplyKeyboardMarkup _keyboard;
        private bool _flagToGetFile;

        public static ReplyKeyboardMarkup Keyboard
        {
            get
            {
                if (_keyboard == null)
                {
                    _keyboard = new ReplyKeyboardMarkup
                    {
                        Keyboard = new[] 
                        {
                            new[]
                            {
                                new KeyboardButton("BMP"),
                                new KeyboardButton("PNG"),
                                new KeyboardButton("GIF"),
                                new KeyboardButton("TIFF"),
                             },
                        },
                        ResizeKeyboard = true,
                        OneTimeKeyboard = true,
                    };
                }
                return _keyboard;
            }
            set => _keyboard = value;
        }

        public void Send(MessageEventArgs e)
        {
            Send("empty", e);
        }

        public void Send(string attribute, MessageEventArgs e)
        {
            SendFileFromServer(attribute, e);
        }
        private async void SendKeyboard(MessageEventArgs e)
        {
            await Client.SendTextMessageAsync(
                e.Message.Chat.Id.ToString(),
                "Выберите формат в который хотите конвертировать изображение",
                replyMarkup: Keyboard);
        }

        private async void SendFileFromServer(
            string file, 
            MessageEventArgs e)
        {
            string path = Path.Combine(Environment.CurrentDirectory + @"\" + file);

            try
            {
                using (Stream stream = File.OpenRead(path))
                {
                    _flagToGetFile = false;

                    await Client.SendDocumentAsync(
                        chatId: e.Message.Chat.Id.ToString(),
                        document: new InputOnlineFile(
                            content: stream,
                            fileName: file),
                        caption: file);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
