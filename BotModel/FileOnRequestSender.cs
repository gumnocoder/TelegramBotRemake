using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotModel
{
    /// <summary>
    /// Представляет логику передачи 
    /// </summary>
    [Obsolete]
    public class FileOnRequestSender :
        IMessageSender, 
        IKeyboardable,
        INotifyFilenameExtensionChoosen
    {
        public FileOnRequestSender(
            ITelegramBotClient Client, 
            ITextMessageListener TextListener, 
            IFileRequester FileRequester)
        {
            _fileRequester = FileRequester;
            _textListener = TextListener;
            this.Client = Client;
        }

        public event FilenameExtensionChoosenHandler FilenameExtensionChoosen;

        /// <summary>
        /// Назначение расширения с проверкой на Null
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Filename"></param>
        /// <param name="mess"></param>
        /// <param name="Client"></param>
        public void OnFilenameExtensionChoosen(
            MessageEventArgs e,
            string Filename, 
            Telegram.Bot.Types.Message mess,
            ITelegramBotClient Client)
        {
            FilenameExtensionChoosen?.Invoke(e, Filename, mess, Client);
        }

        ReplyKeyboardMarkup _keyboard;
        ITextMessageListener _textListener;
        IFileRequester _fileRequester;
        ITelegramBotClient Client;

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
        public  async void SendKeyboard(
            MessageEventArgs e, 
            string Filename, 
            Telegram.Bot.Types.Message mess,
            ITelegramBotClient Client)
        {
            _textListener.FirstMessageFlag = false;
            await Client.SendTextMessageAsync(
                e.Message.Chat.Id.ToString(),
                "Выберите формат в который хотите конвертировать изображение",
                replyMarkup: Keyboard);
            OnFilenameExtensionChoosen(e, Filename, mess, Client);
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
                    _fileRequester.FlagToGetFile = false;

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
