﻿using System;
using System.Globalization;
using System.Windows.Forms;
using FLAccountDB.NoSQL;

namespace DSAccountManager_v2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            DBiFace.DBPercentChanged += DBiFace_DBPercentChanged;
            DBiFace.DBStateChanged += DBiFace_DBStateChanged;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DBiFace.InitiateDB(Properties.Settings.Default.DBType,
                Properties.Settings.Default.DBPath,
                Properties.Settings.Default.FLDBPath);

            if (!DBiFace.IsDBAvailable())
            {
                //Database not found/set up
                var set = new Forms.Settings();
                set.ShowDialog();
                return;
            }

            if (DBiFace.DBCountRows("Accounts") == 0)
                if (MessageBox.Show(
                    @"DB is not initialized. Initialize and scan now?",
                    @"Database is empty",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Database is empty
                    DBiFace.InitDB(Properties.Settings.Default.DBAggressiveScan);
                    return;
                }

            DBiFace.UpdateDB();
        }

        void DBiFace_DBStateChanged(DBStates state)
        {
            var str = "";
            switch (state)
            {
                case DBStates.Ready:
                        str = "DB Ready";
                        break;
                    case DBStates.Initiating:
                        str = "Rescanning...";
                        break;
                    case DBStates.Closed:
                        str = "Closed";
                        break;
                    case DBStates.UpdatingFormFiles:
                        str = "Searching for updates...";
                        break;
                    case DBStates.Updating:
                        str = "Updating...";
                        break;
            }

            Action action = () => toolStatus.Text = str;
            //if (toolProgress.Control.InvokeRequired)
            Invoke(action);
        }

        private void DBiFace_DBPercentChanged(int percent, int qcount)
        {
            Action action = () =>
            {
                toolProgress.Value = percent;
                toolDBQueue.Text = qcount.ToString(CultureInfo.InvariantCulture);
            };
            //if (toolProgress.Control.InvokeRequired)
            toolProgress.Control.Invoke(action);
        }



        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DBiFace.IsDBAvailable())
                fastObjectListView1.SetObjects(DBiFace.GetOnlineTable());
            var set = new Forms.Settings();
            set.ShowDialog();
        }

        

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBiFace.CloseDB();
        }

        private void rescanDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DBiFace.InitDB(Properties.Settings.Default.DBAggressiveScan);
        }
    }
}
