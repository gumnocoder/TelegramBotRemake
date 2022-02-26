using BotModel.Interfaces;
using System;

namespace Services
{
    /// <summary>
    /// Контракты для сервиса инициализации бота
    /// </summary>
    [Obsolete]
    public interface IBotManager
    {
        /// <summary>
        /// Клиент
        /// </summary>
        ITelegramBot Bot { get; set; }
        /// <summary>
        /// Логика запуска клиента
        /// </summary>
        void StartBot();
        /// <summary>
        /// Логика остановки клиента
        /// </summary>
        void StopBot();
    }
}
