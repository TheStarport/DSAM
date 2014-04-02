﻿using System;
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
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //db.LoadDB();
            //db.Update(new DateTime(2014,03,28,22,49,00));
            //Debug.WriteLine(sw.Elapsed.Minutes + "Min(s) " + sw.Elapsed.Seconds + "Sec(s)");
            //sw.Stop();
            //var s = new FLHookTransport.Transport();


            //DBiFace.InitiateHook("localhost",1919,"test");
            //s.SetRep("testy", "li_p_grp", 0.5f);
            Application.Run(new MainForm());
        }
    }
}
