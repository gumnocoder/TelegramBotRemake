using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class TextMessageListener : 
        IMessageListener,
        IFileRequester,
        ITextMessageListener,
        INotifyExtensionChoosen,
        INotifyMessageRequest,
        INotifyInfoRequest,
        INotifyFileRequest
    {

        bool _firstMessageFlag = true;
        bool _flagToGetFile = false;
        string _outputFilenameExtension;


        public event IExtensionChoosenHandler ExtensionChoosen;
        public event Notifications.MessageEventRequestHandler MessageRequest;
        public event InfoRequestEventHandler InfoRequest;
        public event FileRequestEventHanlder FileRequest;

        public void OnMessageRequest(MessageEventArgs e, byte Code)
        {
            MessageRequest?.Invoke(e, Code);
        }
        public void OnExtensionChoosen(string Extension, MessageEventArgs e)
        {
            ExtensionChoosen?.Invoke(Extension, e);
        }

        public void OnInfoRequest(MessageEventArgs e)
        {
            InfoRequest?.Invoke(e);
        }

        public void OnFileRequest(string FileName, MessageEventArgs e)
        {
            FileRequest?.Invoke(FileName, e);
        }

        public bool FlagToGetFile
        { get => _flagToGetFile; set => _flagToGetFile = value; }
        public string OutputFilenameExtension 
        { get => _outputFilenameExtension; set => _outputFilenameExtension = value; }
        public bool FirstMessageFlag 
        { get => _firstMessageFlag; set => _firstMessageFlag = value; }

        public void Listen(object sender, MessageEventArgs e)
        {
            var text = e.Message.Text;

            switch (text)
            {
                case "/start":
                    OnMessageRequest(e, 1);
                    _firstMessageFlag = false;
                    break;
                case "/getdir":
                    _firstMessageFlag = false;
                    OnInfoRequest(e);
                    break;
                case "/getfile":
                    OnMessageRequest(e, 2);
                    _firstMessageFlag = false;
                    _flagToGetFile = true;
                    break;
                case "BMP" or "PNG" or "GIF" or "TIFF":
                    OnExtensionChoosen($".{text.ToLower()}", e);
                    break;
                default:
                    if (_flagToGetFile)
                    {
                        _firstMessageFlag = false;

                        if (text != default)
                        {
                            OnFileRequest(text, e);
                        }
                        else
                        {
                            OnMessageRequest(e, 3);
                        }
                    }
                    else if (_firstMessageFlag)
                    {
                        OnMessageRequest(e, 4);
                        _firstMessageFlag = false;
                    }
                    break;
            }
        }
    }
}
