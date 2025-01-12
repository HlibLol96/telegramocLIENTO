using TL;
using WTelegram;
using System.Linq;
using System.Reflection.Metadata;
using System.Linq.Expressions;
namespace ApiBot
{
    internal class Program
    {
        private static int CompareUser(User user, User user2)
        {
            try
            {
                if (user != null && user2 != null
                    && user.MainUsername != null && user2.MainUsername != null)
                {
                    return user.first_name.CompareTo(user2?.first_name);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        static async Task Main()
        {
            #region login
            string _hash = "8d122d1b0ce540056d6ddf7fff5ac866";
            int _id = 21492715;
            string _number = "+380503863105";
            using var client = new Client(_id, _hash);
            var myself = await client.Login(_number);
            #endregion
            int switcher;
            while (true)
            {
                try
                {
                    Console.WriteLine("Type number:");
                    Console.WriteLine("1send mes 2 name finder 3spammer");
                    switcher = int.Parse(Console.ReadLine());
                    switch (switcher - 1)
                    {
                        case 0:
                            #region sendMes
                            var _chats = await client.Messages_GetAllChats();
                            foreach (var (Id, chat) in _chats.chats)
                            {
                                Console.WriteLine($"The id for {chat.Title} is {chat.ID} .");
                            }
                            long id = 0;
                            Console.WriteLine("Type the ID:");
                            id = long.Parse(Console.ReadLine());
                            await client.SendMessageAsync(_chats.chats[id], Console.ReadLine());

                            #endregion
                            break;
                        case 1:
                            #region name
                            try
                            {
                                var _contacts = await client.Contacts_GetContacts();
                                string _name;
                                var lisrtCont = _contacts.users.Values.ToList();
                                lisrtCont.Sort(CompareUser);
                                foreach (var contact in lisrtCont)
                                {
                                    Console.WriteLine((await client.Users_GetUsers(contact))[0].MainUsername);
                                }
                                Console.WriteLine("tell me the username");
                                string sendUser = Console.ReadLine();
                                var user = await client.Contacts_ResolveUsername(sendUser);
                                Console.WriteLine("Write mes");
                                string mes1 = Console.ReadLine();
                                await client.SendMessageAsync(user, mes1);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            #endregion
                            break;
                        case 2:
                            #region spam

                            var _chat = await client.Messages_GetAllChats();
                            int amount = 0;
                            long _nid = 0;
                            Console.WriteLine("Welcome to the spam machine");
                            Console.WriteLine("Get IDs from here and type one to where you want to spam");
                            Console.WriteLine();
                            Console.WriteLine();
                            foreach (var (Id, chat) in _chat.chats)
                            {
                                Console.WriteLine($"The id for {chat.Title} is {chat.ID} .");
                            }

                            Console.WriteLine("Type the ID:");
                            _nid = long.Parse(s: Console.ReadLine());
                            Console.WriteLine("Now the message:");
                            string mes = Console.ReadLine();
                            Console.WriteLine("Now the amount:");
                            amount = int.Parse(Console.ReadLine());
                            for (int i = 0; i < amount; i++)
                            {
                                await client.SendMessageAsync(_chat.chats[_nid], mes);
                            }
                            #endregion
                            break;
                        case 3:
                            #region join
                            try
                            {
                                Console.WriteLine("write a channle name:");
                                string channelName = Console.ReadLine();
                                var channel = await client.Contacts_Search(channelName);
                                foreach (var chan in channel.chats)
                                {
                                    Console.WriteLine(chan.Value.MainUsername);
                                }
                                Console.WriteLine("now type the chat:");
                                string name = Console.ReadLine();
                                await client.Channels_JoinChannel((Channel)channel.chats.Values.First( s => s.MainUsername == name));
                                
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex);
                                Console.WriteLine("try again");
                            }
                            #endregion
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}