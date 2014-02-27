using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DAM
{
    partial class SearchItemTool : Form
    {
        private BackgroundWorker _bgWorker;
        private MainWindow _mainWindow;

        
        public SearchItemTool(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            if (_bgWorker == null)
            {
                _bgWorker = new BackgroundWorker { WorkerSupportsCancellation = true, WorkerReportsProgress = true};
                _bgWorker.DoWork += _bgWorker_DoWork;
                _bgWorker.ProgressChanged += _bgWorker_ProgressChanged;
                _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
                _bgWorker.RunWorkerAsync();
            }
            else
            {
                if (_bgWorker.IsBusy)
                    _bgWorker.CancelAsync();
                else _bgWorker.RunWorkerAsync();
            }
        }

        void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = e.Result;
        }

        void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value++;
        }


        private void SetMaxBar(int max)
        {
            progressBar1.Maximum = max;
        }

        void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var rows = _mainWindow.charListDataGridView.Rows;

            Action<int> act = SetMaxBar;
            progressBar1.Invoke(act, rows.Count);

            var accDt = new DamDataSet.CharacterListDataTable();


            foreach (DataGridViewRow row in rows)
            {
                var obj = (DamDataSet.CharacterListRow)((DataRowView)row.DataBoundItem).Row;
                if (_bgWorker.CancellationPending)
                {
                    break;
                }

                if (!File.Exists(AppSettings.Default.setAccountDir + "\\" + row.Cells[8].Value)) continue;
                var loadedCharFile = new FLDataFile(AppSettings.Default.setAccountDir + "\\" + row.Cells[8].Value, true);

                bool isAdded = false;

                foreach (var set in loadedCharFile.GetSettings("Player", "cargo"))
                {
                    if (_mainWindow.gameData.GetItemDescByHash(set.UInt(0)) != textBox1.Text) continue;
                    accDt.ImportRow(obj);
                    isAdded = true;
                    break;
                }

                if (!isAdded)
                    foreach (FLDataFile.Setting set in loadedCharFile.GetSettings("Player", "base_equip"))
                    {
                        if (_mainWindow.gameData.GetItemDescByHash(set.UInt(0)) != textBox1.Text) continue;
                        accDt.ImportRow(obj);
                        break;
                    }

                _bgWorker.ReportProgress(0);

            }
            e.Result = accDt;
        }


        }
    }