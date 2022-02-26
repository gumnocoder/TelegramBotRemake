using BotModel.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace BotModel
{
    /// <summary>
    /// Выполняет сохранение изображение в конкретном формате
    /// </summary>
    public class SaveTo : ISave
    {
        public void SaveToFile(string outputFile, Image img, ImageFormat format)
        {
            img.Save(outputFile, format);
        }
    }
}
