using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using TelBot.ViewModel;
using TL;
using WTelegram;

namespace TelBot.Model
{
    public class TelegramClient
    {
       
        private Client _client;
        private static TelegramClient _instance;
        public static TelegramClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TelegramClient();
                }
                return _instance;
            }
        }
        public async Task<Messages_Chats> GetChatsAsync()
        {
            var _chats = await _client.Messages_GetAllChats();
            return _chats;
        }

        public async Task SendMessage(long id,string text,int count = 1)
        {
            var _chats = await _client.Messages_GetAllChats();
            for(int i = 0;i < count; i++)
            {
                await _client.SendMessageAsync(_chats.chats[id], text);
            }
        }
        public void Initialize()
        {
            string _hash = "8d122d1b0ce540056d6ddf7fff5ac866";
            int _id = 21492715;
            string _number = "+380503863105";
            _client = new Client(_id, _hash);
            _client.Login(_number);
        }
        public void ApplyCode(string code)
        {
            _client.Login(code);
        }
        private TelegramClient()
        {
           
        }
    }
}
