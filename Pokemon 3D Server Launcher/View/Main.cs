using Modules.System;
using Pokemon_3D_Server_Launcher_Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pokemon_3D_Server_Launcher.View
{
    public partial class Main : Form
    {
        private Core Core;
        private List<string> LoggerLog = new List<string>();
        private bool ScrollTextBox = true;

        private delegate void LogMessageHandler(string Message);

        public Main()
        {
            InitializeComponent();

            Application.ThreadException += (sender2, ex) => { ex.Exception.CatchError(); };
            AppDomain.CurrentDomain.UnhandledException += (sender2, ex) => { ((Exception)ex.ExceptionObject).CatchError(); };

            Core = new Core();
            Core.Logger.OnLogMessageReceived += (sender, e) => LogMessage(e.Message);
            Core.Start(null);
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Stop(1);
        }

        public void LogMessage(string Message)
        {
            try
            {
                if (Main_Logger.InvokeRequired)
                    Main_Logger.BeginInvoke(new LogMessageHandler(LogMessage), Message);
                else
                {
                    if (ScrollTextBox)
                    {
                        if (!string.IsNullOrWhiteSpace(Main_Logger.Text))
                            Main_Logger.AppendText(Environment.NewLine);

                        Main_Logger.AppendText(Message);

                        if (Main_Logger.Lines.Length > 1000)
                            Main_Logger.Lines = Main_Logger.Lines.Skip(Main_Logger.Lines.Length - 1000).ToArray();
                    }
                    else
                    {
                        if (LoggerLog.Count > 1000)
                            LoggerLog.RemoveRange(0, 1000 - LoggerLog.Count);

                        LoggerLog.Add(Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.CatchError();
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