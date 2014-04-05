using System;
using System.Globalization;
using System.Windows.Forms;
using BrightIdeasSoftware;
using DSAccountManager_v2.GD;
using FLAccountDB;
using FLAccountDB.NoSQL;

namespace DSAccountManager_v2
{
    public partial class MainForm : Form
    {
        private Universe _uni;
        public MainForm()
        {
            InitializeComponent();
            _uni = new Universe(@"g:\Games\freelancer\fl-Disc487\dev");
            equipmentBindingSource.DataSource = _uni.Gis.Equipment;
            systemsBindingSource.DataSource = _uni.Gis.Systems;
            fastObjectListView1.GetColumn("Base").AspectToStringConverter =
                (value) =>
                {
                    if ((string) value == "") return "";
                    return _uni.Gis.Bases.FindByNickname((string) value).IDString;
                };

            fastObjectListView1.GetColumn("Ship").AspectToStringConverter =
                (value) =>
                {
                    if (value == null) return "";
                    return _uni.Gis.Ships.FindByHash((uint)value).Name;
                };

            DBiFace.DBPercentChanged += DBiFace_DBPercentChanged;
            DBiFace.DBStateChanged += DBiFace_DBStateChanged;
            DBiFace.OnReadyRequest += DBiFace_OnReadyRequest;
        }

        void DBiFace_OnReadyRequest(System.Collections.Generic.List<Metadata> meta)
        {
            fastObjectListView1.SetObjects(meta);
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
                DBiFace.GetOnlineTable();
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

        private void button1_Click_1(object sender, EventArgs e)
        {

            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            700);

            if (radioCharname.Checked)
                DBiFace.AccDB.GetMetasByName(textBox1.Text);

            if (radioAccID.Checked)
                DBiFace.AccDB.GetAccountChars(textBox1.Text);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboSearchItem.DisplayMember = "Name";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboSearchItem.DisplayMember = "Nickname";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            1200);
            DBiFace.AccDB.GetMetasByItem((uint)comboSearchItem.SelectedValue);
        }

        private void fastObjectListView1_SelectionChanged(object sender, EventArgs e)
        {
            FillPlayerData((Metadata)fastObjectListView1.SelectedObject);
        }


        private void FillPlayerData(Metadata md)
        {
            var player = md.GetCharacter(Properties.Settings.Default.FLDBPath);
            textBoxName.Text = player.Name;
            textBoxMoney.Text = player.Money.ToString();
            comboBoxSystem.SelectedValue = player.System.ToLowerInvariant();
            dateLastOnline.MaxDate = DateTime.Now;
            dateLastOnline.Value = player.LastOnline;
        }

    }
}
