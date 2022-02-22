using BotModel;
using BotModel.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Args;
using static BotModel.TelegramBot;

namespace Services
{
    [Obsolete]
    public class BotRunner
    {
        private readonly Window _window;
        private readonly ObservableCollection<User> _users;
        private IMessage _message;

        public BotRunner(ITelegramBotClient Client, Window window, ObservableCollection<User> users)
        {
            _window = window;
            _users = users;

            Client.StartReceiving();

            Client.OnMessage += MessageRegistrator;
            Client.OnMessage += MessageListener;
        }

        private User FindUser(long id, string name, string lastName)
        {
            foreach (var user in _users)
            {
                if (user.ID == id)
                {
                    return user;
                }
            }
            return new User(id, name, lastName);
        }
        private void MessageRegistrator(object? sender, MessageEventArgs e)
        {
            _window.Dispatcher.Invoke(() =>
                _message = new Message(
                    FindUser(
                        e.Message.Chat.Id, 
                        e.Message.Chat.FirstName, 
                        e.Message.Chat.LastName),
                DateTime.UtcNow, 
                e.Message.Text,
                e.Message.Chat.FirstName));
            Debug.WriteLine(e.Message.Chat.Id);
        }

        private void MessageListener(object? sender, MessageEventArgs e)
        {
            var message = e.Message.Text;

            if (message == "/start")
            {
                Debug.WriteLine("start command sent");
            }
        }
    }
}
