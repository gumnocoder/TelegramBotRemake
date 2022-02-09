using System.Windows;
using static BotModel.TelegramBot;
using System;

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
            Client.StartReceiving();
        }
    }
}
