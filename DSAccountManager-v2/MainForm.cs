using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using DSAccountManager_v2.Forms;
using DSAccountManager_v2.GD;
using FLAccountDB;
using FLAccountDB.NoSQL;
using LogDispatcher;

namespace DSAccountManager_v2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            Universe.Parse(@"g:\Games\freelancer\fl-Disc487\dev");
            equipmentSearchBindingSource.DataSource = Universe.Gis.Equipment;
            systemsBindingSource.DataSource = Universe.Gis.Systems;
            systemsSearchBindingSource.DataSource = Universe.Gis.Systems;
            shipsBindingSource.DataSource = Universe.Gis.Ships;

            //ObjectListView.EditorRegistry.Register(typeof(float), typeof(NumericUpDown));

            ObjectListView.EditorRegistry.Register(typeof(float), delegate(Object model, OLVColumn column, Object value)
            {
                var nu = new NudFloat
                {
                    Maximum = 1, 
                    Minimum = -1, 
                    DecimalPlaces = 3, 
                    Increment = (decimal)0.1,
                    Value = (float)value
                };
                return nu;
            });




            fastObjectListView1.GetColumn("Base").AspectToStringConverter =
                value =>
                {
                    if ((string) value == "") return "";
                    return Universe.Gis.Bases.FindByNickname((string) value).Name;
                };

            olvRep.GetColumn("Faction").AspectToStringConverter =
                value =>
                {
                    if ((string)value == "") return "";
                    var tmp = Universe.Gis.Factions.FirstOrDefault(w => w.Nickname == (string) value);
                    if (tmp != null)
                        return tmp.FactionName;
                    return (string) value;
                };


            fastObjectListView1.GetColumn("Ship").AspectToStringConverter =
                value =>
                {
                    if (value == null) return "";
                    return Universe.Gis.Ships.FindByHash((uint)value).Name;
                };

            DBiFace.DBPercentChanged += DBiFace_DBPercentChanged;
            DBiFace.DBStateChanged += DBiFace_DBStateChanged;
            DBiFace.OnReadyRequest += DBiFace_OnReadyRequest;
        }

        void DBiFace_OnReadyRequest(List<Metadata> meta)
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
            var set = new Settings();
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

// ReSharper disable once UnusedVariable
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
// ReSharper disable once UnusedVariable
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

        private Character _curCharacter;
        private void FillPlayerData(Metadata md)
        {
            var player = md.GetCharacter(Properties.Settings.Default.FLDBPath);
            _curCharacter = player;
            textBoxName.Text = _curCharacter.Name;
            textBoxMoney.Text = _curCharacter.Money.ToString(CultureInfo.InvariantCulture);
            comboBoxSystem.SelectedValue = _curCharacter.System.ToLowerInvariant();
            dateLastOnline.MaxDate = DateTime.Now;
            dateLastOnline.Value = _curCharacter.LastOnline;
            olvRep.SetObjects(_curCharacter.Reputation.ToList());
            var eqList = AccountHelper.EquipTable.GetTable(_curCharacter);
            dlvEquipment.DataSource = eqList;

        }

        private void radioSearchSystem_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchSystem.Checked)
                systemsSearchBindingSource.DataSource = Universe.Gis.Systems;

        }

        private void radioSearchBase_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchBase.Checked)
                systemsSearchBindingSource.DataSource = Universe.Gis.Bases;
        }

        private void radioSearchLocName_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchLocName.Checked)
                comboSearchLocation.DisplayMember = "Name";
        }

        private void radioSearchLocNickname_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSearchLocNickname.Checked)
                comboSearchLocation.DisplayMember = "Nickname";
        }

        private void buttonSearchLocation_Click(object sender, EventArgs e)
        {
// ReSharper disable once UnusedVariable
            var v = new WaitWindow.Window(this,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            handler => DBiFace.AccDB.OnGetFinishWindow += handler,
            1200);

            if (radioSearchSystem.Checked)
                DBiFace.AccDB.GetMetasBySystem((string)comboSearchLocation.SelectedValue);
        }

        private void numericRep_ValueChanged(object sender, EventArgs e)
        {

            ((ReputationItem) olvRep.SelectedObject).Value = (float)numericRep.Value;
        }

        private void olvRep_SelectionChanged(object sender, EventArgs e)
        {
            if (olvRep.SelectedObject == null) return;
            numericRep.Value = (decimal)((ReputationItem)olvRep.SelectedObject).Value; //olvRep.SelectedObject+		
            trackBar2.Value = (int)(numericRep.Value * 100);
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            numericRep.Value = ((decimal)trackBar2.Value / 100);
            ((ReputationItem)olvRep.SelectedObject).Value = (float)numericRep.Value;
        }

    }
}
