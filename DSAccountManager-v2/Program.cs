using System;
using System.Windows.Forms;
using DSAccountManager_v2.GD;

namespace DSAccountManager_v2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //TODO: dis here for debug only!
            var u = new Universe(@"g:\Games\freelancer\fl-Disc487\dev");
            Application.Run(new Form1());
        }
    }
}
