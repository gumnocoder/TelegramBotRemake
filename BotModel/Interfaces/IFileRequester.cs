namespace BotModel.Interfaces
{
    /// <summary>
    /// Запрос файла с сервера
    /// </summary>
    public interface IFileRequester
    {
        bool FlagToGetFile { get; set; }
        string OutputFilenameExtension { get; set; }
    }
}
