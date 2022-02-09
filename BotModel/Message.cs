﻿using BotModel.Base;
using System.Collections.ObjectModel;

namespace BotModel
{
    /// <summary>
    /// Класс представляющий сообщение
    /// </summary>
    public class Message : BaseNotificationClass
    {
        #region Конструкторы

        public Message() { }

        public Message(
            string time,
            string messageText,
            string firstName,
            long id)
        {
            Time = time;
            MessageText = messageText;
            FirstName = firstName;
            Id = id;
        }

        #endregion

        #region Поля

        private string _time, _messageText, _firstName;
        private long _id;

        #endregion

        #region Свойства

        public string Time
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

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public long Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
