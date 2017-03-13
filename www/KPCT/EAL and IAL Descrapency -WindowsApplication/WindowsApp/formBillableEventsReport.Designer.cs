namespace WindowsApp
{
    partial class formBillableEventsReport
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formBillableEventsReport));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.DetalsView = new ComponentFactory.Krypton.Docking.KryptonDockableNavigator();
            this.DetailsView = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.kryptonDataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Change = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChangeTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContainerNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Line = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrightKind = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Facility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReportView = new ComponentFactory.Krypton.Navigator.KryptonPage();
            this.rptBillableEventsReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.BillableEventsReport1 = new WindowsApp.BillableEventsReport();
            this.lblToTime = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.ToTimeDateTimePicker = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.lblFromTime = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.FromTimeDateTimePicker = new ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker();
            this.ComboBoxTypeofChange = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnSubmit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblTypeOfChange = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetalsView)).BeginInit();
            this.DetalsView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetailsView)).BeginInit();
            this.DetailsView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportView)).BeginInit();
            this.ReportView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxTypeofChange)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.kryptonPanel1);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel.TabIndex = 0;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.DetalsView);
            this.kryptonPanel1.Controls.Add(this.lblToTime);
            this.kryptonPanel1.Controls.Add(this.ToTimeDateTimePicker);
            this.kryptonPanel1.Controls.Add(this.lblFromTime);
            this.kryptonPanel1.Controls.Add(this.FromTimeDateTimePicker);
            this.kryptonPanel1.Controls.Add(this.ComboBoxTypeofChange);
            this.kryptonPanel1.Controls.Add(this.btnSubmit);
            this.kryptonPanel1.Controls.Add(this.lblTypeOfChange);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel1.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel1.TabIndex = 1;
            // 
            // DetalsView
            // 
            this.DetalsView.Location = new System.Drawing.Point(3, 44);
            this.DetalsView.Name = "DetalsView";
            this.DetalsView.Pages.AddRange(new ComponentFactory.Krypton.Navigator.KryptonPage[] {
            this.DetailsView,
            this.ReportView});
            this.DetalsView.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.DetalsView.SelectedIndex = 1;
            this.DetalsView.Size = new System.Drawing.Size(839, 560);
            this.DetalsView.TabIndex = 32;
            this.DetalsView.Text = "View";
            // 
            // DetailsView
            // 
            this.DetailsView.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.DetailsView.Controls.Add(this.kryptonDataGridView1);
            this.DetailsView.Flags = 65534;
            this.DetailsView.LastVisibleSet = true;
            this.DetailsView.MinimumSize = new System.Drawing.Size(50, 50);
            this.DetailsView.Name = "DetailsView";
            this.DetailsView.Size = new System.Drawing.Size(837, 535);
            this.DetailsView.Text = "Data View";
            this.DetailsView.ToolTipTitle = "Page ToolTip";
            this.DetailsView.UniqueName = "730CAE15A1994AF303BBE4A40D388D90";
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.AllowUserToAddRows = false;
            this.kryptonDataGridView1.AllowUserToDeleteRows = false;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Change,
            this.ChangeTime,
            this.ContainerNo,
            this.FromValue,
            this.ToValue,
            this.Line,
            this.FrightKind,
            this.Category,
            this.Facility,
            this.Wt,
            this.Note});
            this.kryptonDataGridView1.Location = new System.Drawing.Point(3, 2);
            this.kryptonDataGridView1.MultiSelect = false;
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonDataGridView1.ReadOnly = true;
            this.kryptonDataGridView1.Size = new System.Drawing.Size(822, 502);
            this.kryptonDataGridView1.TabIndex = 32;
            // 
            // Change
            // 
            this.Change.DataPropertyName = "CHANGE";
            this.Change.HeaderText = "Change";
            this.Change.Name = "Change";
            this.Change.ReadOnly = true;
            // 
            // ChangeTime
            // 
            this.ChangeTime.DataPropertyName = "CHANGETIME";
            this.ChangeTime.HeaderText = "ChangeTime";
            this.ChangeTime.Name = "ChangeTime";
            this.ChangeTime.ReadOnly = true;
            // 
            // ContainerNo
            // 
            this.ContainerNo.DataPropertyName = "CONTAINERNO";
            this.ContainerNo.HeaderText = "ContainerNo";
            this.ContainerNo.Name = "ContainerNo";
            this.ContainerNo.ReadOnly = true;
            // 
            // FromValue
            // 
            this.FromValue.DataPropertyName = "FROMVALUE";
            this.FromValue.HeaderText = "FromValue";
            this.FromValue.Name = "FromValue";
            this.FromValue.ReadOnly = true;
            // 
            // ToValue
            // 
            this.ToValue.DataPropertyName = "TOVALUE";
            this.ToValue.HeaderText = "ToValue";
            this.ToValue.Name = "ToValue";
            this.ToValue.ReadOnly = true;
            // 
            // Line
            // 
            this.Line.DataPropertyName = "Line";
            this.Line.HeaderText = "Line";
            this.Line.Name = "Line";
            this.Line.ReadOnly = true;
            // 
            // FrightKind
            // 
            this.FrightKind.DataPropertyName = "Freight";
            this.FrightKind.HeaderText = "Freight Kind";
            this.FrightKind.Name = "FrightKind";
            this.FrightKind.ReadOnly = true;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CATEGORY";
            this.Category.HeaderText = "Category";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // Facility
            // 
            this.Facility.DataPropertyName = "FACILITY";
            this.Facility.HeaderText = "Facility";
            this.Facility.Name = "Facility";
            this.Facility.ReadOnly = true;
            // 
            // Wt
            // 
            this.Wt.DataPropertyName = "Wt";
            this.Wt.HeaderText = "Wt (Kg)";
            this.Wt.Name = "Wt";
            this.Wt.ReadOnly = true;
            // 
            // Note
            // 
            this.Note.DataPropertyName = "NOTE";
            this.Note.HeaderText = "Note";
            this.Note.Name = "Note";
            this.Note.ReadOnly = true;
            // 
            // ReportView
            // 
            this.ReportView.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.ReportView.Controls.Add(this.rptBillableEventsReport);
            this.ReportView.Flags = 65534;
            this.ReportView.LastVisibleSet = true;
            this.ReportView.MinimumSize = new System.Drawing.Size(50, 50);
            this.ReportView.Name = "ReportView";
            this.ReportView.Size = new System.Drawing.Size(837, 535);
            this.ReportView.Text = "Report View";
            this.ReportView.ToolTipTitle = "Page ToolTip";
            this.ReportView.UniqueName = "90C42E72D71545555A8EB3CFAC67E7DB";
            // 
            // rptBillableEventsReport
            // 
            this.rptBillableEventsReport.ActiveViewIndex = 0;
            this.rptBillableEventsReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rptBillableEventsReport.DisplayGroupTree = false;
            this.rptBillableEventsReport.Location = new System.Drawing.Point(2, -1);
            this.rptBillableEventsReport.Name = "rptBillableEventsReport";
            this.rptBillableEventsReport.ReportSource = this.BillableEventsReport1;
            this.rptBillableEventsReport.Size = new System.Drawing.Size(830, 533);
            this.rptBillableEventsReport.TabIndex = 26;
            // 
            // lblToTime
            // 
            this.lblToTime.Location = new System.Drawing.Point(506, 18);
            this.lblToTime.Name = "lblToTime";
            this.lblToTime.Size = new System.Drawing.Size(56, 19);
            this.lblToTime.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblToTime.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblToTime.TabIndex = 30;
            this.lblToTime.Values.Text = "To Time :";
            // 
            // ToTimeDateTimePicker
            // 
            this.ToTimeDateTimePicker.CalendarTodayDate = new System.DateTime(2012, 9, 6, 0, 0, 0, 0);
            this.ToTimeDateTimePicker.CustomFormat = "dd-MM-yyyy HH:MM";
            this.ToTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ToTimeDateTimePicker.Location = new System.Drawing.Point(585, 17);
            this.ToTimeDateTimePicker.Name = "ToTimeDateTimePicker";
            this.ToTimeDateTimePicker.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.ToTimeDateTimePicker.Size = new System.Drawing.Size(112, 20);
            this.ToTimeDateTimePicker.TabIndex = 29;
            // 
            // lblFromTime
            // 
            this.lblFromTime.Location = new System.Drawing.Point(273, 19);
            this.lblFromTime.Name = "lblFromTime";
            this.lblFromTime.Size = new System.Drawing.Size(69, 19);
            this.lblFromTime.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblFromTime.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblFromTime.TabIndex = 28;
            this.lblFromTime.Values.Text = "From Time :";
            // 
            // FromTimeDateTimePicker
            // 
            this.FromTimeDateTimePicker.CalendarTodayDate = new System.DateTime(2012, 9, 6, 0, 0, 0, 0);
            this.FromTimeDateTimePicker.CustomFormat = "dd-MM-yyyy HH:MM";
            this.FromTimeDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.FromTimeDateTimePicker.Location = new System.Drawing.Point(352, 18);
            this.FromTimeDateTimePicker.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.FromTimeDateTimePicker.Name = "FromTimeDateTimePicker";
            this.FromTimeDateTimePicker.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.FromTimeDateTimePicker.Size = new System.Drawing.Size(112, 20);
            this.FromTimeDateTimePicker.TabIndex = 27;
            // 
            // ComboBoxTypeofChange
            // 
            this.ComboBoxTypeofChange.DropDownWidth = 121;
            this.ComboBoxTypeofChange.Items.AddRange(new object[] {
            "---Select---",
            "CarrierOutBoundIntended Change",
            "CarrierOutBoundDeclared Change",
            "Category Change",
            "Destination Change",
            "Dray Status Change",
            "Group Change",
            "POD Change"});
            this.ComboBoxTypeofChange.Location = new System.Drawing.Point(130, 15);
            this.ComboBoxTypeofChange.Name = "ComboBoxTypeofChange";
            this.ComboBoxTypeofChange.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.ComboBoxTypeofChange.Size = new System.Drawing.Size(137, 22);
            this.ComboBoxTypeofChange.TabIndex = 26;
            this.ComboBoxTypeofChange.Text = "---Select---";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(732, 15);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnSubmit.Size = new System.Drawing.Size(65, 24);
            this.btnSubmit.TabIndex = 22;
            this.btnSubmit.Values.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblTypeOfChange
            // 
            this.lblTypeOfChange.Location = new System.Drawing.Point(30, 18);
            this.lblTypeOfChange.Name = "lblTypeOfChange";
            this.lblTypeOfChange.Size = new System.Drawing.Size(94, 19);
            this.lblTypeOfChange.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblTypeOfChange.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblTypeOfChange.TabIndex = 20;
            this.lblTypeOfChange.Values.Text = "Type of Change :";
            // 
            // formBillableEventsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formBillableEventsReport";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.Text = "formBillableEventsReport";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formBillableEventsReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DetalsView)).EndInit();
            this.DetalsView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DetailsView)).EndInit();
            this.DetailsView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReportView)).EndInit();
            this.ReportView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ComboBoxTypeofChange)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSubmit;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblTypeOfChange;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ComboBoxTypeofChange;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblFromTime;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker FromTimeDateTimePicker;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblToTime;
        private ComponentFactory.Krypton.Toolkit.KryptonDateTimePicker ToTimeDateTimePicker;
        private BillableEventsReport BillableEventsReport1;
        private ComponentFactory.Krypton.Docking.KryptonDockableNavigator DetalsView;
        private ComponentFactory.Krypton.Navigator.KryptonPage DetailsView;
        private ComponentFactory.Krypton.Navigator.KryptonPage ReportView;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rptBillableEventsReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Change;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChangeTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContainerNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Line;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrightKind;
        private System.Windows.Forms.DataGridViewTextBoxColumn Category;
        private System.Windows.Forms.DataGridViewTextBoxColumn Facility;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Note;
        
    }
}

