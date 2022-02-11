using System.Windows;
using static BotModel.TelegramBot;
using System;
using BotModel;
using Services;

namespace TelegramBotRemake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Obsolete]
        public MainWindow()
        {
            InitializeComponent();
            new BotRunner(this, BotModel.User.Users);
            //Client.StartReceiving();
        }
    }
}
