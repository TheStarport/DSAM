using System;
using System.Diagnostics;
using System.Windows.Forms;
using DSAccountManager_v2.GD;
using FLAccountDB;
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
            var db = new NoSQLDB(@"g:\flacc\flam2.db", @"d:\utrk\Docs\My Games\Freelancer\Accts\MultiPlayerHook");
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //db.LoadDB();
            //db.Update(new DateTime(2014,03,28,22,49,00));
            //Debug.WriteLine(sw.Elapsed.Minutes + "Min(s) " + sw.Elapsed.Seconds + "Sec(s)");
            //sw.Stop();
            var s = new FLHookTransport.Transport();
            s.OpenSocket("192.168.56.101", 1919, "test");
            //var v = s.GetPlayersOnline();
            //s.SetRep("testy", "li_p_grp", 0.5f);
            Application.Run(new Form1());
        }
    }
}
