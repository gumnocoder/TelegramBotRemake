using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class ImageConverter : IImageConverter, INotifyImageConversion
    {
        public ImageConverter(
            IImageMessageListener ImageMessageListener,
            IFileRequester FileRequester,
            ISave Saver)
        {
            _imageMessageListener = ImageMessageListener;
            _fileRequester = FileRequester;
            _saver = Saver;
        }

        string _outputFile, _outputExtension, _inputFile;
        bool _inputImageExists;
        ISave _saver;
        IImageMessageListener _imageMessageListener;
        IFileRequester _fileRequester;
        Image _image;

        public event ImageConvertFinishHandler ImageConverted;

        public void OnImageConverted(string FileName, MessageEventArgs e)
        {
            ImageConverted?.Invoke(FileName, e);
        }

        public async void StartConvert(string Extension, MessageEventArgs e)
        {
            if (_imageMessageListener.InputImageExists)
            {
                _inputFile = e.Message.MessageId.ToString();
                _fileRequester.OutputFilenameExtension = Extension;
                string temp = _inputFile + Extension;

                try
                {
                    _image = Image.FromFile(temp, true);

                }
                catch (OutOfMemoryException ex)
                {

                    Debug.WriteLine(ex.GetType().ToString());
                    Debug.WriteLine(ex.Source);
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.InnerException);
                    Debug.WriteLine(ex.TargetSite);
                }
                catch (Exception)
                {
                    Debug.WriteLine("Произошла непредвиденная ошибка");
                }


                Debug.WriteLine($"_inputFile {_inputFile + ".jpg"}");
                Debug.WriteLine($"_inputFile.jpg exists {File.Exists(_inputFile + ".jpg")}");
                Debug.WriteLine("ImageSaver.StartSave");
                _inputFile = e.Message.MessageId.ToString();
                Debug.WriteLine($"_inputFile = {_inputFile}");
                _outputFile = $"{_inputFile}{_fileRequester.OutputFilenameExtension}";
                Debug.WriteLine($"_outputFile = {_outputFile}");
                //ImageMessageListener.inputImageExists = false;
                Debug.WriteLine($"ImageMessageListener.inputImageExists = {_imageMessageListener.InputImageExists}");
                Debug.WriteLine($"_image == null {_image == null}");
                if (_image != null)
                {
                    await Task.Run(() =>
                    {
                        Debug.WriteLine($"_image != null !!!!!! {_image != null}");
                        Debug.WriteLine($"TextMessageListener.outputFilenameExtension != default {_fileRequester.OutputFilenameExtension != default}");
                        if (_fileRequester.OutputFilenameExtension != default)
                        {
                            Debug.WriteLine($"TextMessageListener.outputFilenameExtension {_fileRequester.OutputFilenameExtension}");
                            switch (_fileRequester.OutputFilenameExtension)
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
                    });
                }
            }
        }
    }
}
