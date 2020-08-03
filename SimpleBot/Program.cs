using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace SimpleBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient("1333442954:AAEA2Zn5jkvRc3ag6Cv8qdJyX2Bo8G3Fb68");
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls|SecurityProtocolType.Ssl3;
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var me = await bot.GetMeAsync();
            Console.WriteLine(me.Username);
            Console.ReadLine();
        }
    }
}
