namespace BotModel.Interfaces
{
    public interface ITextMessageListener
    {
        bool FirstMessageFlag { get; set; }

        IImageMessageListener MessageListener { get; set; }

        void SetImageMessageListener(IImageMessageListener MessageListener);
    }
}
