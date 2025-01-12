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
using TelBot.Model;
using System.Windows.Navigation;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using TL;

namespace TelBot.ViewModel
{
    class GeneralViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private string text; 
        public string Text 
        {
            get => text;
            set 
            {
                text =  value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public string Amount
        {
            get => amount;
            set
            {
               amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string amount;
        private string id;
       
        public ICommand AcceptCommand;

        public async void  DoAcceptCommand(object parameter)
        {
            try
            {
                long _id = long.Parse(Id);
                int count = int.Parse(Amount);
                await TelegramClient.Instance.SendMessage(_id, Text, count);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ObservableCollection<ChatBase> _collection;
        public ObservableCollection<ChatBase> Collection
        {
            get => _collection;
            set => _collection = value; 
        }

        public GeneralViewModel()
        {
         AcceptCommand = new Command(DoAcceptCommand,(object parameter) =>true );
            Collection = new ObservableCollection<ChatBase>();
            ShowStuff();
        }
        public async void ShowStuff()
        {
            var _chats = await TelegramClient.Instance.GetChatsAsync();
                foreach (var (Id, chat) in _chats.chats)
                {
               Collection.Add(chat);
               
                }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propertyName) );
        }
        
    }
}
