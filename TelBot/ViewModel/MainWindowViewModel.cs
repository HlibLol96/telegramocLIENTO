using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using TelBot.Model;
using WTelegram;
using TelBot.View;

namespace TelBot.ViewModel
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged (nameof(Password));
            }
        }
     
        private ICommand startCommand;
        public ICommand StartCommand => startCommand;

        private ICommand acceptCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand AcceptCommand => acceptCommand;

        public MainWindowViewModel()
        {
            startCommand = new Command(DoStartCommand, CanDo);
            acceptCommand = new Command(DoAcceptCommand, CanDo);
        }
        public void DoStartCommand(object parameter)
        {
            TelegramClient.Instance.Initialize();
        }
        public void DoAcceptCommand(Object paarameter)
        {
            
            Chooser chooser = new Chooser();
            TelegramClient.Instance.ApplyCode(password);
            chooser.Show();
        }
        private bool CanDo(object parameter) => true;

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
