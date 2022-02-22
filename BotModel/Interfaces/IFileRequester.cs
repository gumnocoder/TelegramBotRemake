namespace BotModel.Interfaces
{
    public interface IFileRequester
    {
        bool FlagToGetFile { get; set; }
        string OutputFilenameExtension { get; set; }
    }
}
