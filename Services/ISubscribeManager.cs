using BotModel;
using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [Obsolete]
    public interface ISubscribeManager
    {
        ISave Saver { get; set; }
        IMessageSender FileSender { get; set; }
        IMessageSender FilesOnServerInfoSender { get; set; }
        IMessageListener TextMessageListener { get; set; }
        IMessageListener ImageMessageListener { get; set; }
        IImageSaver UserImageSaver { get; set; }
        IImageConverter ImageConverter { get; set; }
        IKeyboardable KeyboardSender { get; set; }
        IArchivationTool ArchivationTool { get; set; }

        public void Subscibe();
        public void Unsubscribe();
    }
}
