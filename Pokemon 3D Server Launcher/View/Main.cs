using Modules.System;
using Pokemon_3D_Server_Core;
using Pokemon_3D_Server_Core.Logger;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Pokemon_3D_Server_Launcher.View
{
    public partial class Main : Form, ILogger
    {
        private delegate void LogMessageSafe(string Message);

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Core.Logger.RegisterLog(this);
            Program.instance.Start();
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.instance.Stop();
        }

        /// <summary>
        /// Print stuff to console.
        /// </summary>
        public void LogMessage(string Message)
        {
            try
            {
                if (Main_Logger.InvokeRequired)
                {
                    Main_Logger.BeginInvoke(new LogMessageSafe(LogMessage), Message);
                    return;
                }

                int SelectionStart = Main_Logger.SelectionStart;
                int SelectionLength = Main_Logger.SelectionLength;

                if (!string.IsNullOrWhiteSpace(Main_Logger.Text))
                    Main_Logger.AppendText(Environment.NewLine);

                Main_Logger.AppendText(Message);

                if (Main_Logger.Lines.Length > 1000)
                    Main_Logger.Lines = Main_Logger.Lines.Skip(Main_Logger.Lines.Length - 1000).ToArray();
            }
            catch (Exception ex)
            {
                ex.CatchError();
            }
        }
    }
}
