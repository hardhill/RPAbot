using RPAbot.Properties;
using System;
using System.IO;
using System.Windows.Forms;

namespace RPAbot
{
    public class Parameters
    {
        private Settings settings;
        public string htmldata { get 
            {
                string dir = settings.datahtml;
                if (!Directory.Exists(dir))
                {
                    try
                    {
                        var dirinfo = Directory.CreateDirectory(dir);
                        return dir;
                    }
                    catch (Exception err)
                    {
                        return Application.StartupPath;
                    }
                }
                else
                {
                    return dir;
                }
                
            }
        }
        public long interval { 
            get {
                return settings.interval;
            } }
        public string token { get { return settings.token; } }
        public int chatrpa { get { return Convert.ToInt32(settings.chatrpa); } }
        public Parameters()
        {
            this.settings = RPAbot.Properties.Settings.Default;
        }
    }
}