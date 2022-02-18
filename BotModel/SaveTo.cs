using BotModel.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace BotModel
{
    public class SaveTo : ISave
    {
        public void SaveToFile(string outputFile, Image img, ImageFormat format)
        {
            img.Save(outputFile, format);
        }
    }
}
