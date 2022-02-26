using System.Drawing;
using System.Drawing.Imaging;

namespace BotModel.Interfaces
{
    /// <summary>
    /// Для реализации в классах выполняющих сохранение изображений
    /// </summary>
    public interface ISave
    {
        /// <summary>
        /// Логика сохранения изображений
        /// </summary>
        /// <param name="outputFile">название конечного файла</param>
        /// <param name="img">сохраняемое изображение</param>
        /// <param name="format">конечный формат изображения</param>
        void SaveToFile(string outputFile, Image img, ImageFormat format);
    }
}
