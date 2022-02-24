using BotModel.Interfaces;
using System;

namespace Services
{
    [Obsolete]
    public interface IBotManager
    {
        ITelegramBot Bot { get; set; }
        void StartBot();

        void StopBot();
    }
}
