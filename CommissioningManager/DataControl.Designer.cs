namespace CommissioningManager
{
    partial class DataControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.listBoxFileList = new System.Windows.Forms.CheckedListBox();
            this.btnValidate = new System.Windows.Forms.Button();
            this.textBoxResult = new System.Windows.Forms.RichTextBox();
            this.BtnExport = new System.Windows.Forms.Button();
            this.checkedListBoxEx1 = new CommissioningManager.Controls.CheckedListBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(3, 81);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(1001, 409);
            this.dataGridView.TabIndex = 0;
            // 
            // listBoxFileList
            // 
            this.listBoxFileList.FormattingEnabled = true;
            this.listBoxFileList.HorizontalScrollbar = true;
            this.listBoxFileList.Location = new System.Drawing.Point(1011, 302);
            this.listBoxFileList.Name = "listBoxFileList";
            this.listBoxFileList.ScrollAlwaysVisible = true;
            this.listBoxFileList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxFileList.Size = new System.Drawing.Size(249, 169);
            this.listBoxFileList.TabIndex = 1;
            this.listBoxFileList.Visible = false;
            // 
            // btnValidate
            // 
            this.btnValidate.Location = new System.Drawing.Point(1010, 6);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(125, 69);
            this.btnValidate.TabIndex = 3;
            this.btnValidate.Text = "Validate";
            this.btnValidate.UseVisualStyleBackColor = true;
            // 
            // textBoxResult
            // 
            this.textBoxResult.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxResult.Location = new System.Drawing.Point(4, 6);
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ReadOnly = true;
            this.textBoxResult.Size = new System.Drawing.Size(1000, 69);
            this.textBoxResult.TabIndex = 4;
            this.textBoxResult.Text = "";
            // 
            // BtnExport
            // 
            this.BtnExport.Location = new System.Drawing.Point(1141, 6);
            this.BtnExport.Name = "BtnExport";
            this.BtnExport.Size = new System.Drawing.Size(118, 69);
            this.BtnExport.TabIndex = 5;
            this.BtnExport.Text = "Export";
            this.BtnExport.UseVisualStyleBackColor = true;
            // 
            // checkedListBoxEx1
            // 
            this.checkedListBoxEx1.FormattingEnabled = true;
            this.checkedListBoxEx1.HorizontalScrollbar = true;
            this.checkedListBoxEx1.Location = new System.Drawing.Point(1011, 82);
            this.checkedListBoxEx1.Name = "checkedListBoxEx1";
            this.checkedListBoxEx1.ScrollAlwaysVisible = true;
            this.checkedListBoxEx1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.checkedListBoxEx1.Size = new System.Drawing.Size(248, 409);
            this.checkedListBoxEx1.TabIndex = 1;
            // 
            // DataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.checkedListBoxEx1);
            this.Controls.Add(this.BtnExport);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.listBoxFileList);
            this.Controls.Add(this.dataGridView);
            this.Name = "DataControl";
            this.Size = new System.Drawing.Size(1265, 494);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.CheckedListBox listBoxFileList;
        private System.Windows.Forms.Button btnValidate;
        private System.Windows.Forms.RichTextBox textBoxResult;
        private System.Windows.Forms.Button BtnExport;
        private Controls.CheckedListBoxEx checkedListBoxEx1;
    }
}
