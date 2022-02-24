using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Diagnostics;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotModel
{
    [Obsolete]
    public class FileOnRequestSender : IMessageSender, IKeyboardable, INotifyFilenameExtensionChoosen
    {
        public FileOnRequestSender(ITelegramBotClient Client) { this.Client = Client; }


        public event FilenameExtensionChoosenHandler FilenameExtensionChoosen;
        ITelegramBotClient Client;

        public void OnFilenameExtensionChoosen(MessageEventArgs e)
        {
            FilenameExtensionChoosen?.Invoke(e);
        }

        public FileOnRequestSender(ITelegramBotClient Client, ref bool FlagToGetFile, ref bool FirstMessageFlag)
        { 
            _flagToGetFile = FlagToGetFile;
            _firstMessageFlag = FirstMessageFlag;
            this.Client = Client;
        }


        ReplyKeyboardMarkup _keyboard;
        private bool _flagToGetFile;
        bool _firstMessageFlag;

        public ReplyKeyboardMarkup Keyboard
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
        public  async void SendKeyboard(MessageEventArgs e)
        {
            _firstMessageFlag = false;
            await Client.SendTextMessageAsync(
                e.Message.Chat.Id.ToString(),
                "Выберите формат в который хотите конвертировать изображение",
                replyMarkup: Keyboard);
            OnFilenameExtensionChoosen(e);
            //FilenameExtensionChoosen(e);
            //OnFilenameExtensionChoosen(e);
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
