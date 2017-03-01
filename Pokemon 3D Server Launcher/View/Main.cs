using Modules.System;
using Pokemon_3D_Server_Launcher_Core;
using Pokemon_3D_Server_Launcher_Core.Interfaces;
using Pokemon_3D_Server_Launcher_Core.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pokemon_3D_Server_Launcher.View
{
    public partial class Main : Form
    {
        private ICore Core;
        private List<string> LoggerLog = new List<string>();
        private bool ScrollTextBox = true;

        public Main()
        {
            InitializeComponent();

            Core = new Core();
            Core.Logger.OnLogMessageReceived += (sender, e) => Main_Logger.BeginInvoke(new EventHandler<LoggerEventArgs>(LogMessage), sender, e);
            Core.Start(Core);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Stop(1);
        }

        public void LogMessage(object sender, LoggerEventArgs e)
        {
            if (ScrollTextBox)
            {
                if (!string.IsNullOrWhiteSpace(Main_Logger.Text))
                    Main_Logger.AppendText(Environment.NewLine);

                Main_Logger.AppendText(e.Message);

                if (Main_Logger.Lines.Length > 1000)
                    Main_Logger.Lines = Main_Logger.Lines.Skip(Main_Logger.Lines.Length - 1000).ToArray();
            }
            else
            {
                if (LoggerLog.Count > 1000)
                    LoggerLog.RemoveRange(0, 1000 - LoggerLog.Count);

                LoggerLog.Add(e.Message);
            }
        }

        private void Main_Logger_TextChanged(object sender, EventArgs e)
        {
            if (ScrollTextBox)
            {
                Main_Logger.SelectionStart = Main_Logger.TextLength;
                Main_Logger.ScrollToCaret();
            }
        }

        private void Main_Logger_Leave(object sender, EventArgs e)
        {
            ScrollTextBox = true;

            if (LoggerLog.Count > 0)
            {
                for (int i = 0; i < LoggerLog.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(Main_Logger.Text))
                        Main_Logger.AppendText(Environment.NewLine);

                    Main_Logger.AppendText(LoggerLog[i]);
                }

                LoggerLog.RemoveRange(0, LoggerLog.Count);
            }

            Main_Logger.SelectionStart = Main_Logger.TextLength;
            Main_Logger.ScrollToCaret();
        }

        private void Main_Logger_Enter(object sender, EventArgs e)
        {
            ScrollTextBox = false;
        }
    }
}