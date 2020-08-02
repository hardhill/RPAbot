using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace RPAbot
{
    class BotShell
    {
        private Timer timer;
        private Parameters parameters;
        private WorkData workData;
        public delegate void DoWork(object eventArgs);
        public event DoWork OnDoWork;
        public delegate void StartTimer(object e);
        public event StartTimer OnStartTimer;
        public delegate void StopTimer(object e);
        public event StartTimer OnStopTimer;
        public BotShell(Parameters parameters)
        {

            timer = new Timer();
            this.parameters = parameters;
            workData = new WorkData();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = TimeSpan.FromSeconds(parameters.interval).TotalMilliseconds;
        }
        public void Start()
        {
            workData.Reset();
            timer.Start();
            OnStartTimer?.Invoke(workData);

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ReadDataHtml();
            OnDoWork?.Invoke(workData);
        }

        private void ReadDataHtml()
        {
            IEnumerable<string> files = Directory.EnumerateFiles(parameters.htmldata);
            foreach (var ff in files)
            {
                using (StreamReader sr = new StreamReader(ff, Encoding.UTF8))
                {
                    string html = sr.ReadToEnd();

                }
            }
        }

        public void Stop()
        {
            timer.Stop();
            OnStopTimer?.Invoke(workData);
        }


    }

    internal class WorkData
    {
        private int workCounter;
        public int WorkCounter { get { return workCounter++; } }
        public WorkData()
        {
            workCounter = 0;
        }
        public void Reset()
        {
            workCounter = 0;
        }
    }
}
