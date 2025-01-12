using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace bot
{
    internal class Program
    {
        private static List<History> SavedInfo= new List<History>();
        static async Task Main(string[] args)
        {


            var bot = new TelegramBotClient("7840398385:AAEFTXqg__uP6thIG9Pc5QlcdoRGI7StBUg");
            bot.StartReceiving(Update, Error);
            Console.ReadLine();

        }

        private static async Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private static async Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            if (update.Message != null)
            {
             
                if (update.Message.Text.ToLower().Contains(Commands.Start))
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "Hello there. If you dont know hte commands type /help");
                    return;
                }
                else if (update.Message.Text.ToLower().Contains(Commands.Help))
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "4 commands:" +
                        " \n /weather = put a city name after that" +
                        " \n /start = start the program" +
                        " \n /help = the command that u typed rn" +
                        " \n /history = shows all the weather history");
                    return;
                }
                else if (update.Message.Text.ToLower().Contains(Commands.Weather))
                {
                    try
                    {
                        //await client.SendTextMessageAsync(update.Message.Chat.Id,
                        //"Please enter a city name");
                        string city = update.Message.Text.ToLower().Split(" ")[1];
                        WebRequest sinoptik = WebRequest.Create($"http://api.openweathermap.org/geo/1.0/direct?q={city}&limit=9&appid=29c64ea1287275a9734ad1172864676a");
                        WebResponse response = sinoptik.GetResponse();
                        Stream stream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        string data = reader.ReadToEnd();
                        var serializeData = JsonConvert.DeserializeObject<List<CityInfo>>(data);
                        if (serializeData.Count > 0)
                        {
                            foreach (var temp in serializeData)
                            {
                                WebRequest tempSinoptik = WebRequest.Create($"https://api.openweathermap.org/data/2.5/weather?lat={temp.Lat}&lon={temp.Lon}&appid=29c64ea1287275a9734ad1172864676a");
                                WebResponse tempResponse = tempSinoptik.GetResponse();
                                Stream tempStream = tempResponse.GetResponseStream();
                                StreamReader tempReader = new StreamReader(tempStream);
                                string tempData = tempReader.ReadToEnd();
                                var tempInfo = JsonConvert.DeserializeObject<Weather>(tempData);
                                await client.SendTextMessageAsync(update.Message.Chat.Id, $"tempersture:{tempInfo.Main.Temp} timezone:{tempInfo.TimeZone}");
                                SavedInfo.Add(new History() { Command = Commands.Weather,Data = $"tempersture:{tempInfo.Main.Temp} timezone:{tempInfo.TimeZone}"});
                            }
                        }
                        // await client.SendTextMessageAsync(update.Message.Chat.Id, data);
                        return;
                    }
                    catch (Exception ex)
                    {
                        await client.SendTextMessageAsync(update.Message.Chat.Id, "Dont forget the city!!!");
                    }
                }
                else if (update.Message.Text.ToLower().Contains(Commands.History))
                {
                    try 
                    {
                        string data = " ";
                        foreach( var history in SavedInfo)
                        {
                            data += $"{history.Command} {history.Data}";
                        }
                        await client.SendTextMessageAsync(update.Message.Chat.Id, data);
                    }
                    catch(Exception ex)
                    {
                        await client.SendTextMessageAsync(update.Message.Chat.Id, "e");
                    }
                }

            }
        }
    }
}

