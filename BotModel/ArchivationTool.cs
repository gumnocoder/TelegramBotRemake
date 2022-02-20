using BotModel.Interfaces;
using System;
using System.IO;
using System.IO.Compression;
using Telegram.Bot.Args;

namespace BotModel
{
    [Obsolete]
    public class ArchivationTool : IArchivationTool
    {
        public ArchivationTool(IMessageSender archiveSender, string fileName)
        {
            _sender = archiveSender;
            _fileName = fileName;
            _archiveName = $"{fileName}.zip";
            OnArchivationComplete += _sender.Send;
        }

        IMessageSender _sender;
        string _archiveName, _fileName;
        public event IArchivationTool.ArchivationCompleteHandler OnArchivationComplete;

        public void StartCompressing(MessageEventArgs e)
        {
            using (FileStream file = new FileStream(_fileName, FileMode.OpenOrCreate))
            {
                using (FileStream archive = File.Create(_archiveName))
                {
                    using (GZipStream compression = new GZipStream(archive, CompressionMode.Compress))
                    {
                        file.CopyTo(compression);
                    }
                }
            }

            OnArchivationComplete?.Invoke(_archiveName, e);
            OnArchivationComplete -= _sender.Send;
        }
    }
}
