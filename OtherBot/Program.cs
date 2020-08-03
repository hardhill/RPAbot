using NetTelegramBotApi;
using NetTelegramBotApi.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OtherBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            WebProxy proxyObject = new WebProxy("http://10.3.239.2:3128/",true);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
           // ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            TelegramBot bot = new TelegramBot("1333442954:AAEA2Zn5jkvRc3ag6Cv8qdJyX2Bo8G3Fb68");
            var req = new GetMe();
            bot.WebProxy = proxyObject;
            var me = await bot.MakeRequestAsync(req);
        }
    }
}
