using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPAbot
{
    public partial class Form1 : Form
    {
        public Parameters parameters { get; set; }
        private List<string> htmls;
        private BotShell botShell;
        
        public Form1()
        {
            InitializeComponent();
            parameters = new Parameters();
            htmls = new List<string>();
            botShell = new BotShell(parameters);
            botShell.OnDoWork += BotShell_OnDoWork;
            botShell.OnStartTimer += BotShell_OnStartTimer;
            
        }

        private void BotShell_OnStartTimer(object e)
        {
            WorkData workData = (WorkData)e;
            lblStatus.Text = workData.WorkCounter.ToString();
        }

        private void BotShell_OnDoWork(object eventArgs)
        {
            WorkData workData = (WorkData)eventArgs;
            lblStatus.Text = workData.WorkCounter.ToString();
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            bStart.Enabled = false;
            botShell.Start();
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            botShell.Stop();
            bStart.Enabled = true;
        }
    }
}
