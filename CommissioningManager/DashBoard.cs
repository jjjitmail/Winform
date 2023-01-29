using CommissioningManager.Data;
using CommissioningManager.Helpers;
using CommissioningManager.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using CommissioningManager.Controllers;
using CommissioningManager.Interfaces;

namespace CommissioningManager
{
    public partial class DashBoard : Form
    {
        #region private variables

        private IModel<LuxDataModel> _luxDataModel;
        private IModel<ScannerDataModel> _scannerDataModel;
        private IModel<TeleControllerDataModel> _teleControllerDataModel;

        public DataControl dataControlTeleController = new DataControl();
        public ProgressBar Progressbar = new ProgressBar();
        #endregion

        public DashBoard(IModel<LuxDataModel> ILuxDataModel, 
                         IModel<ScannerDataModel> IScannerDataModel,
                         IModel<TeleControllerDataModel> ITeleControllerDataModel)
        {
            this._luxDataModel = ILuxDataModel;
            this._scannerDataModel = IScannerDataModel;
            this._teleControllerDataModel = ITeleControllerDataModel;

            InitializeComponent();
            Progressbar = progressBar1;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DashBoard_Shown(object sender, EventArgs e)
        {
            Start();

            var DatabaseConnectionlist = Utls<BindingList<DatabaseModel>>.LoadFromXML(Environment.CurrentDirectory + @"\Files\DatabaseModel.xml");            
            comboBoxDatabaseName.DisplayMember = "DatabaseName";
            comboBoxDatabaseName.ValueMember = "DatabaseConnection";
            comboBoxDatabaseName.DataSource = DatabaseConnectionlist;
            textBoxDatabaseConnection.Text = comboBoxDatabaseName.SelectedValue.ToString();            
        }

        private void Start()
        {
            ReadLuxDataFiles();
            ReadScannerFiles();
            ReadTeleControllerFiles();

            MouseEvents();
        }
        
        private void MouseEvents()
        {
            _luxDataModel.RegisterEvents(this, ReadLuxDataFiles);
            _scannerDataModel.RegisterEvents(this, ReadScannerFiles);
            _teleControllerDataModel.RegisterEvents(this, ReadTeleControllerFiles);
        }

        #region read files
        private void ReadLuxDataFiles(bool preFillQuery = false)
        {
            _luxDataModel = ModelController<LuxDataModel>
                .LoadModel<IModel<LuxDataModel>>(dataGridViewLuxData, preFillQuery);
        }

        private void ReadScannerFiles(bool preFillQuery = false)
        {
            _scannerDataModel = ModelController<ScannerDataModel>
                .LoadModel<IModel<ScannerDataModel>>(dataControlScanData, preFillQuery);
        }

        private void ReadTeleControllerFiles(bool preFillQuery = false)
        {
            _teleControllerDataModel = ModelController<TeleControllerDataModel>
                .LoadModel<IModel<TeleControllerDataModel>>(dataControlTeleController, preFillQuery);
        }
        #endregion

        private void comboBoxDatabaseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxDatabaseConnection.Text = comboBoxDatabaseName.SelectedValue.ToString();
        }

        private void btnTeleController_Click(object sender, EventArgs e)
        {
            var text = textBoxTeleController.Text;
            
            if (text.Trim() == "")
            {
                ShowTeleController();
            }
        }

        private void ShowTeleController()
        {
            if (tabControl.Controls.Count > 2)
                tabControl.Controls.RemoveAt(2);
            
            TabPage _TeleControllerTabPage = new TabPage();
            _TeleControllerTabPage.Text = "TeleControllerData";
            _TeleControllerTabPage.Controls.Add(dataControlTeleController);
            tabControl.Controls.Add(_TeleControllerTabPage);
        }
    }
}