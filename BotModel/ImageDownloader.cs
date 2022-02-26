using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    /// <summary>
    /// Класс содержащий логику скачивания изображения из сообщения
    /// </summary>
    [Obsolete]
    public class ImageDownloader : IImageSaver, INotifyImageDownloadFinish
    {
        public ImageDownloader(ITelegramBotClient Client)
        {
            this.Client = Client;
        }

        public event ImageDownloadFinishEventHandler ImageDownloadFinish;
        ITelegramBotClient Client; 

        public void OnImageDownloadFinish(
            MessageEventArgs e, 
            string Filename,
            Telegram.Bot.Types.Message mess, 
            ITelegramBotClient Client)
        {
                ImageDownloadFinish?.Invoke(e, Filename, mess, Client);
        }
        
        public async void StartSave(MessageEventArgs e)
        {
            using (FileStream fs = new(e.Message.MessageId.ToString() + ".jpg", FileMode.Create))
            {
                await Client.GetInfoAndDownloadFileAsync(e.Message.Photo[^1].FileId.ToString(), fs);
            }
            
            string _file = e.Message.MessageId.ToString() + ".jpg";
            if (File.Exists(_file))
            {
                OnImageDownloadFinish(e, _file, e.Message, this.Client);
            }
        }
    }

}
