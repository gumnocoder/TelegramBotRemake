using System;

namespace BotModel.Interfaces
{
    [Obsolete]
    public interface IFileConverterStarter
    {
        IImageConverter ImageConverter { get; set; }

        void SetImageConverter(IImageConverter _imageConverter);

    }
}
