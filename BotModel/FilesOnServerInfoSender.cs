using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Telegram.Bot.Args;

namespace BotModel
{
    /// <summary>
    /// Класс содержащий логику отправки списка файлов, 
    /// находящихся в директории с исполняющим файлом приложения, 
    /// по запросу пользователя
    /// </summary>
    [Obsolete]
    public class FilesOnServerInfoSender : IMessageSender, INotifyListRequest
    {

        DirectoryInfo _path;
        string _filesList = string.Empty;
        ObservableCollection<string> _files;

        public event ListRequestEventHandler ListRequest;

        /// <summary>
        /// запрос списка файлов ListRequest с проверкой на null
        /// </summary>
        /// <param name="e"></param>
        /// <param name="FilesList"></param>
        public void OnListRequest(MessageEventArgs e, string FilesList)
        {
            ListRequest?.Invoke(e, FilesList);
        }

        /// <summary>
        /// Путь к файлам
        /// </summary>
        public DirectoryInfo Path 
        {
            get 
            {
                if (_path == default)
                { _path = new(Environment.CurrentDirectory); }
                return _path; 
            }
            set
            {
                _path = value;
            }
        }

        /// <summary>
        /// Список файлов
        /// </summary>
        public ObservableCollection<string> Files
        { 
            get 
            { 
                if (_files == default)
                { _files = new(); }
                return _files; 
            }
            set => _files = value; 
        }

        public void Send(MessageEventArgs e)
        {
            if (Files.Count == 0) { BuildDirectoryView(); }
            SendDirectoryViewOnRequest(e);
            OnListRequest(e, _filesList);
            _filesList = string.Empty;
        }

        public void Send(string message, MessageEventArgs e)
        {
            Send(e);
        }

        /// <summary>
        /// Заполняет список файлов игнорируя
        /// файлы с указанными расширениями
        /// </summary>
        private void BuildDirectoryView()
        {
            foreach (var file in Path.GetFiles())
            {
                if (!file.Name.ToString().Contains(".ini") &&
                    !file.Name.ToString().Contains(".dll") && 
                    !file.Name.ToString().Contains(".json") && 
                    !file.Name.ToString().Contains(".pdb") &&
                    !file.Name.ToString().Contains(".exe") &&
                    !file.Name.ToString().Contains(".xml"))
                {
                    Files.Add(file.Name.ToString());
                }
            }
        }



        /// <summary>
        /// конвертирует коллекцию<string> в string для 
        /// отправки одним сообщением
        /// </summary>
        /// <param name="e"></param>
        private void SendDirectoryViewOnRequest(MessageEventArgs e)
        {
            foreach (var file in Files)
            {
                _filesList += file.ToString() + "\n";
            }
        }
    }
}
