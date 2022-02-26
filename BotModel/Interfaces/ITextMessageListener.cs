namespace BotModel.Interfaces
{
    /// <summary>
    /// Для классов сканирующих текст сообщений
    /// </summary>
    public interface ITextMessageListener
    {
        /// <summary>
        /// Флаг первого сообщения
        /// </summary>
        bool FirstMessageFlag { get; set; }
    }
}
