namespace WAP3
{
    partial class formTallySheetNew
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.radioStuffing = new System.Windows.Forms.RadioButton();
            this.radioCarting = new System.Windows.Forms.RadioButton();
            this.cmbHandledType = new System.Windows.Forms.ComboBox();
            this.lblHandledType = new System.Windows.Forms.Label();
            this.cmbHandledBy = new System.Windows.Forms.ComboBox();
            this.lblHandledBy = new System.Windows.Forms.Label();
            this.cmbWeatherCondition = new System.Windows.Forms.ComboBox();
            this.lblWeatherCondition = new System.Windows.Forms.Label();
            this.cmbCargoCondition = new System.Windows.Forms.ComboBox();
            this.lblCargoCondition = new System.Windows.Forms.Label();
            this.txtCTotal = new System.Windows.Forms.TextBox();
            this.lblCTotal = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHight = new System.Windows.Forms.TextBox();
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblHight = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.lblOPLoc = new System.Windows.Forms.Label();
            this.lblOpQty = new System.Windows.Forms.Label();
            this.txtOpQty = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtTruckNo = new System.Windows.Forms.Label();
            this.lblTruckNo = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtTotalQty1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblStatus1 = new System.Windows.Forms.Label();
            this.dataGridTallySheet = new System.Windows.Forms.DataGrid();
            this.lblContainerNo = new System.Windows.Forms.Label();
            this.txtContainerNo = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 268);
            this.tabControl1.TabIndex = 75;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Highlight;
            this.tabPage1.Controls.Add(this.txtContainerNo);
            this.tabPage1.Controls.Add(this.lblContainerNo);
            this.tabPage1.Controls.Add(this.radioStuffing);
            this.tabPage1.Controls.Add(this.radioCarting);
            this.tabPage1.Controls.Add(this.cmbHandledType);
            this.tabPage1.Controls.Add(this.lblHandledType);
            this.tabPage1.Controls.Add(this.cmbHandledBy);
            this.tabPage1.Controls.Add(this.lblHandledBy);
            this.tabPage1.Controls.Add(this.cmbWeatherCondition);
            this.tabPage1.Controls.Add(this.lblWeatherCondition);
            this.tabPage1.Controls.Add(this.cmbCargoCondition);
            this.tabPage1.Controls.Add(this.lblCargoCondition);
            this.tabPage1.Controls.Add(this.txtCTotal);
            this.tabPage1.Controls.Add(this.lblCTotal);
            this.tabPage1.Controls.Add(this.txtWidth);
            this.tabPage1.Controls.Add(this.txtHight);
            this.tabPage1.Controls.Add(this.lblWidth);
            this.tabPage1.Controls.Add(this.lblHight);
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.cmbLocation);
            this.tabPage1.Controls.Add(this.lblOPLoc);
            this.tabPage1.Controls.Add(this.lblOpQty);
            this.tabPage1.Controls.Add(this.txtOpQty);
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Controls.Add(this.txtTruckNo);
            this.tabPage1.Controls.Add(this.lblTruckNo);
            this.tabPage1.Controls.Add(this.btnAdd);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(240, 245);
            this.tabPage1.Text = "TallySheetEntry";
            // 
            // radioStuffing
            // 
            this.radioStuffing.Location = new System.Drawing.Point(73, 14);
            this.radioStuffing.Name = "radioStuffing";
            this.radioStuffing.Size = new System.Drawing.Size(71, 20);
            this.radioStuffing.TabIndex = 2;
            this.radioStuffing.Text = "Stuffing";
            // 
            // radioCarting
            // 
            this.radioCarting.Location = new System.Drawing.Point(4, 14);
            this.radioCarting.Name = "radioCarting";
            this.radioCarting.Size = new System.Drawing.Size(67, 20);
            this.radioCarting.TabIndex = 0;
            this.radioCarting.Text = "Carting";
            // 
            // cmbHandledType
            // 
            this.cmbHandledType.Location = new System.Drawing.Point(113, 147);
            this.cmbHandledType.Name = "cmbHandledType";
            this.cmbHandledType.Size = new System.Drawing.Size(125, 22);
            this.cmbHandledType.TabIndex = 7;
            // 
            // lblHandledType
            // 
            this.lblHandledType.Location = new System.Drawing.Point(3, 152);
            this.lblHandledType.Name = "lblHandledType";
            this.lblHandledType.Size = new System.Drawing.Size(109, 15);
            this.lblHandledType.Text = "Handled Type";
            // 
            // cmbHandledBy
            // 
            this.cmbHandledBy.Location = new System.Drawing.Point(113, 124);
            this.cmbHandledBy.Name = "cmbHandledBy";
            this.cmbHandledBy.Size = new System.Drawing.Size(125, 22);
            this.cmbHandledBy.TabIndex = 6;
            // 
            // lblHandledBy
            // 
            this.lblHandledBy.Location = new System.Drawing.Point(3, 129);
            this.lblHandledBy.Name = "lblHandledBy";
            this.lblHandledBy.Size = new System.Drawing.Size(109, 15);
            this.lblHandledBy.Text = "Handled By";
            // 
            // cmbWeatherCondition
            // 
            this.cmbWeatherCondition.Location = new System.Drawing.Point(113, 101);
            this.cmbWeatherCondition.Name = "cmbWeatherCondition";
            this.cmbWeatherCondition.Size = new System.Drawing.Size(125, 22);
            this.cmbWeatherCondition.TabIndex = 5;
            // 
            // lblWeatherCondition
            // 
            this.lblWeatherCondition.Location = new System.Drawing.Point(3, 106);
            this.lblWeatherCondition.Name = "lblWeatherCondition";
            this.lblWeatherCondition.Size = new System.Drawing.Size(109, 15);
            this.lblWeatherCondition.Text = "Weather Condition";
            // 
            // cmbCargoCondition
            // 
            this.cmbCargoCondition.Location = new System.Drawing.Point(113, 78);
            this.cmbCargoCondition.Name = "cmbCargoCondition";
            this.cmbCargoCondition.Size = new System.Drawing.Size(125, 22);
            this.cmbCargoCondition.TabIndex = 4;
            // 
            // lblCargoCondition
            // 
            this.lblCargoCondition.Location = new System.Drawing.Point(3, 83);
            this.lblCargoCondition.Name = "lblCargoCondition";
            this.lblCargoCondition.Size = new System.Drawing.Size(103, 15);
            this.lblCargoCondition.Text = "Cargo Condition";
            // 
            // txtCTotal
            // 
            this.txtCTotal.Enabled = false;
            this.txtCTotal.Location = new System.Drawing.Point(105, 184);
            this.txtCTotal.MaxLength = 5;
            this.txtCTotal.Name = "txtCTotal";
            this.txtCTotal.ReadOnly = true;
            this.txtCTotal.Size = new System.Drawing.Size(40, 21);
            this.txtCTotal.TabIndex = 10;
            this.txtCTotal.TabStop = false;
            // 
            // lblCTotal
            // 
            this.lblCTotal.Location = new System.Drawing.Point(105, 170);
            this.lblCTotal.Name = "lblCTotal";
            this.lblCTotal.Size = new System.Drawing.Size(40, 15);
            this.lblCTotal.Text = "RTotal";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(5, 184);
            this.txtWidth.MaxLength = 5;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(40, 21);
            this.txtWidth.TabIndex = 8;
            this.txtWidth.TextChanged += new System.EventHandler(this.txtWidth_TextChanged);
            // 
            // txtHight
            // 
            this.txtHight.Location = new System.Drawing.Point(53, 184);
            this.txtHight.MaxLength = 5;
            this.txtHight.Name = "txtHight";
            this.txtHight.Size = new System.Drawing.Size(40, 21);
            this.txtHight.TabIndex = 9;
            this.txtHight.TextChanged += new System.EventHandler(this.txtHight_TextChanged);
            // 
            // lblWidth
            // 
            this.lblWidth.Location = new System.Drawing.Point(5, 170);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(40, 15);
            this.lblWidth.Text = "Width";
            // 
            // lblHight
            // 
            this.lblHight.Location = new System.Drawing.Point(53, 170);
            this.lblHight.Name = "lblHight";
            this.lblHight.Size = new System.Drawing.Size(46, 15);
            this.lblHight.Text = "Height";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.Location = new System.Drawing.Point(4, 221);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(233, 22);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbLocation
            // 
            this.cmbLocation.Location = new System.Drawing.Point(113, 55);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(125, 22);
            this.cmbLocation.TabIndex = 3;
            // 
            // lblOPLoc
            // 
            this.lblOPLoc.Location = new System.Drawing.Point(3, 60);
            this.lblOPLoc.Name = "lblOPLoc";
            this.lblOPLoc.Size = new System.Drawing.Size(92, 15);
            this.lblOPLoc.Text = "OP Location";
            // 
            // lblOpQty
            // 
            this.lblOpQty.Location = new System.Drawing.Point(142, 17);
            this.lblOpQty.Name = "lblOpQty";
            this.lblOpQty.Size = new System.Drawing.Size(45, 15);
            this.lblOpQty.Text = "OP.Qty.";
            // 
            // txtOpQty
            // 
            this.txtOpQty.Location = new System.Drawing.Point(188, 11);
            this.txtOpQty.MaxLength = 5;
            this.txtOpQty.Name = "txtOpQty";
            this.txtOpQty.ReadOnly = true;
            this.txtOpQty.Size = new System.Drawing.Size(49, 21);
            this.txtOpQty.TabIndex = 113;
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Maroon;
            this.lblStatus.Location = new System.Drawing.Point(4, 206);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(235, 15);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtTruckNo
            // 
            this.txtTruckNo.ForeColor = System.Drawing.Color.White;
            this.txtTruckNo.Location = new System.Drawing.Point(61, -1);
            this.txtTruckNo.Name = "txtTruckNo";
            this.txtTruckNo.Size = new System.Drawing.Size(147, 15);
            this.txtTruckNo.Text = "Truck No";
            // 
            // lblTruckNo
            // 
            this.lblTruckNo.Location = new System.Drawing.Point(3, -1);
            this.lblTruckNo.Name = "lblTruckNo";
            this.lblTruckNo.Size = new System.Drawing.Size(64, 15);
            this.lblTruckNo.Text = "Truck No:";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnAdd.Location = new System.Drawing.Point(165, 183);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 22);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Highlight;
            this.tabPage2.Controls.Add(this.txtTotalQty1);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.lblStatus1);
            this.tabPage2.Controls.Add(this.dataGridTallySheet);
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(232, 242);
            this.tabPage2.Text = "TallySheetGrid";
            // 
            // txtTotalQty1
            // 
            this.txtTotalQty1.ForeColor = System.Drawing.Color.Maroon;
            this.txtTotalQty1.Location = new System.Drawing.Point(186, 15);
            this.txtTotalQty1.Name = "txtTotalQty1";
            this.txtTotalQty1.Size = new System.Drawing.Size(50, 15);
            this.txtTotalQty1.Text = "0";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(128, 15);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 15);
            this.label12.Text = "Total";
            // 
            // lblStatus1
            // 
            this.lblStatus1.Location = new System.Drawing.Point(3, 15);
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(116, 15);
            // 
            // dataGridTallySheet
            // 
            this.dataGridTallySheet.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dataGridTallySheet.Location = new System.Drawing.Point(0, 51);
            this.dataGridTallySheet.Name = "dataGridTallySheet";
            this.dataGridTallySheet.Size = new System.Drawing.Size(240, 194);
            this.dataGridTallySheet.TabIndex = 1;
            // 
            // lblContainerNo
            // 
            this.lblContainerNo.Location = new System.Drawing.Point(3, 37);
            this.lblContainerNo.Name = "lblContainerNo";
            this.lblContainerNo.Size = new System.Drawing.Size(92, 15);
            this.lblContainerNo.Text = "Container No";
            // 
            // txtContainerNo
            // 
            this.txtContainerNo.Location = new System.Drawing.Point(113, 33);
            this.txtContainerNo.Name = "txtContainerNo";
            this.txtContainerNo.Size = new System.Drawing.Size(125, 21);
            this.txtContainerNo.TabIndex = 119;
            // 
            // formTallySheetNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.tabControl1);
            this.Menu = this.mainMenu1;
            this.Name = "formTallySheetNew";
            this.Text = "Tally Sheet";
            this.Load += new System.EventHandler(this.formTallySheetNew_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.Label lblOPLoc;
        private System.Windows.Forms.Label lblOpQty;
        public System.Windows.Forms.TextBox txtOpQty;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label txtTruckNo;
        private System.Windows.Forms.Label lblTruckNo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label txtTotalQty1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblStatus1;
        private System.Windows.Forms.DataGrid dataGridTallySheet;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblHight;
        public System.Windows.Forms.TextBox txtWidth;
        public System.Windows.Forms.TextBox txtHight;
        public System.Windows.Forms.TextBox txtCTotal;
        private System.Windows.Forms.Label lblCTotal;
        private System.Windows.Forms.Label lblCargoCondition;
        private System.Windows.Forms.ComboBox cmbCargoCondition;
        private System.Windows.Forms.ComboBox cmbWeatherCondition;
        private System.Windows.Forms.Label lblWeatherCondition;
        private System.Windows.Forms.ComboBox cmbHandledBy;
        private System.Windows.Forms.Label lblHandledBy;
        private System.Windows.Forms.ComboBox cmbHandledType;
        private System.Windows.Forms.Label lblHandledType;
        private System.Windows.Forms.RadioButton radioStuffing;
        private System.Windows.Forms.RadioButton radioCarting;
        private System.Windows.Forms.Label lblContainerNo;
        private System.Windows.Forms.TextBox txtContainerNo;
    }
}