using System;

namespace BotModel.Interfaces
{
    public interface IMessage
    {
        DateTime Time { get; }
        string MessageText { get; }
        string Sender { get; set; }
    }
}
