namespace WAP3
{
    partial class formTaskDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.taskConfigBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.localdbDataSet = new WAP3.localdbDataSet();
            this.taskConfigTableAdapter = new WAP3.localdbDataSetTableAdapters.TaskConfigTableAdapter();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.taskConfigBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.localdbDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // taskConfigBindingSource
            // 
            this.taskConfigBindingSource.DataMember = "TaskConfig";
            this.taskConfigBindingSource.DataSource = this.localdbDataSet;
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.dataGrid1.BackgroundColor = System.Drawing.SystemColors.Highlight;
            this.dataGrid1.DataSource = this.taskConfigBindingSource;
            this.dataGrid1.Location = new System.Drawing.Point(0, 54);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.Size = new System.Drawing.Size(240, 211);
            this.dataGrid1.TabIndex = 0;
            // 
            // localdbDataSet
            // 
            this.localdbDataSet.DataSetName = "localdbDataSet";
            this.localdbDataSet.Prefix = "";
            this.localdbDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // taskConfigTableAdapter
            // 
            this.taskConfigTableAdapter.ClearBeforeFill = true;
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Back";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // formTaskDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.dataGrid1);
            this.Menu = this.mainMenu1;
            this.Name = "formTaskDetails";
            this.Text = "Task Details";
            this.Load += new System.EventHandler(this.formTaskDetails_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskConfigBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.localdbDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private WAP3.localdbDataSet localdbDataSet;
        private System.Windows.Forms.BindingSource taskConfigBindingSource;
        private WAP3.localdbDataSetTableAdapters.TaskConfigTableAdapter taskConfigTableAdapter;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}