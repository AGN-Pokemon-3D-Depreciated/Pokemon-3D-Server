using Pokemon_3D_Server_Launcher.View;
using System;
using System.Windows.Forms;

namespace Pokemon_3D_Server_Launcher
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}
