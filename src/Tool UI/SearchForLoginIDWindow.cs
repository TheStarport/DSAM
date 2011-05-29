using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DAM.Tool_UI
{
    public partial class SearchForAccountsByLoginIDWindow : Form
    {
        AppServiceInterface appServices;
        DataAccess dataAccess = null;

        public SearchForAccountsByLoginIDWindow(AppServiceInterface appServices, DataAccess dataAccess)
        {
            this.appServices = appServices;
            this.dataAccess = dataAccess;
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            label2.Text = "Matching Accounts - Searching";

            string[] ids = new string[1];
            ids[0] = FLUtility.EscapeLikeExpressionString(textBox1.Text);
            dataAccess.GetLoginIDListByLoginID(damDataSet.LoginIDList, ids);
            label2.Text = "Matching Accounts";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonShowAccount_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string accDir = (string)dataGridView1.SelectedCells[0].OwningRow.Cells[accDirDataGridViewTextBoxColumn.Index].Value;
                appServices.FilterOnAccDir(accDir);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                string accDir = (string)dataGridView1.SelectedCells[0].OwningRow.Cells[accDirDataGridViewTextBoxColumn.Index].Value;
                appServices.FilterOnAccDir(accDir);
            }
        }
    }
}
