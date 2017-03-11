using Modules.System;
using Pokemon_3D_Server_Launcher_Core;
using Pokemon_3D_Server_Launcher_Core.Logger;
using Pokemon_3D_Server_Launcher_Core.PlayerList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using static Pokemon_3D_Server_Launcher_Core.PlayerList.PlayerListEventArgs;

namespace Pokemon_3D_Server_Launcher.View
{
    public partial class Main : Form
    {
        private Core Core;
        private List<string> LoggerLog = new List<string>();
        private bool ScrollTextBox = true;
        private BindingList<PlayerListEventArgs> PlayerList = new BindingList<PlayerListEventArgs>() { AllowEdit = true, AllowNew = true, AllowRemove = true };

        public Main()
        {
            InitializeComponent();

            Core = new Core();
            Application.ThreadException += (sender, ex) => { ex.Exception.CatchError(); };
            AppDomain.CurrentDomain.UnhandledException += (sender, ex) => { ((Exception)ex.ExceptionObject).CatchError(); };
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Core.Logger.OnLogMessageReceived += (sender2, e2) => Main_Logger.BeginInvoke(new EventHandler<LoggerEventArgs>(LogMessage), sender2, e2);
            Core.PlayerList.OnPlayerListUpdate += (sender2, e2) => Main_PlayerList.BeginInvoke(new EventHandler<PlayerListEventArgs>(UpdatePlayerList), sender2, e2);
            Core.Start();

            Main_PlayerList.DataSource = PlayerList;
            Main_PlayerList.DisplayMember = "Data";
            Main_PlayerList.ValueMember = "Id";
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Stop(0);
        }

        private void LogMessage(object sender, LoggerEventArgs e)
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

        private void UpdatePlayerList(object sender, PlayerListEventArgs e)
        {
            if (e.Operation == Operations.Add)
            {
                if (!PlayerList.Any(a => a.Id == e.Id))
                    PlayerList.Add(e);
            }
            else if (e.Operation == Operations.Remove)
            {
                if (PlayerList.Any(a => a.Id == e.Id))
                    PlayerList.Remove(PlayerList.Where(a => a.Id == e.Id).First());
            }
            else if (e.Operation == Operations.Update)
            {
                if (PlayerList.Any(a => a.Id == e.Id))
                {
                    PlayerListEventArgs currentValue = PlayerList.Where(a => a.Id == e.Id).First();
                    currentValue = e;
                }
            }
        }
    }
}