using BotModel.Interfaces;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Telegram.Bot.Args;

namespace BotModel
{
    public delegate void ImageConvertFinishHandler(string FileName, MessageEventArgs e);

    [Obsolete]
    public class ImageSaver : IImageSaver, IImageConverter
    {
        public ImageSaver(
            IImageMessageListener ImageMessageListener,
            IFileRequester FileRequester,
           // ref string outputExtension, 
           //ref bool inputImageExists,
            ISave Saver)
        {
            _imageMessageListener = ImageMessageListener;
            _fileRequester = FileRequester;
            //_outputFile = _inputFile + outputExtension;
            _saver = Saver;


        }

        string _outputFile, _outputExtension, _inputFile;
        bool _inputImageExists;
        ISave _saver;
        IImageMessageListener _imageMessageListener;
        IFileRequester _fileRequester;
        //static Image _image;


        public event ImageConvertFinishHandler ImageConverted;

        public async void StartSave(MessageEventArgs e)
        {
            _inputFile = e.Message.MessageId.ToString();
            Image _image = null;
            _imageMessageListener.InputImageExists = true;
            string temp = _inputFile + ".jpg";
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
                        ImageConverted?.Invoke(_outputFile, e);
                    }
                });
/*                    await Task.Run(() =>
                    {
                        if (TextMessageListener.outputFilenameExtension != default)
                        {
                            switch (TextMessageListener.outputFilenameExtension)
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
                            TextMessageListener.outputFilenameExtension = default;
                            OnImageConverted?.Invoke(_outputFile, e);
                        }
                    });*/
                }
        }
    }

    public interface IImageConverter
    {
        event ImageConvertFinishHandler ImageConverted;
    }
}
