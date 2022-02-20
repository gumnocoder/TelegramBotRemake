using BotModel.Interfaces;
using System;
using System.IO;
using Telegram.Bot.Args;
using static BotModel.TelegramBot;

namespace BotModel
{
    [Obsolete]
    public class ImageDownloader : IImageSaver
    {

        public delegate void ImageDownloadFinishHandler();
        public event ImageDownloadFinishHandler ImageDownloadFinish;

        public async void StartSave(MessageEventArgs e)
        {
            using (FileStream fs = new(e.Message.MessageId.ToString() + ".jpg", FileMode.Create))
            {
                await Client.GetInfoAndDownloadFileAsync(e.Message.Photo[^1].FileId.ToString(), fs);
                ImageDownloadFinish?.Invoke();
            }
        }
    }
}
