namespace CommissioningManager
{
    partial class DashBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashBoard));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.LuxDataPage = new System.Windows.Forms.TabPage();
            this.dataGridViewLuxData = new CommissioningManager.DataControl();
            this.ScanDataPage = new System.Windows.Forms.TabPage();
            this.dataControlScanData = new CommissioningManager.DataControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.textBoxDatabaseConnection = new System.Windows.Forms.TextBox();
            this.comboBoxDatabaseName = new System.Windows.Forms.ComboBox();
            this.checkBoxDeleteFiles = new System.Windows.Forms.CheckBox();
            this.textBoxTeleController = new System.Windows.Forms.TextBox();
            this.btnTeleController = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.LuxDataPage.SuspendLayout();
            this.ScanDataPage.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.LuxDataPage);
            this.tabControl.Controls.Add(this.ScanDataPage);
            this.tabControl.Location = new System.Drawing.Point(3, 78);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1276, 550);
            this.tabControl.TabIndex = 0;
            // 
            // LuxDataPage
            // 
            this.LuxDataPage.Controls.Add(this.dataGridViewLuxData);
            this.LuxDataPage.Location = new System.Drawing.Point(4, 22);
            this.LuxDataPage.Name = "LuxDataPage";
            this.LuxDataPage.Padding = new System.Windows.Forms.Padding(3);
            this.LuxDataPage.Size = new System.Drawing.Size(1268, 524);
            this.LuxDataPage.TabIndex = 0;
            this.LuxDataPage.Text = "LuxData";
            this.LuxDataPage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewLuxData
            // 
            this.dataGridViewLuxData.AutoScroll = true;
            this.dataGridViewLuxData.AutoSize = true;
            this.dataGridViewLuxData.Location = new System.Drawing.Point(3, -1);
            this.dataGridViewLuxData.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewLuxData.Name = "dataGridViewLuxData";
            this.dataGridViewLuxData.Size = new System.Drawing.Size(1660, 607);
            this.dataGridViewLuxData.TabIndex = 2;
            // 
            // ScanDataPage
            // 
            this.ScanDataPage.Controls.Add(this.dataControlScanData);
            this.ScanDataPage.Location = new System.Drawing.Point(4, 22);
            this.ScanDataPage.Name = "ScanDataPage";
            this.ScanDataPage.Padding = new System.Windows.Forms.Padding(3);
            this.ScanDataPage.Size = new System.Drawing.Size(1268, 524);
            this.ScanDataPage.TabIndex = 1;
            this.ScanDataPage.Text = "ScanData";
            this.ScanDataPage.UseVisualStyleBackColor = true;
            // 
            // dataControlScanData
            // 
            this.dataControlScanData.AutoScroll = true;
            this.dataControlScanData.AutoSize = true;
            this.dataControlScanData.Location = new System.Drawing.Point(3, -1);
            this.dataControlScanData.Margin = new System.Windows.Forms.Padding(4);
            this.dataControlScanData.Name = "dataControlScanData";
            this.dataControlScanData.Size = new System.Drawing.Size(1660, 607);
            this.dataControlScanData.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Location = new System.Drawing.Point(1019, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(256, 16);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1279, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Location = new System.Drawing.Point(0, 597);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1279, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // textBoxDatabaseConnection
            // 
            this.textBoxDatabaseConnection.Location = new System.Drawing.Point(3, 27);
            this.textBoxDatabaseConnection.Multiline = true;
            this.textBoxDatabaseConnection.Name = "textBoxDatabaseConnection";
            this.textBoxDatabaseConnection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDatabaseConnection.Size = new System.Drawing.Size(1012, 46);
            this.textBoxDatabaseConnection.TabIndex = 3;
            // 
            // comboBoxDatabaseName
            // 
            this.comboBoxDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDatabaseName.FormattingEnabled = true;
            this.comboBoxDatabaseName.Location = new System.Drawing.Point(1021, 27);
            this.comboBoxDatabaseName.Name = "comboBoxDatabaseName";
            this.comboBoxDatabaseName.Size = new System.Drawing.Size(119, 21);
            this.comboBoxDatabaseName.TabIndex = 4;
            this.comboBoxDatabaseName.SelectedIndexChanged += new System.EventHandler(this.comboBoxDatabaseName_SelectedIndexChanged);
            // 
            // checkBoxDeleteFiles
            // 
            this.checkBoxDeleteFiles.AutoSize = true;
            this.checkBoxDeleteFiles.Location = new System.Drawing.Point(1146, 31);
            this.checkBoxDeleteFiles.Name = "checkBoxDeleteFiles";
            this.checkBoxDeleteFiles.Size = new System.Drawing.Size(134, 17);
            this.checkBoxDeleteFiles.TabIndex = 3;
            this.checkBoxDeleteFiles.Text = "Delete files after export";
            this.checkBoxDeleteFiles.UseVisualStyleBackColor = true;
            // 
            // textBoxTeleController
            // 
            this.textBoxTeleController.Location = new System.Drawing.Point(1022, 52);
            this.textBoxTeleController.Name = "textBoxTeleController";
            this.textBoxTeleController.Size = new System.Drawing.Size(118, 20);
            this.textBoxTeleController.TabIndex = 5;
            // 
            // btnTeleController
            // 
            this.btnTeleController.Location = new System.Drawing.Point(1147, 49);
            this.btnTeleController.Name = "btnTeleController";
            this.btnTeleController.Size = new System.Drawing.Size(120, 23);
            this.btnTeleController.TabIndex = 6;
            this.btnTeleController.Text = "TeleController";
            this.btnTeleController.UseVisualStyleBackColor = true;
            this.btnTeleController.Click += new System.EventHandler(this.btnTeleController_Click);
            // 
            // DashBoard
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1279, 619);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnTeleController);
            this.Controls.Add(this.textBoxTeleController);
            this.Controls.Add(this.checkBoxDeleteFiles);
            this.Controls.Add(this.comboBoxDatabaseName);
            this.Controls.Add(this.textBoxDatabaseConnection);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "DashBoard";
            this.Text = "Commissioning Manager 2017";
            this.Shown += new System.EventHandler(this.DashBoard_Shown);
            this.tabControl.ResumeLayout(false);
            this.LuxDataPage.ResumeLayout(false);
            this.LuxDataPage.PerformLayout();
            this.ScanDataPage.ResumeLayout(false);
            this.ScanDataPage.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage LuxDataPage;
        private System.Windows.Forms.TabPage ScanDataPage;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private DataControl dataControlScanData;
        private DataControl dataGridViewLuxData;
        private System.Windows.Forms.ComboBox comboBoxDatabaseName;
        public System.Windows.Forms.TextBox textBoxDatabaseConnection;
        public System.Windows.Forms.CheckBox checkBoxDeleteFiles;
        private System.Windows.Forms.TextBox textBoxTeleController;
        private System.Windows.Forms.Button btnTeleController;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

