using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace RPAbot
{
    class BotShell
    {
        private System.Timers.Timer timer;
        private Parameters parameters;
        
        private ObservableCollection<string> htmls;
        private TelegramBotClient client;
        
        public delegate void DoWork();
        public event DoWork OnDoWork;
        public delegate void StartTimer(object e);
        public event StartTimer OnStartTimer;
        public delegate void StopTimer(object e);
        public event StartTimer OnStopTimer;
        public BotShell(Parameters parameters)
        {
            htmls = new ObservableCollection<string>();
            timer = new System.Timers.Timer();
            this.parameters = parameters;
            client = new TelegramBotClient(parameters.token);
            //следующая строка обязательная для инициализации соединения бота
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = TimeSpan.FromSeconds(parameters.interval).TotalMilliseconds;
        }
        public async Task Start()
        {
            WorkData.Reset();
            htmls.Clear();
            timer.Start();
             
            var me = await client.GetMeAsync();
            WorkData.BotId = me.Id;
            //client.StartReceiving();
            OnStartTimer?.Invoke(null);
        }

        

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadDataHtml();
            SendMessageHtml();
            OnDoWork?.Invoke();
        }

        private async void SendMessageHtml()
        {
            try
            {
                await client?.SendTextMessageAsync(new ChatId(parameters.chatrpa), "<b>Hello from winbot</b>",ParseMode.Html);
            }catch(Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }

        private void ReadDataHtml()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(parameters.htmldata);
            foreach (var ff in files)
            {
                using (StreamReader sr = new StreamReader(ff, Encoding.UTF8))
                {
                    string html = sr.ReadToEnd();
                    htmls.Add(html);
                }
                try
                {
                    System.IO.File.Delete(ff);
                }catch(Exception er)
                {

                }
            }
        }

        public void Stop()
        {
            //client.StopReceiving();
            timer.Stop();
            OnStopTimer?.Invoke(null);
        }


    }

    internal static class WorkData
    {
        private static int workCounter = 0;

        public static int WorkCounter => workCounter++;
        public static int BotId {get;set;}
        public static void Reset()
        {
            workCounter = 0;
            BotId = 0;
        }

    }
}
