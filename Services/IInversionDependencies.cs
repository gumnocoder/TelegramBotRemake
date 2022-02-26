using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    /// <summary>
    /// Для логики инвертирования зависимостей
    /// </summary>
    public interface IInversionDependencies
    {
        /// <summary>
        /// Разворачивает зависимости
        /// </summary>
        void InvertDependencies();
    }
}
