using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using TelBot.Model;

namespace TelBot.ViewModel
{
    internal class FindNameViewModel : INotifyPropertyChanged
    {
        private ICommand acceptCommand;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ICommand AcceptCommand => acceptCommand;

        public FindNameViewModel()
        {
            acceptCommand = new Command(DoAcceptCommand, CanDo);
        }
        public void DoAcceptCommand(object parameter) => MessageBox.Show("sendmes");

        private bool CanDo(object parameter) => true;

    }
}
