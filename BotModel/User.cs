using BotModel.Base;
using BotModel.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace BotModel
{
    public class User : BaseNotificationClass, IUser
    {
        public User(
            long id,
            string firstName,
            string lastName)
        {
            _id = id;
            FirstName = firstName;
            LastName = lastName;
            Users.Add(this);
            _messages = new();
            Debug.WriteLine(this);
        }

        private readonly long _id;
        private string _firstName;
        private string _lastName;
        private ObservableCollection<IMessage> _messages;
        private static ObservableCollection<User> _users;

        public long ID 
        {
            get => _id;
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

        public string LastName 
        {
            get => _lastName; 
            set
            {
                _lastName = value;
                OnPropertyChanged(); 
            } 
        }

        public static ObservableCollection<User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new();
                }
                return _users;
            }
        }

        public ObservableCollection<IMessage> Messages => _messages;

        public override string ToString()
        {
            return $"user {ID} - {FirstName} {LastName}";
        }
    }
}
