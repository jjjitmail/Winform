using CommissioningManager.Controls;
using System.Windows.Forms;

namespace CommissioningManager
{
    public partial class DataControl : UserControl
    {
        public DataControl()
        {
            InitializeComponent();
        }

        public DataGridView DataGridView { get { return this.dataGridView; } }

        public CheckedListBoxEx CheckedListBox { get { return this.checkedListBoxEx1; } }

        public RichTextBox ResultTextBox { get { return this.textBoxResult; } }

        public Button ValidateButton { get { return this.btnValidate; } }

        public Button ExportButton { get { return this.BtnExport; } }
    }
}