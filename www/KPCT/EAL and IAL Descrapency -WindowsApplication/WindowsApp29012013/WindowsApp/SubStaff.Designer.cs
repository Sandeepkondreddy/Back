namespace WindowsApp
{
    partial class SubStaff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubStaff));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.gridSubStaff = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.subStaffDetailsGroupBox = new ComponentFactory.Krypton.Toolkit.KryptonGroupBox();
            this.ComboDesignation = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.ComboDepartment = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtRemarks = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtContactNo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtSubStaffName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtSubStaffCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblRemarks = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblContactNo = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblDesignation = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblDepartment = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblSubstaffName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblsubStaffCode = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.SubStaffCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubStaffName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.Designation = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn();
            this.ContactNo = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            this.Remarks = new ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSubStaff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subStaffDetailsGroupBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subStaffDetailsGroupBox.Panel)).BeginInit();
            this.subStaffDetailsGroupBox.Panel.SuspendLayout();
            this.subStaffDetailsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ComboDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboDepartment)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.btnSave);
            this.kryptonPanel.Controls.Add(this.gridSubStaff);
            this.kryptonPanel.Controls.Add(this.subStaffDetailsGroupBox);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel.TabIndex = 0;
            this.kryptonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonPanel_Paint);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(744, 234);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnSave.Size = new System.Drawing.Size(65, 24);
            this.btnSave.TabIndex = 23;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gridSubStaff
            // 
            this.gridSubStaff.AllowUserToOrderColumns = true;
            this.gridSubStaff.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SubStaffCode,
            this.SubStaffName,
            this.Department,
            this.Designation,
            this.ContactNo,
            this.Remarks});
            this.gridSubStaff.Location = new System.Drawing.Point(66, 221);
            this.gridSubStaff.Name = "gridSubStaff";
            this.gridSubStaff.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.gridSubStaff.Size = new System.Drawing.Size(639, 165);
            this.gridSubStaff.TabIndex = 1;
            // 
            // subStaffDetailsGroupBox
            // 
            this.subStaffDetailsGroupBox.Location = new System.Drawing.Point(3, 12);
            this.subStaffDetailsGroupBox.Name = "subStaffDetailsGroupBox";
            this.subStaffDetailsGroupBox.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            // 
            // subStaffDetailsGroupBox.Panel
            // 
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.ComboDesignation);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.ComboDepartment);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.txtRemarks);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.txtContactNo);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.txtSubStaffName);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.txtSubStaffCode);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.lblRemarks);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.lblContactNo);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.lblDesignation);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.lblDepartment);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.lblSubstaffName);
            this.subStaffDetailsGroupBox.Panel.Controls.Add(this.lblsubStaffCode);
            this.subStaffDetailsGroupBox.Size = new System.Drawing.Size(839, 203);
            this.subStaffDetailsGroupBox.TabIndex = 0;
            this.subStaffDetailsGroupBox.Text = "Caption";
            // 
            // ComboDesignation
            // 
            this.ComboDesignation.DropDownWidth = 121;
            this.ComboDesignation.Items.AddRange(new object[] {
            "---Select---"});
            this.ComboDesignation.Location = new System.Drawing.Point(493, 38);
            this.ComboDesignation.Name = "ComboDesignation";
            this.ComboDesignation.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.ComboDesignation.Size = new System.Drawing.Size(144, 22);
            this.ComboDesignation.TabIndex = 32;
            this.ComboDesignation.Text = "---Select---";
            // 
            // ComboDepartment
            // 
            this.ComboDepartment.DropDownWidth = 121;
            this.ComboDepartment.Items.AddRange(new object[] {
            "---Select---"});
            this.ComboDepartment.Location = new System.Drawing.Point(150, 113);
            this.ComboDepartment.Name = "ComboDepartment";
            this.ComboDepartment.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.ComboDepartment.Size = new System.Drawing.Size(144, 22);
            this.ComboDepartment.TabIndex = 31;
            this.ComboDepartment.Text = "---Select---";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Location = new System.Drawing.Point(493, 113);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtRemarks.Size = new System.Drawing.Size(144, 50);
            this.txtRemarks.TabIndex = 30;
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(493, 72);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtContactNo.Size = new System.Drawing.Size(144, 22);
            this.txtContactNo.TabIndex = 29;
            // 
            // txtSubStaffName
            // 
            this.txtSubStaffName.Location = new System.Drawing.Point(150, 72);
            this.txtSubStaffName.Name = "txtSubStaffName";
            this.txtSubStaffName.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtSubStaffName.Size = new System.Drawing.Size(144, 22);
            this.txtSubStaffName.TabIndex = 28;
            // 
            // txtSubStaffCode
            // 
            this.txtSubStaffCode.Location = new System.Drawing.Point(150, 36);
            this.txtSubStaffCode.Name = "txtSubStaffCode";
            this.txtSubStaffCode.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtSubStaffCode.Size = new System.Drawing.Size(144, 22);
            this.txtSubStaffCode.TabIndex = 27;
            // 
            // lblRemarks
            // 
            this.lblRemarks.Location = new System.Drawing.Point(368, 113);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(58, 19);
            this.lblRemarks.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblRemarks.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblRemarks.TabIndex = 26;
            this.lblRemarks.Values.Text = "Remarks :";
            // 
            // lblContactNo
            // 
            this.lblContactNo.Location = new System.Drawing.Point(368, 72);
            this.lblContactNo.Name = "lblContactNo";
            this.lblContactNo.Size = new System.Drawing.Size(75, 19);
            this.lblContactNo.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblContactNo.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblContactNo.TabIndex = 25;
            this.lblContactNo.Values.Text = "Contact No. :";
            // 
            // lblDesignation
            // 
            this.lblDesignation.Location = new System.Drawing.Point(368, 38);
            this.lblDesignation.Name = "lblDesignation";
            this.lblDesignation.Size = new System.Drawing.Size(76, 19);
            this.lblDesignation.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblDesignation.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblDesignation.TabIndex = 24;
            this.lblDesignation.Values.Text = "Designation :";
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(17, 113);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(76, 19);
            this.lblDepartment.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblDepartment.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblDepartment.TabIndex = 23;
            this.lblDepartment.Values.Text = "Department :";
            // 
            // lblSubstaffName
            // 
            this.lblSubstaffName.Location = new System.Drawing.Point(17, 72);
            this.lblSubstaffName.Name = "lblSubstaffName";
            this.lblSubstaffName.Size = new System.Drawing.Size(94, 19);
            this.lblSubstaffName.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblSubstaffName.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblSubstaffName.TabIndex = 22;
            this.lblSubstaffName.Values.Text = "Sub Staff Name :";
            // 
            // lblsubStaffCode
            // 
            this.lblsubStaffCode.Location = new System.Drawing.Point(17, 38);
            this.lblsubStaffCode.Name = "lblsubStaffCode";
            this.lblsubStaffCode.Size = new System.Drawing.Size(90, 19);
            this.lblsubStaffCode.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblsubStaffCode.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblsubStaffCode.TabIndex = 21;
            this.lblsubStaffCode.Values.Text = "Sub Staff Code :";
            // 
            // SubStaffCode
            // 
            this.SubStaffCode.HeaderText = "Sub Staff Code";
            this.SubStaffCode.Name = "SubStaffCode";
            // 
            // SubStaffName
            // 
            this.SubStaffName.HeaderText = "Sub Staff Name";
            this.SubStaffName.Name = "SubStaffName";
            // 
            // Department
            // 
            this.Department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Department.DropDownWidth = 121;
            this.Department.HeaderText = "Department";
            this.Department.Items.AddRange(new string[] {
            "---Select---",
            "Accounts",
            "Administration",
            "Aviation",
            "CMAC",
            "CPS",
            "CVR Complex",
            "Civil Maintenance",
            "Container Terminal",
            "Customer Service",
            "DCO AVLS",
            "DCO CY",
            "DCO E",
            "DCO GO",
            "DCO IOY",
            "DCO JO",
            "DCO L",
            "DCO O",
            "DCO OP",
            "DCO PC",
            "DCO R",
            "DCO WB",
            "DCO WH",
            "DCO WS",
            "DCO Water",
            "EHS",
            "ETS",
            "ETS MBU",
            "ETS MHC",
            "Electrical Supervisor",
            "F&S",
            "Hospitality",
            "Human Resources",
            "Information Technology",
            "KPCL",
            "KPPCL",
            "Liquid Storage",
            "MMHS",
            "Marine",
            "Marine D",
            "Marine EHS",
            "Marine F&S",
            "Marine PC",
            "Marine QC",
            "Marine Services",
            "Marketing",
            "PDF",
            "PDF O",
            "POC",
            "Port Customs",
            "Project",
            "Public Relations",
            "Railway Logistics",
            "Security",
            "Services",
            "Stores & Procurement"});
            this.Department.Name = "Department";
            this.Department.Width = 100;
            // 
            // Designation
            // 
            this.Designation.AutoCompleteCustomSource.AddRange(new string[] {
            "Driver"});
            this.Designation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Designation.DropDownWidth = 121;
            this.Designation.HeaderText = "Designation";
            this.Designation.Items.AddRange(new string[] {
            "---Select---",
            "AC Technician",
            "ATM Rules",
            "Accountant",
            "Accounts Assistant",
            "Admin-Facilitates",
            "Advisor - Dredging & Reclamation",
            "Advisor - Railway",
            "Advocate",
            "Area Officer",
            "Assistant",
            "Assistant Manager (Hydrography)",
            "Assistant Manager (Survey)",
            "Assistant Manager (yards)",
            "Assistant(POC)",
            "Asst Cook",
            "Asst Crane Operator",
            "Asst Engineer (Electrical)-Dredger",
            "Asst Engineer(civil)",
            "Asst Engineer(mech)",
            "Asst Foreman",
            "Asst General Manager",
            "Asst Manager",
            "Asst Manager - Custom Desk",
            "Asst Manager - Workshop",
            "Asst Operator",
            "Asst Radio Officer",
            "Asst Radio Operator",
            "Asst Reach Stacker Operator",
            "Asst Retai out let Manager",
            "Asst.Yard Officer",
            "Audit Assistant",
            "Boat Driver",
            "Boat Helper",
            "Branch Manager",
            "Business Analyst",
            "C.S.O",
            "Captain",
            "Cashier",
            "Checker",
            "Cheif Advisor",
            "Cheif Officer",
            "Chief Control Officer",
            "Chief Controler",
            "Chief Executive Officer",
            "Chief Manager",
            "Chief Project Manager",
            "Civil Works",
            "Cleaner",
            "Clerk(Tally)",
            "Comm Executive",
            "Commercial Manager",
            "Commercial Officer",
            "Commi-I",
            "Company Secretary",
            "Computer Operator",
            "Consultant",
            "Contractor",
            "Cook",
            "Cook cum Pantry Boy",
            "Crane Operator",
            "Crane Technician",
            "Customer Destination Representative",
            "Customer Service Agent",
            "Customer Service Executive",
            "D Automobile Engineering",
            "DCO-OP",
            "DCO-Operations Pipeline",
            "DGM(Liquid Storage)",
            "DGM(Security-System)",
            "DGM-Container Terminal",
            "Data Base Administrator",
            "Data Entry Operator",
            "Deck Cadet",
            "Deputy Manager",
            "Desktop Support Engineer",
            "Developer",
            "Diesel Pump Tester",
            "Diploma(CSE)",
            "Diver",
            "Dredger Operator",
            "Dredging Officer",
            "Dredging Supervisor",
            "Driver",
            "Driver-cum-Fire Operator",
            "Dy Chief Controller",
            "Dy General Manager",
            "EAP",
            "Electrician",
            "Engineer",
            "Engineer MMHS",
            "Executive",
            "Executive - HR",
            "Executive - Operations",
            "Executive Assistant",
            "Executive Chef",
            "Faculty",
            "Fire Officer",
            "Fire Supervisor",
            "Fire Tender Driver",
            "Fire Tender Driver cum Operator",
            "Fireman",
            "Fitter",
            "Foreman",
            "Forklift Operator",
            "Front Office Executive",
            "GPC",
            "GTE - Weighbridge",
            "Garage Incharge",
            "Gardener",
            "General Manager",
            "General Purpose Crew",
            "Geologist",
            "Graduate Trainee",
            "Graduate Trainee - Jetty",
            "Graduate Trainee Engineer",
            "Graduate Trainee Officer(Legal)",
            "Gravel Supervisor",
            "HAV",
            "Head",
            "Helper",
            "Hon. Sub. Major",
            "Hospitality",
            "Hydra Operataor",
            "Hydrographic Surveyor",
            "Incharge",
            "Jetty Supervisor",
            "Jr Dredger Operator",
            "Jr Officer",
            "Jr. Asst",
            "Junior Checker",
            "Junior Developer",
            "Junior Engineer",
            "Junior Executive",
            "Lecturer",
            "Legal Officer",
            "MBU Operator",
            "Maintenance Supervisor",
            "Management Trainee",
            "Manager",
            "Manager (Brand Communications)",
            "Marketing Executive",
            "Master",
            "Mechanic",
            "Mechanical Engineer",
            "Mechanical Fitter",
            "Mechanist",
            "Meter Designer",
            "Mobile Crane Operator",
            "Mobile Mechanical Crane Operator",
            "NK",
            "Networking Engineer",
            "Office Assistant",
            "Office Boy",
            "Officer",
            "Operator",
            "Operator Dredger",
            "Personal Assistant",
            "Personnel Officer",
            "Pilot",
            "Port Yard Master",
            "President - Engineering",
            "Procurement",
            "Programer",
            "Programmer",
            "Project Engineer",
            "Purchasing & Stores Manager",
            "Quality Controller",
            "Quality Specialists",
            "Quarry Incharge",
            "RTG Operator",
            "Radio Officer",
            "Reach Stack Operator",
            "Reach Stacker Operator",
            "Receptionist",
            "Regional Manager",
            "SEP",
            "SPA Therapist",
            "SR-III",
            "Safety Supervisor",
            "Sales Executive",
            "Sales Incharg",
            "Sales Manager",
            "Secretary",
            "Section Controller",
            "Security Guard",
            "Security Officer",
            "Self Employed",
            "Senior Business Analyst",
            "Senior Dredging Operator",
            "Senior Engineer",
            "Senior Executive",
            "Senior Fire Operator",
            "Senior Manager",
            "Senior Officer",
            "Senior Technician",
            "Sennebogen Operator",
            "Shift Incharge",
            "Shift Manager",
            "Shipping Executive",
            "Site Engineer",
            "Skipper",
            "Solar Lighter",
            "Sous Chef",
            "Spa & Yoga Therapist",
            "Sr Assistant",
            "Sr Computer Operator",
            "Sr Crane Operator",
            "Sr Draftsman",
            "Sr Dredging Operator",
            "Sr Electrician",
            "Sr Engineer",
            "Sr Engineer (Networking & Hardware)",
            "Sr Engineer - Weighbridge Maintenance",
            "Sr Fire Operator",
            "Sr Pilot",
            "Sr Radio Officer",
            "Sr Tech cum Operator",
            "Sr Technician",
            "Sr Technician cum Asst Crane Opr",
            "Sr Vice President",
            "Sr Yard Master",
            "Sr.Manager",
            "Stenographer",
            "Steward",
            "Store Keeper",
            "Superintendent",
            "Supervisor",
            "Support Engineer",
            "Support Engineer - Video Survilance",
            "System Administrator",
            "TY",
            "Teacher",
            "Tech cum Asst Crane Operator",
            "Tech cum Asst Operator",
            "Technical Incharge",
            "Technican",
            "Technician",
            "Technician (E & I)",
            "Technician cum Asst Operator",
            "Technician-cum-Crane Operator",
            "Tide Supervisor",
            "Trainee",
            "Trainee - Electrical",
            "Trainee Checker",
            "Trainee Crane Operator",
            "Trainee Draughtsman",
            "Trainee Engineer",
            "Trainee Fireman",
            "Trainee Marine Officer",
            "Trainee Operator",
            "Trainee Pilot",
            "Trainee Reach Stacker Operator",
            "Trainee Supervisor",
            "Trainee Technician",
            "Trainee Tide Supervisor",
            "Trainee Yard Master",
            "Vice President",
            "Watchman",
            "Weigh Bridge Operator",
            "Weigh Bridge Receiver",
            "Weighbridge Maintenance Engineer",
            "Welder",
            "Welder Helper",
            "Yard Master",
            "Yard Office Receiver",
            "Yard Officer",
            "Yard Supervisor",
            "Zonal Manager",
            "Zonal Officer",
            "cpa"});
            this.Designation.Name = "Designation";
            this.Designation.Width = 100;
            // 
            // ContactNo
            // 
            this.ContactNo.HeaderText = "Contact No";
            this.ContactNo.Name = "ContactNo";
            this.ContactNo.Width = 100;
            // 
            // Remarks
            // 
            this.Remarks.HeaderText = "Remarks";
            this.Remarks.Name = "Remarks";
            this.Remarks.Width = 100;
            // 
            // SubStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SubStaff";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.Text = "SubStaff";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.SubStaff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSubStaff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subStaffDetailsGroupBox.Panel)).EndInit();
            this.subStaffDetailsGroupBox.Panel.ResumeLayout(false);
            this.subStaffDetailsGroupBox.Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subStaffDetailsGroupBox)).EndInit();
            this.subStaffDetailsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ComboDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ComboDepartment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonGroupBox subStaffDetailsGroupBox;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblsubStaffCode;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSubstaffName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDepartment;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblDesignation;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblContactNo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblRemarks;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSubStaffCode;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSubStaffName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtContactNo;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtRemarks;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ComboDepartment;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox ComboDesignation;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView gridSubStaff;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubStaffCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubStaffName;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn Department;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewComboBoxColumn Designation;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn ContactNo;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridViewTextBoxColumn Remarks;
    }
}

