namespace BotModel.Interfaces
{
    /// <summary>
    /// Для классов сканирующих сообщения на присутствие изображений
    /// </summary>
    public interface IImageMessageListener
    {
        /// <summary>
        /// Флаг сигнализирующий о присутствии изображения в сообщении
        /// </summary>
        bool InputImageExists { get; set; }
    }
}
