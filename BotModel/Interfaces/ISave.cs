using System.Drawing;
using System.Drawing.Imaging;

namespace BotModel.Interfaces
{
    public interface ISave
    {
        void SaveToFile(string outputFile, Image img, ImageFormat format);
    }
}
