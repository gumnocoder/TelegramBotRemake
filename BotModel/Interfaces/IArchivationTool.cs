using System;
using Telegram.Bot.Args;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Интерфейс содержащий контрактры для 
    /// организации логики класса архиватора
    /// </summary>
    [Obsolete]
    public interface IArchivationTool
    {
        void StartCompressing(string fileName, MessageEventArgs e);
    }
}
