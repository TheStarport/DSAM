using System;
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

            if (!DBiFace.IsDBAvailable()) return;

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
            //TODO: update the main grid
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

        private void button1_Click(object sender, EventArgs e)
        {
// ReSharper disable once ObjectCreationAsStatement
            var v = new WaitWindow.Window(this,
                handler => DBiFace.DBRenew += handler,
                handler => DBiFace.DBRenew += handler,
                5000);
        }
    }
}
