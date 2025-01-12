using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TelBot.View;
using TelBot.Model;

namespace TelBot.ViewModel
{
    internal class ChooserViewModel
    {

        private string _text = "choose one";

        public event PropertyChangedEventHandler? PropertyChanged;
        private ICommand sendCommand;
        public ICommand SendCommand => sendCommand;

        private ICommand joinCommand;
        public ICommand JoinCommand => joinCommand;

        private ICommand findCommand;
        public ICommand FindCommand => findCommand;

        public ChooserViewModel()
        {

            joinCommand = new Command(DoJoinCommand, CanDo);
            sendCommand = new Command(DoSendCommand, CanDo);
            findCommand = new Command(DoFindCommand, CanDo);
        }
        public void DoSendCommand(object parameter)
        {
            General general = new General();
            general.Show();
        }
        public void DoJoinCommand(Object skiif) => MessageBox.Show("hello world");
        public void DoFindCommand(Object skiif) => MessageBox.Show("hello world");
        private bool CanDo(object parameter) => true;
       
     
    }
}

