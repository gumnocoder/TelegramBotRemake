using BotModel.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using Telegram.Bot.Args;
using static BotModel.TelegramBot;

namespace BotModel
{
    [Obsolete]
    public class ImageSaver : IImageSaver
    {
        public ImageSaver(
            MessageEventArgs e, 
            ref string outputExtension, 
            ref bool inputImageExists,
            ISave Saver)
        {
            _inputImageExists = inputImageExists;
            _inputFile = e.Message.MessageId.ToString();
            _outputExtension = outputExtension;
            _outputFile = _inputFile + outputExtension;
            _saver = Saver;

            try
            {
                _image = Image.FromFile(_inputFile);
            }
            catch (Exception)
            {
                Client.SendTextMessageAsync(
                    e.Message.Chat.Id,
                    "Произошла непредвиденная ошибка");
            }
        }

        string _outputFile, _outputExtension, _inputFile;
        bool _inputImageExists;
        ISave _saver;
        Image _image;

        public delegate void ImageConvertFinishHandler();
        public static event ImageConvertFinishHandler OnImageConverted;

        public async void StartSave(MessageEventArgs e)
        {
            _inputImageExists = false;

            if (_image != null)
            {
                await Task.Run(() =>
                {
                    if (_outputExtension != default)
                    {
                        switch (_outputExtension)
                        {
                            case ".bmp":
                                _saver.SaveToFile(_outputFile, _image, ImageFormat.Bmp);
                                break;
                            case ".png":
                                _saver.SaveToFile(_outputFile, _image, ImageFormat.Png);
                                break;
                            case ".gif":
                                _saver.SaveToFile(_outputFile, _image, ImageFormat.Gif);
                                break;
                            case ".tiff":
                                _saver.SaveToFile(_outputFile, _image, ImageFormat.Tiff);
                                break;
                        }
                        _outputExtension = default;
                        OnImageConverted?.Invoke();
                    }
                });
            }
        }
    }
}
