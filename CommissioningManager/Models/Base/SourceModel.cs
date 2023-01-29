using CommissioningManager.Controls;
using CommissioningManager.Data;
using CommissioningManager.Enum;
using CommissioningManager.Filters;
using CommissioningManager.Helpers;
using CommissioningManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommissioningManager.Models.Base
{
    public abstract class SourceModel<T> : ISource where T : class
    {
        protected static Func<System.Enum, string> FileTypeValue => StringEnum.GetStringValue;
        protected DirectoryInfo dinfoInputs = new DirectoryInfo(Environment.CurrentDirectory + @"\Inputs\");
        protected DirectoryInfo dinfoProcessed = new DirectoryInfo(Environment.CurrentDirectory + @"\Processed\");
        protected DirectoryInfo dinfoFiles = new DirectoryInfo(Environment.CurrentDirectory + @"\Files\");

        public string FileModelPath = Environment.CurrentDirectory + @"\Files\FileModel.xml";

        CultureInfo EnglishCulture = new CultureInfo("en-EN");

        public SourceModel()
        {
            ResultList = new List<Result>();
            DataList = new SortableBindingList<T>();
            FileList = new BindingList<string>();
            FilesFilter = new FilesFilter();
            ProgressBar = new ProgressBar();
            PrepareEnvironment();
            InitCompanyCombo();            
        }

        public virtual FileType FileType { get; set; }

        public FilesFilter FilesFilter { get; set; }
        public SortableBindingList<T> DataList { get; set; }
        public BindingList<string> FileList { get; set; }
        public IEnumerable<T> DoubleDataList { get; set; }
        public IEnumerable<T> InvalidDoubleSameDataList { get; set; }
        public IEnumerable<T> DoubleSameDataList { get; set; }
        public List<Result> ResultList { get; set; }
        public DataControl DataControl { get; set; }
        public bool IsValid { get { return this.ResultList.All(x => string.IsNullOrEmpty(x.Error.Message)); } }
        public bool DeleteFiles { get; set; }
        public string DatabaseConnectionString { get; set; }
        public ComboBox HeaderComboBox { get; set; }
        public BindingList<CompanyModel> CompanyModellist { get; set; }
        public ProgressBar ProgressBar { get; set; }

        internal abstract Model InternalProcess<Model>() where Model : SourceModel<T>, new();

        protected Func<Model> Process<Model>(Func<FileInfo, Model> fileInfo) where Model : SourceModel<T>, new()
        {
            Model model = new Model();
            model.DataControl = this.DataControl;
            FillQuery();
            model.FilesFilter = this.FilesFilter;

            var pocess = Observable.Start(() =>
            {
                FileInfo[] files = dinfoInputs.GetFiles(FileTypeValue(model.FileType));
                foreach (var item in files)
                {
                    model.FileList.Add(item.Name);
                }

                if (model.FilesFilter.Query.Any())
                {
                    files = files.Where(x => model.FilesFilter.Query.Contains(x.Name)).ToArray();
                }

                if (model.FilesFilter.Query.Count == 0)
                {
                    files = new FileInfo[] { };
                }

                foreach (FileInfo file in files)
                {
                    Execute(model, () =>
                    {
                        foreach (var item in fileInfo(file).DataList)
                        {
                            model.DataList.Add(item);
                        }
                        return model.DataList.Count;
                    });
                }
            });
            pocess.Wait();
            InitUIBinding(model);            
            return () => model;
        }

        public async Task AsyncAction(Func<Task> func)
        {
            await Task.Run(()=> func());
        }

        protected async Task<Func<SourceModel<T>>> SaveAsync(DashBoard dashboard)
        {
            this.DataList = DataControl.DataGridView.DataSource as SortableBindingList<T>;
            this.ProgressBar = dashboard.Progressbar;
            this.ProgressBar.Value = 0;
            this.ProgressBar.Maximum = this.DataList.Count * 2;
            var model = ModelValidator<T>.Validate(this);
            this.ProgressBar = model.ProgressBar;

            if (!model.IsValid)
            {
                this.ResultList = model.ResultList;
                this.DoubleDataList = model.DoubleDataList;
                InitUIBinding(this);
                return () => this;
            }

            await AsyncAction(async () => 
            {
                Execute(this, () =>
                {
                    using (var context = new DataContext<T>(model.DatabaseConnectionString))
                    {
                        if (!context.Database.Exists())
                        {
                            throw new Exception("Database connection error");
                        }
                        foreach (var item in this.DataList)
                        {
                            context.Set(typeof(T)).Add(item);
                        }
                        return context.SaveChanges();
                    }
                });
            });

            if (model.IsValid)
            {
                this.DataList = new SortableBindingList<T>();                
                MoveFiles();
                this.ProgressBar.Value = this.ProgressBar.Maximum;
            }
            InitUIBinding(this);
            return () => this;
        }

        public void ResultBinding<Model>(Model model) where Model : SourceModel<T>
        {
            foreach (var item in model.ResultList)
            {
                model.DataControl.ResultTextBox.AppendAppText(item.Message + "\r\n");
            }
        }

        public void RegisterEvents(DashBoard dashboard, Action<bool> action)
        {
            DataControl.ValidateButton.Click += (object sender, EventArgs e) => ValidateButtonThis(dashboard);
            DataControl.DataGridView.CellContentClick += (object sender, DataGridViewCellEventArgs e) => DataGridView_CellContentClick(sender, e);
            DataControl.DataGridView.KeyDown += (object sender, KeyEventArgs e) => DataGridView_KeyDown(sender, e);
            DataControl.DataGridView.Sorted += (object sender, EventArgs e) => ControlBinder<T>.UpdateGridCells(this);
            DataControl.ExportButton.Click += (object sender, EventArgs e) => ExportButton_Click(dashboard);
            DataControl.CheckedListBox.MouseClick += (object sender, MouseEventArgs e) => { CheckedListBoxMouseClick(e, action); };
        }

        public void CheckedListBoxMouseClick(MouseEventArgs e, Action<bool> action)
        {
            int index = DataControl.CheckedListBox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                var isIsItemChecked = DataControl.CheckedListBox.GetItemChecked(index);
                DataControl.CheckedListBox.SetItemChecked(index, !isIsItemChecked);
                
                FillQuery();
                action?.Invoke(false);
            }
        }

        public void PreFillQuery(bool value = true)
        {
            for (int i = 0; i < DataControl.CheckedListBox.Items.Count; i++)
            {
                DataControl.CheckedListBox.SetItemChecked(i, value);
            }
            FillQuery();
        }

        #region private  

        private void InitCompanyCombo()
        {
            CompanyModellist = Utls<BindingList<CompanyModel>>.LoadFromXML(dinfoFiles + "CompanyModel.xml");
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            string[] columnsArray = { "OrganizationName", "ObjectOwnedBy", "Replacement" };
            // Company header handler
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn && e.RowIndex < 0 && (columnsArray.Contains(senderGrid.Columns[e.ColumnIndex].Name)))
            {
                HeaderComboBox = new ComboBox();
                if (senderGrid.Columns[e.ColumnIndex].Name == "OrganizationName")
                {
                    HeaderComboBox.Items.AddRange(CompanyModellist.Select(x => x.Name).ToArray());
                }
                DataControl.DataGridView.Controls.Add(HeaderComboBox);
                Rectangle oRectangle = DataControl.DataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                HeaderComboBox.Location = new Point(oRectangle.X, oRectangle.Y);
                HeaderComboBox.Size = new Size(oRectangle.Width, oRectangle.Height);
                HeaderComboBox.DropDownClosed += (object s, EventArgs ev) => HeaderComboBox.Visible = false;
                HeaderComboBox.SelectedIndexChanged += (object s, EventArgs ev) => CompanyComboBox_Action(e.ColumnIndex, senderGrid.DataSource);
                HeaderComboBox.LostFocus += (object s, EventArgs ev) => CompanyComboBox_Action(e.ColumnIndex, senderGrid.DataSource);
                HeaderComboBox.Visible = true;
                HeaderComboBox.Focus();
            }
            else if (senderGrid.Columns[e.ColumnIndex] is DataGridViewTextBoxColumn && e.RowIndex < 0)
            {
                DataControl.DataGridView.Rows[0].Cells[e.ColumnIndex].AccessibilityObject.DoDefaultAction();
                InitDataGridViewCells(x => x.Selected = x.ColumnIndex == e.ColumnIndex);
                DataControl.DataGridView.Rows[0].Cells[e.ColumnIndex].AccessibilityObject.DoDefaultAction();
            }
            else
            {
                if (HeaderComboBox != null)
                    HeaderComboBox.Visible = false;
            }
            this.DataList = senderGrid.DataSource as SortableBindingList<T>;
            ControlBinder<T>.UpdateGridCells(this);
        }

        private void CompanyComboBox_Action(int ColumnIndex, object dataSource)
        {
            var SelectedValue = HeaderComboBox.Text;
            InitDataGridViewCells(x => 
            {
                if (x.ColumnIndex == ColumnIndex)
                {
                    x.Value = SelectedValue;
                }
            });
            HeaderComboBox.Visible = false;

            this.DataList = dataSource as SortableBindingList<T>;
            ControlBinder<T>.UpdateGridCells(this);
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                InitDataGridViewCells(x =>
                {
                    if (x.Selected && !x.ReadOnly)
                        x.Value = "";
                });
            }
        }

        private void ExportButton_Click(DashBoard dashboard)
        {
            this.DatabaseConnectionString = dashboard.textBoxDatabaseConnection.Text;
            this.DeleteFiles = dashboard.checkBoxDeleteFiles.Checked;
            _ = SaveAsync(dashboard);
        }
        
        private void ValidateButtonThis(DashBoard dashboard)
        {
            this.DataList = DataControl.DataGridView.DataSource as SortableBindingList<T>;
            this.ProgressBar = dashboard.Progressbar;
            this.ProgressBar.Value = 0;
            this.ProgressBar.Maximum = this.DataList.Count * 2;
            var model = ModelValidator<T>.Validate(this);
            
            if (model.IsValid)
            {
                this.ResultList.Add(new Result { Message = DateTime.Now + ": Validation passed" });
            }
            else
            {
                this.ResultList = model.ResultList;
                this.DoubleDataList = model.DoubleDataList;
            }
            this.ProgressBar = model.ProgressBar;
            InitUIBinding(this);
        }

        private void Execute<Model>(Model model, Func<int> action) where Model : SourceModel<T>
        {
            this.ResultList = new List<Result>();
            try
            {
                var count = action();
                this.ResultList.Add(new Result { Message = DateTime.Now + ": " + count + " records processed successfully" });
            }
            catch (Exception err)
            {
                var EntityValidationErrors = err.GetType().GetProperty("EntityValidationErrors");
                if (EntityValidationErrors != null && EntityValidationErrors.GetValue(err, null) != null)
                {
                    var dbError = EntityValidationErrors.GetValue(err, null);
                    var Result = new Result { DbEntityValidationResults = (List<DbEntityValidationResult>)dbError };
                    var ValidationError = Result.DbEntityValidationResults.First().ValidationErrors.First();
                    Result.Message = DateTime.Now + ": (Error) PropertyName: " + ValidationError.PropertyName + ", ErrorMessage: " + ValidationError.ErrorMessage;

                    this.ResultList.Add(Result);
                }
                else
                {
                    this.ResultList.Add(new Result { Error = new Exception(DateTime.Now + ":  (Error) System: " + err.Message) });
                }
            }
        }

        private void PrepareEnvironment()
        {
            if (!Directory.Exists(dinfoInputs.FullName))
            {
                Directory.CreateDirectory(dinfoInputs.FullName);
            }
            if (!Directory.Exists(dinfoProcessed.FullName))
            {
                Directory.CreateDirectory(dinfoProcessed.FullName);
            }
        }

        private void MoveFiles()
        {
            var randomStringList = FilesFilter.Query.GetRandomString(3);

            List<FileModel> list = Utls<List<FileModel>>.LoadFromXML(FileModelPath);

            for (int i = 0; i < FilesFilter.Query.Count; i++)
            {
                string sourceFile = dinfoInputs.FullName + FilesFilter.Query[i].ToString();

                string desFile = dinfoProcessed.FullName + FilesFilter.Query[i].ToString();

                var filename = Path.GetFileNameWithoutExtension(desFile);
                desFile = dinfoProcessed.FullName + filename + "_" + randomStringList.Skip(i).First() + Path.GetExtension(desFile);

                if (this.DeleteFiles)
                {
                    File.Delete(sourceFile);
                    this.ResultList.Add(new Result { Message = DateTime.Now + ": '" + FilesFilter.Query[i] + "' deleted successfully" });
                }
                else
                {
                    File.Move(sourceFile, desFile);
                    this.ResultList.Add(new Result { Message = DateTime.Now + ": '" + FilesFilter.Query[i] + "' moved successfully" });
                }
                list.Add(new FileModel { Name = FilesFilter.Query[i] });
                this.FileList.Remove(FilesFilter.Query[i]);
            }

            Utls<List<FileModel>>.SaveToXML(list, FileModelPath);

            this.FilesFilter.Query = new List<string>();
            this.DataList = new SortableBindingList<T>();
            PreFillQuery(false);
        }

        private void InitUIBinding<Model>(Model model) where Model : SourceModel<T>
        {
            model.DataControl.CheckedListBox.DataSource = model.FileList;
            ControlBinder<T>.DataGridView(model);
            model.ResultList.Add(new Result { Message = DateTime.Now + ": " + model.DataList.Count + " records loaded successfully" });
            ResultBinding(model);
        }

        private void InitDataGridViewCells(Action<DataGridViewCell> action = null)
        {
            for (int i = 0; i < DataControl.DataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < DataControl.DataGridView.Rows[i].Cells.Count; j++)
                {
                    action?.Invoke(DataControl.DataGridView.Rows[i].Cells[j]);
                }
            }
        }

        private void FillQuery()
        {
            FilesFilter = new FilesFilter();
            foreach (var item in DataControl.CheckedListBox.CheckedItems)
            {
                FilesFilter.Query.Add(item.ToString());
            }
        }
        #endregion
    }
}