using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    /// <summary>
    ///Класс содержжащий логику конвертирования изображения
    /// </summary>
    [Obsolete]
    public class BotImageConverter : IBotImageConverter, INotifyImageConversion
    {
        public BotImageConverter(
            IImageMessageListener ImageMessageListener,
            IFileRequester FileRequester,
            ISave Saver)
        {
            _imageMessageListener = ImageMessageListener;
            _fileRequester = FileRequester;
            _saver = Saver;
        }

        string _outputFile, _inputFile;
        ISave _saver;
        IImageMessageListener _imageMessageListener;
        IFileRequester _fileRequester;
        Image _image;
        Telegram.Bot.Types.Message _mess;
        ITelegramBotClient Client;

        public event ImageConvertFinishHandler ImageConverted;

        /// <summary>
        /// Вызывает событие ImageConverted c проверкой на null
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="e"></param>
        public void OnImageConverted(string FileName, MessageEventArgs e)
        {
            ImageConverted?.Invoke(FileName, e);
        }

        public void GetParams(
            MessageEventArgs e, 
            string Filename,
            Telegram.Bot.Types.Message mess, 
            ITelegramBotClient Client)
        {
            _mess = mess;
            Debug.WriteLine($"_mess != null {_mess != null}");
            this.Client = Client;
            Debug.WriteLine($"Client != null {Client != null}");
            _inputFile = Filename;
        }

        public async void StartConvert(string Extension, MessageEventArgs e)
        {
            Debug.WriteLine($"StartConvert");
            if (_imageMessageListener.InputImageExists)
            {
                Debug.WriteLine($"_imageMessageListener.InputImageExists");
                using (FileStream fs = new("tmp", FileMode.Create))
                {
                    Debug.WriteLine($"FileStream fs = new(tmp, FileMode.Create");
                    await Client.GetInfoAndDownloadFileAsync(_mess.Photo[^1].FileId.ToString(), fs);
                    Debug.WriteLine($"{_mess.Photo[^1].FileId.ToString()}");
                    _image = Image.FromStream(fs);
                    Debug.WriteLine($"_image != null {_image != null}");

                    _outputFile = $"{_inputFile}{Extension}";

                    switch (Extension)
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

                    _fileRequester.OutputFilenameExtension = default;
                    OnImageConverted(_outputFile, e);
                }
            }
        }
    }
}
