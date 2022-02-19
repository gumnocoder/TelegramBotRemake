using BotModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Telegram.Bot.Args;
using static BotModel.TelegramBot;

namespace BotModel
{
    [Obsolete]
    public class FilesOnServerInfoViewer : IMessageSender
    {
        public FilesOnServerInfoViewer(MessageEventArgs e) => 
            Send(e);

        static DirectoryInfo _path;
        static ObservableCollection<string> _files;
        public static DirectoryInfo Path 
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

        public static ObservableCollection<string> Files
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
            BuildDirectoryView();
            SendDirectoryViewOnRequest(e);
        }

        private static void BuildDirectoryView()
        {
            foreach (var file in Path.GetFiles())
            {
                Files.Add(file.Name.ToString());
            }
        }

        private static void SendDirectoryViewOnRequest(
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
