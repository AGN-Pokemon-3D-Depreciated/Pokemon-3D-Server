using Modules.System;
using Pokemon_3D_Server_Core;
using Pokemon_3D_Server_Launcher.View;
using System;
using System.Windows.Forms;

namespace Pokemon_3D_Server_Launcher
{
    public class Program
    {
        /// <summary>
        /// Get Core instance.
        /// </summary>
        public static Core instance { get; private set; }

        [STAThread]
        public static void Main(string[] args)
        {
            instance = new Core(args);

            Application.ThreadException += (sender, ex) => { ex.Exception.CatchError(); };
            AppDomain.CurrentDomain.UnhandledException += (sender, ex) => { ((Exception)ex.ExceptionObject).CatchError(); };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
