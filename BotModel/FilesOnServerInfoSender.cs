using BotModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class FilesOnServerInfoSender : IMessageSender
    {
        public FilesOnServerInfoSender(ITelegramBotClient Client) { this.Client = Client; }

        DirectoryInfo _path;
        ObservableCollection<string> _files;
        ITelegramBotClient Client;
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
        }

        public void Send(string message, MessageEventArgs e)
        {
            Send(e);
        }

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

        private void SendDirectoryViewOnRequest(
            MessageEventArgs e)
        {
            var id = e.Message.Chat.Id.ToString();
            string filesList = string.Empty;
            foreach (var file in Files)
            {
                filesList += file.ToString() + "\n";
            }

            Client.SendTextMessageAsync(
                id,
                "Список файлов доступных к скачиванию:\n\n" + filesList);
        }
    }
}
