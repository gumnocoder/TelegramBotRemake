using BotModel.Base;
using BotModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BotModel
{
    /// <summary>
    /// Класс представляющий сообщение
    /// </summary>
    public class Message : BaseNotificationClass, IMessage
    {
        #region Конструкторы


        public Message(
            IUser user,
            DateTime time,
            string messageText, 
            string sender)
        {
            Time = time;
            MessageText = messageText;
            user.Messages.Add(this);
            Sender = sender;
            Debug.WriteLine(this);
        }

        #endregion

        #region Поля

        private string  _messageText;
        private DateTime _time;
        private string _sender;
        private long _id;

        #endregion

        #region Свойства

        public string Sender
        {
            get => _sender;
            set
            {
                _sender = value;
                OnPropertyChanged();
            }
        }
        public DateTime Time
        {
            get => _time;
            set
            {
                _time = value;
                OnPropertyChanged();
            }
        }

        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Sender} {Time} : {MessageText}";
        }
    }
}
