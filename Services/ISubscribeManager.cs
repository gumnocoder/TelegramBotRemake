using BotModel.Interfaces;
using System;

namespace Services
{
    /// <summary>
    /// Контракты для класса реализации подписок на события
    /// </summary>
    [Obsolete]
    public interface ISubscribeManager
    {
        ISave Saver { get; set; }
        IMessageSender FileSender { get; set; }
        IMessageSender FilesOnServerInfoSender { get; set; }
        IMessageListener TextMessageListener { get; set; }
        IMessageListener ImageMessageListener { get; set; }
        IImageSaver UserImageSaver { get; set; }
        IBotImageConverter ImageConverter { get; set; }
        IKeyboardable KeyboardSender { get; set; }
        IArchivationTool ArchivationTool { get; set; }

        /// <summary>
        /// Подписка на события
        /// </summary>
        public void Subscibe();
        /// <summary>
        /// Отписка от событий
        /// </summary>
        public void Unsubscribe();
    }
}
