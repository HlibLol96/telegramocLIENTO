using System;
using System.Windows.Input;
using System.Windows;
using TelBot.Model;
using WTelegram;

namespace TelBot.ViewModel
{
    internal class JoinChannelViewModel
    {
        
        private ICommand joinCommand;
        public ICommand   JoinCommand => joinCommand;

        public JoinChannelViewModel()
        {
            joinCommand = new Command(DoJoinCommand, CanDo);
        }
        public void DoJoinCommand(object parameter) => MessageBox.Show("sendmes");

        private bool CanDo(object parameter) => true;


    }
}
