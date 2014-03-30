namespace DSAccountManager_v2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rescanDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolDBQueue = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.preferencesToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // preferencesToolStripMenuItem
            // 
            this.preferencesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
            this.preferencesToolStripMenuItem.Name = "preferencesToolStripMenuItem";
            this.preferencesToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.preferencesToolStripMenuItem.Text = "Preferences";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rescanDBToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // rescanDBToolStripMenuItem
            // 
            this.rescanDBToolStripMenuItem.Name = "rescanDBToolStripMenuItem";
            this.rescanDBToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.rescanDBToolStripMenuItem.Text = "Rescan DB";
            this.rescanDBToolStripMenuItem.Click += new System.EventHandler(this.rescanDBToolStripMenuItem_Click);
            // 
            // fastObjectListView1
            // 
            this.fastObjectListView1.AllColumns.Add(this.olvColumn1);
            this.fastObjectListView1.AllColumns.Add(this.olvColumn2);
            this.fastObjectListView1.AllColumns.Add(this.olvColumn3);
            this.fastObjectListView1.AllColumns.Add(this.olvColumn4);
            this.fastObjectListView1.AllColumns.Add(this.olvColumn5);
            this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5});
            this.fastObjectListView1.Location = new System.Drawing.Point(0, 27);
            this.fastObjectListView1.Name = "fastObjectListView1";
            this.fastObjectListView1.ShowCommandMenuOnRightClick = true;
            this.fastObjectListView1.ShowFilterMenuOnRightClick = false;
            this.fastObjectListView1.ShowGroups = false;
            this.fastObjectListView1.Size = new System.Drawing.Size(462, 325);
            this.fastObjectListView1.TabIndex = 1;
            this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
            this.fastObjectListView1.View = System.Windows.Forms.View.Details;
            this.fastObjectListView1.VirtualMode = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Text = "Player Name";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Rank";
            this.olvColumn2.CellPadding = null;
            this.olvColumn2.Text = "Rank";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Money";
            this.olvColumn3.CellPadding = null;
            this.olvColumn3.Text = "Money";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Base";
            this.olvColumn4.CellPadding = null;
            this.olvColumn4.Text = "Base";
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "LastOnline";
            this.olvColumn5.AspectToStringFormat = "{0:d}";
            this.olvColumn5.CellPadding = null;
            this.olvColumn5.Text = "Last Online";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStatus,
            this.toolProgress,
            this.toolDBQueue});
            this.statusStrip1.Location = new System.Drawing.Point(0, 353);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(834, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStatus
            // 
            this.toolStatus.Name = "toolStatus";
            this.toolStatus.Size = new System.Drawing.Size(57, 17);
            this.toolStatus.Text = "DB Ready";
            // 
            // toolProgress
            // 
            this.toolProgress.Name = "toolProgress";
            this.toolProgress.Size = new System.Drawing.Size(100, 16);
            // 
            // toolDBQueue
            // 
            this.toolDBQueue.Name = "toolDBQueue";
            this.toolDBQueue.Size = new System.Drawing.Size(13, 17);
            this.toolDBQueue.Text = "0";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 375);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.fastObjectListView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "DSAM 2.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private BrightIdeasSoftware.FastObjectListView fastObjectListView1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.ToolStripMenuItem rescanDBToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStatus;
        private System.Windows.Forms.ToolStripProgressBar toolProgress;
        private System.Windows.Forms.ToolStripStatusLabel toolDBQueue;
    }
}

