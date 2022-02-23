using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{

    [Obsolete]
    public class ImageDownloader : IImageSaver, INotifyImageDownloadFinish
    {
        public ImageDownloader(ITelegramBotClient Client)
        {
            this.Client = Client;
        }

        public event ImageDownloadFinishEventHandler ImageDownloadFinish;
        ITelegramBotClient Client; 

        async void OnImageDownloadFinish(MessageEventArgs e)
        {
            if (File.Exists(e.Message.MessageId.ToString() + ".jpg"))
            {
                await Task.Run(delegate ()
                    { 
                        if (new FileInfo(e.Message.MessageId.ToString() + ".jpg").Length > 0)
                        {
                            Debug.WriteLine("ImageDownloader.StartSave.File.Exists");
                            ImageDownloadFinish?.Invoke(e);
                        }
                    }
                );
            }
        }

        public async void StartSave(MessageEventArgs e)
        {
            using (FileStream fs = new(e.Message.MessageId.ToString() + ".jpg", FileMode.Create))
            {
                await Client.GetInfoAndDownloadFileAsync(e.Message.Photo[^1].FileId.ToString(), fs);
                OnImageDownloadFinish(e);
            }
        }
    }

}
