﻿namespace DAM
{
    partial class PropertiesWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertiesWindow));
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.UninterestedCharsDirButton = new System.Windows.Forms.Button();
            this.textBoxMoveUninterestedCharsDir = new System.Windows.Forms.TextBox();
            this.checkBoxMoveUninterestedChars = new System.Windows.Forms.CheckBox();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBoxCheckDefaultLights = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckDefaultEngine = new System.Windows.Forms.CheckBox();
            this.checkBoxCheckDefaultPowerPlant = new System.Windows.Forms.CheckBox();
            this.checkBoxReportVisitError = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticFixCharFiles = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxAutomaticCharClean = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticCharWipe = new System.Windows.Forms.CheckBox();
            this.checkBoxChangedOnly = new System.Windows.Forms.CheckBox();
            this.flDirButton = new System.Windows.Forms.Button();
            this.textBoxFLDir = new System.Windows.Forms.TextBox();
            this.checkBoxWriteEncryptedFiles = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ionCrossDirButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxIoncrossDir = new System.Windows.Forms.TextBox();
            this.textBoxAccDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.accountDirButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxStatsFactions = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBoxStatisticsDir = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUnicode = new System.Windows.Forms.CheckBox();
            this.textBoxFLHookPort = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxFLHookLogin = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.numericUpDownHistoryHorizon = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFLHookPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoryHorizon)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(495, 425);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(414, 425);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(558, 407);
            this.tabControl1.TabIndex = 17;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(550, 381);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.UninterestedCharsDirButton);
            this.groupBox2.Controls.Add(this.textBoxMoveUninterestedCharsDir);
            this.groupBox2.Controls.Add(this.checkBoxMoveUninterestedChars);
            this.groupBox2.Controls.Add(this.numericUpDown3);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.checkBoxCheckDefaultLights);
            this.groupBox2.Controls.Add(this.checkBoxCheckDefaultEngine);
            this.groupBox2.Controls.Add(this.checkBoxCheckDefaultPowerPlant);
            this.groupBox2.Controls.Add(this.checkBoxReportVisitError);
            this.groupBox2.Controls.Add(this.checkBoxAutomaticFixCharFiles);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.numericUpDown1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.checkBoxAutomaticCharClean);
            this.groupBox2.Controls.Add(this.checkBoxAutomaticCharWipe);
            this.groupBox2.Controls.Add(this.checkBoxChangedOnly);
            this.groupBox2.Controls.Add(this.flDirButton);
            this.groupBox2.Controls.Add(this.textBoxFLDir);
            this.groupBox2.Controls.Add(this.checkBoxWriteEncryptedFiles);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ionCrossDirButton);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBoxIoncrossDir);
            this.groupBox2.Controls.Add(this.textBoxAccDir);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.accountDirButton);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(537, 311);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Files";
            // 
            // UninterestedCharsDirButton
            // 
            this.UninterestedCharsDirButton.Location = new System.Drawing.Point(502, 182);
            this.UninterestedCharsDirButton.Name = "UninterestedCharsDirButton";
            this.UninterestedCharsDirButton.Size = new System.Drawing.Size(29, 23);
            this.UninterestedCharsDirButton.TabIndex = 39;
            this.UninterestedCharsDirButton.Text = "...";
            this.UninterestedCharsDirButton.UseVisualStyleBackColor = true;
            this.UninterestedCharsDirButton.Click += new System.EventHandler(this.UninterestedCharsDirButton_Click);
            // 
            // textBoxMoveUninterestedCharsDir
            // 
            this.textBoxMoveUninterestedCharsDir.Location = new System.Drawing.Point(221, 185);
            this.textBoxMoveUninterestedCharsDir.Name = "textBoxMoveUninterestedCharsDir";
            this.textBoxMoveUninterestedCharsDir.Size = new System.Drawing.Size(275, 20);
            this.textBoxMoveUninterestedCharsDir.TabIndex = 38;
            // 
            // checkBoxMoveUninterestedChars
            // 
            this.checkBoxMoveUninterestedChars.AutoSize = true;
            this.checkBoxMoveUninterestedChars.Location = new System.Drawing.Point(9, 188);
            this.checkBoxMoveUninterestedChars.Name = "checkBoxMoveUninterestedChars";
            this.checkBoxMoveUninterestedChars.Size = new System.Drawing.Size(206, 17);
            this.checkBoxMoveUninterestedChars.TabIndex = 37;
            this.checkBoxMoveUninterestedChars.Text = "Don\'t delete characters move them to:";
            this.checkBoxMoveUninterestedChars.UseVisualStyleBackColor = true;
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(413, 239);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown3.TabIndex = 36;
            this.numericUpDown3.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(467, 241);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "days";
            // 
            // checkBoxCheckDefaultLights
            // 
            this.checkBoxCheckDefaultLights.AutoSize = true;
            this.checkBoxCheckDefaultLights.Location = new System.Drawing.Point(392, 265);
            this.checkBoxCheckDefaultLights.Name = "checkBoxCheckDefaultLights";
            this.checkBoxCheckDefaultLights.Size = new System.Drawing.Size(134, 17);
            this.checkBoxCheckDefaultLights.TabIndex = 34;
            this.checkBoxCheckDefaultLights.Text = "Check for default lights";
            this.checkBoxCheckDefaultLights.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDefaultEngine
            // 
            this.checkBoxCheckDefaultEngine.AutoSize = true;
            this.checkBoxCheckDefaultEngine.Location = new System.Drawing.Point(186, 288);
            this.checkBoxCheckDefaultEngine.Name = "checkBoxCheckDefaultEngine";
            this.checkBoxCheckDefaultEngine.Size = new System.Drawing.Size(142, 17);
            this.checkBoxCheckDefaultEngine.TabIndex = 33;
            this.checkBoxCheckDefaultEngine.Text = "Check for default engine";
            this.checkBoxCheckDefaultEngine.UseVisualStyleBackColor = true;
            // 
            // checkBoxCheckDefaultPowerPlant
            // 
            this.checkBoxCheckDefaultPowerPlant.AutoSize = true;
            this.checkBoxCheckDefaultPowerPlant.Location = new System.Drawing.Point(186, 265);
            this.checkBoxCheckDefaultPowerPlant.Name = "checkBoxCheckDefaultPowerPlant";
            this.checkBoxCheckDefaultPowerPlant.Size = new System.Drawing.Size(165, 17);
            this.checkBoxCheckDefaultPowerPlant.TabIndex = 32;
            this.checkBoxCheckDefaultPowerPlant.Text = "Check for default power plant";
            this.checkBoxCheckDefaultPowerPlant.UseVisualStyleBackColor = true;
            // 
            // checkBoxReportVisitError
            // 
            this.checkBoxReportVisitError.AutoSize = true;
            this.checkBoxReportVisitError.Location = new System.Drawing.Point(9, 288);
            this.checkBoxReportVisitError.Name = "checkBoxReportVisitError";
            this.checkBoxReportVisitError.Size = new System.Drawing.Size(129, 17);
            this.checkBoxReportVisitError.TabIndex = 31;
            this.checkBoxReportVisitError.Text = "Report visit entry error";
            this.checkBoxReportVisitError.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutomaticFixCharFiles
            // 
            this.checkBoxAutomaticFixCharFiles.AutoSize = true;
            this.checkBoxAutomaticFixCharFiles.Location = new System.Drawing.Point(9, 265);
            this.checkBoxAutomaticFixCharFiles.Name = "checkBoxAutomaticFixCharFiles";
            this.checkBoxAutomaticFixCharFiles.Size = new System.Drawing.Size(170, 17);
            this.checkBoxAutomaticFixCharFiles.TabIndex = 30;
            this.checkBoxAutomaticFixCharFiles.Text = "Automatically fix character files";
            this.checkBoxAutomaticFixCharFiles.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(240, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(167, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "minutes time online and older than";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(230, 217);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 28;
            this.label8.Text = "days";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(186, 239);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown2.TabIndex = 27;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(179, 214);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown1.TabIndex = 26;
            this.numericUpDown1.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Delete character files inactive for";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(174, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Delete character files with less than";
            // 
            // checkBoxAutomaticCharClean
            // 
            this.checkBoxAutomaticCharClean.AutoSize = true;
            this.checkBoxAutomaticCharClean.Location = new System.Drawing.Point(289, 165);
            this.checkBoxAutomaticCharClean.Name = "checkBoxAutomaticCharClean";
            this.checkBoxAutomaticCharClean.Size = new System.Drawing.Size(186, 17);
            this.checkBoxAutomaticCharClean.TabIndex = 23;
            this.checkBoxAutomaticCharClean.Text = "Automatically clean character files";
            this.checkBoxAutomaticCharClean.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutomaticCharWipe
            // 
            this.checkBoxAutomaticCharWipe.AutoSize = true;
            this.checkBoxAutomaticCharWipe.Location = new System.Drawing.Point(9, 165);
            this.checkBoxAutomaticCharWipe.Name = "checkBoxAutomaticCharWipe";
            this.checkBoxAutomaticCharWipe.Size = new System.Drawing.Size(222, 17);
            this.checkBoxAutomaticCharWipe.TabIndex = 22;
            this.checkBoxAutomaticCharWipe.Text = "Automatically remove inactive characters ";
            this.checkBoxAutomaticCharWipe.UseVisualStyleBackColor = true;
            // 
            // checkBoxChangedOnly
            // 
            this.checkBoxChangedOnly.Checked = true;
            this.checkBoxChangedOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChangedOnly.Location = new System.Drawing.Point(9, 135);
            this.checkBoxChangedOnly.Name = "checkBoxChangedOnly";
            this.checkBoxChangedOnly.Size = new System.Drawing.Size(267, 24);
            this.checkBoxChangedOnly.TabIndex = 21;
            this.checkBoxChangedOnly.Text = "Check changed character files only";
            // 
            // flDirButton
            // 
            this.flDirButton.Location = new System.Drawing.Point(502, 107);
            this.flDirButton.Name = "flDirButton";
            this.flDirButton.Size = new System.Drawing.Size(29, 23);
            this.flDirButton.TabIndex = 14;
            this.flDirButton.Text = "...";
            this.flDirButton.UseVisualStyleBackColor = true;
            this.flDirButton.Click += new System.EventHandler(this.flDirButton_Click);
            // 
            // textBoxFLDir
            // 
            this.textBoxFLDir.Location = new System.Drawing.Point(9, 109);
            this.textBoxFLDir.Name = "textBoxFLDir";
            this.textBoxFLDir.Size = new System.Drawing.Size(487, 20);
            this.textBoxFLDir.TabIndex = 13;
            this.textBoxFLDir.Text = "C:\\Program Files\\Microsoft Games\\Freelancer";
            // 
            // checkBoxWriteEncryptedFiles
            // 
            this.checkBoxWriteEncryptedFiles.AutoSize = true;
            this.checkBoxWriteEncryptedFiles.Location = new System.Drawing.Point(289, 139);
            this.checkBoxWriteEncryptedFiles.Name = "checkBoxWriteEncryptedFiles";
            this.checkBoxWriteEncryptedFiles.Size = new System.Drawing.Size(122, 17);
            this.checkBoxWriteEncryptedFiles.TabIndex = 9;
            this.checkBoxWriteEncryptedFiles.Text = "Write encrypted files";
            this.checkBoxWriteEncryptedFiles.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Freelancer directory";
            // 
            // ionCrossDirButton
            // 
            this.ionCrossDirButton.Location = new System.Drawing.Point(502, 69);
            this.ionCrossDirButton.Name = "ionCrossDirButton";
            this.ionCrossDirButton.Size = new System.Drawing.Size(29, 23);
            this.ionCrossDirButton.TabIndex = 8;
            this.ionCrossDirButton.Text = "...";
            this.ionCrossDirButton.UseVisualStyleBackColor = true;
            this.ionCrossDirButton.Click += new System.EventHandler(this.ioncrossDirButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Ion Cross settings directory";
            // 
            // textBoxIoncrossDir
            // 
            this.textBoxIoncrossDir.Location = new System.Drawing.Point(9, 71);
            this.textBoxIoncrossDir.Name = "textBoxIoncrossDir";
            this.textBoxIoncrossDir.Size = new System.Drawing.Size(487, 20);
            this.textBoxIoncrossDir.TabIndex = 6;
            this.textBoxIoncrossDir.Text = "C:\\Program Files\\Microsoft Games\\Freelancer\\IONCROSS";
            // 
            // textBoxAccDir
            // 
            this.textBoxAccDir.Location = new System.Drawing.Point(9, 32);
            this.textBoxAccDir.Name = "textBoxAccDir";
            this.textBoxAccDir.Size = new System.Drawing.Size(487, 20);
            this.textBoxAccDir.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Player account directory";
            // 
            // accountDirButton
            // 
            this.accountDirButton.Location = new System.Drawing.Point(502, 30);
            this.accountDirButton.Name = "accountDirButton";
            this.accountDirButton.Size = new System.Drawing.Size(29, 23);
            this.accountDirButton.TabIndex = 0;
            this.accountDirButton.Text = "...";
            this.accountDirButton.UseVisualStyleBackColor = true;
            this.accountDirButton.Click += new System.EventHandler(this.accountDirButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(550, 381);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Statistics";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.numericUpDownHistoryHorizon);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.textBoxStatsFactions);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.textBoxStatisticsDir);
            this.groupBox3.Location = new System.Drawing.Point(7, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(537, 196);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "HTML Statistics Output";
            // 
            // textBoxStatsFactions
            // 
            this.textBoxStatsFactions.Location = new System.Drawing.Point(9, 72);
            this.textBoxStatsFactions.Multiline = true;
            this.textBoxStatsFactions.Name = "textBoxStatsFactions";
            this.textBoxStatsFactions.Size = new System.Drawing.Size(522, 90);
            this.textBoxStatsFactions.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 55);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(421, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "Generate activity statistics for the following factions (prefix or suffix seperat" +
                "ed by spaces)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 13);
            this.label11.TabIndex = 34;
            this.label11.Text = "Statistics directory";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(502, 30);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(29, 23);
            this.button3.TabIndex = 35;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBoxStatisticsDir
            // 
            this.textBoxStatisticsDir.Location = new System.Drawing.Point(9, 32);
            this.textBoxStatisticsDir.Name = "textBoxStatisticsDir";
            this.textBoxStatisticsDir.Size = new System.Drawing.Size(487, 20);
            this.textBoxStatisticsDir.TabIndex = 33;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxUnicode);
            this.groupBox1.Controls.Add(this.textBoxFLHookPort);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.textBoxFLHookLogin);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(5, 323);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 50);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FLHook";
            // 
            // checkBoxUnicode
            // 
            this.checkBoxUnicode.AutoSize = true;
            this.checkBoxUnicode.Location = new System.Drawing.Point(450, 19);
            this.checkBoxUnicode.Name = "checkBoxUnicode";
            this.checkBoxUnicode.Size = new System.Drawing.Size(87, 17);
            this.checkBoxUnicode.TabIndex = 16;
            this.checkBoxUnicode.Text = "Unicode port";
            this.checkBoxUnicode.UseVisualStyleBackColor = true;
            // 
            // textBoxFLHookPort
            // 
            this.textBoxFLHookPort.Location = new System.Drawing.Point(233, 18);
            this.textBoxFLHookPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.textBoxFLHookPort.Name = "textBoxFLHookPort";
            this.textBoxFLHookPort.Size = new System.Drawing.Size(48, 20);
            this.textBoxFLHookPort.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(287, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "(0 to disable flhook support)";
            // 
            // textBoxFLHookLogin
            // 
            this.textBoxFLHookLogin.Location = new System.Drawing.Point(45, 16);
            this.textBoxFLHookLogin.Name = "textBoxFLHookLogin";
            this.textBoxFLHookLogin.Size = new System.Drawing.Size(128, 20);
            this.textBoxFLHookLogin.TabIndex = 13;
            this.textBoxFLHookLogin.Text = "test";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Login";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Port";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 171);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 13);
            this.label14.TabIndex = 38;
            this.label14.Text = "History horizon";
            // 
            // numericUpDownHistoryHorizon
            // 
            this.numericUpDownHistoryHorizon.Location = new System.Drawing.Point(88, 169);
            this.numericUpDownHistoryHorizon.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownHistoryHorizon.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownHistoryHorizon.Name = "numericUpDownHistoryHorizon";
            this.numericUpDownHistoryHorizon.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownHistoryHorizon.TabIndex = 40;
            this.numericUpDownHistoryHorizon.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(142, 171);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(29, 13);
            this.label15.TabIndex = 41;
            this.label15.Text = "days";
            // 
            // PropertiesWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 452);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(590, 490);
            this.MinimumSize = new System.Drawing.Size(590, 430);
            this.Name = "PropertiesWindow";
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.PropertiesWindow_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFLHookPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHistoryHorizon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxAutomaticCharClean;
        private System.Windows.Forms.CheckBox checkBoxAutomaticCharWipe;
        private System.Windows.Forms.CheckBox checkBoxChangedOnly;
        private System.Windows.Forms.Button flDirButton;
        private System.Windows.Forms.TextBox textBoxFLDir;
        private System.Windows.Forms.CheckBox checkBoxWriteEncryptedFiles;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button ionCrossDirButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxIoncrossDir;
        private System.Windows.Forms.TextBox textBoxAccDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button accountDirButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxStatsFactions;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBoxStatisticsDir;
        private System.Windows.Forms.CheckBox checkBoxCheckDefaultEngine;
        private System.Windows.Forms.CheckBox checkBoxCheckDefaultPowerPlant;
        private System.Windows.Forms.CheckBox checkBoxReportVisitError;
        private System.Windows.Forms.CheckBox checkBoxAutomaticFixCharFiles;
        private System.Windows.Forms.CheckBox checkBoxCheckDefaultLights;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.TextBox textBoxMoveUninterestedCharsDir;
        private System.Windows.Forms.CheckBox checkBoxMoveUninterestedChars;
        private System.Windows.Forms.Button UninterestedCharsDirButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxUnicode;
        private System.Windows.Forms.NumericUpDown textBoxFLHookPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxFLHookLogin;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numericUpDownHistoryHorizon;

    }
}