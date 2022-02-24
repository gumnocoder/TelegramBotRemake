using BotModel.Interfaces;
using BotModel.Notifications;
using System;
using System.IO;
using System.IO.Compression;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class ArchivationTool : IArchivationTool, INotifyArchivationComplete
    {
        public ArchivationTool(IMessageSender archiveSender)
        {
            _sender = archiveSender;
            ArchivationComplete += _sender.Send;
        }

        IMessageSender _sender;
        string _archiveName;
        public event ArchivationCompleteEventHandler ArchivationComplete;

        public void OnArchivationComplete(string FileName, MessageEventArgs e)
        {
            ArchivationComplete?.Invoke(FileName, e);
        }
        public void StartCompressing(string fileName, MessageEventArgs e)
        {
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                _archiveName = $"{fileName}.zip";

                using (FileStream archive = File.Create(_archiveName))
                {
                    using (GZipStream compression = new GZipStream(archive, CompressionMode.Compress))
                    {
                        file.CopyTo(compression);
                    }
                }
            }

            OnArchivationComplete(_archiveName, e);
            ArchivationComplete -= _sender.Send;
        }
    }
}
