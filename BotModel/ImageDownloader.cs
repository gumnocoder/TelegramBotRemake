using BotModel.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public delegate void ImageDownloadFinishHandler(MessageEventArgs e);

    [Obsolete]
    public class ImageDownloader : IImageSaver, IImageDownloader
    {
        public ImageDownloader(ITelegramBotClient Client)
        {
            this.Client = Client;
        }

        public event ImageDownloadFinishHandler OnImageDownloadFinish;
        ITelegramBotClient Client; 

        async void ImageDownloadFinish(MessageEventArgs e)
        {
            if (File.Exists(e.Message.MessageId.ToString() + ".jpg"))
            {
                await Task.Run(delegate ()
                    { 
                        if (new FileInfo(e.Message.MessageId.ToString() + ".jpg").Length > 0)
                        {
                            Debug.WriteLine("ImageDownloader.StartSave.File.Exists");
                            OnImageDownloadFinish?.Invoke(e);
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
                ImageDownloadFinish(e);
            }
        }
    }

    public interface IImageDownloader
    {
        [Obsolete]
        event ImageDownloadFinishHandler? OnImageDownloadFinish;
    }
}
