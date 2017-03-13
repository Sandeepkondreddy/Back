namespace WAP3
{
    partial class formConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formConfig));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItemHome = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.btnRequestConfig = new System.Windows.Forms.Button();
            this.lblReaderNo = new System.Windows.Forms.Label();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.lblReaderIP = new System.Windows.Forms.Label();
            this.lblMacAdd = new System.Windows.Forms.Label();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.lblServerPort = new System.Windows.Forms.Label();
            this.lblTaskNo = new System.Windows.Forms.Label();
            this.lblProc = new System.Windows.Forms.Label();
            this.txtReaderNo = new System.Windows.Forms.TextBox();
            this.txtReaderName = new System.Windows.Forms.TextBox();
            this.txtReaderIP = new System.Windows.Forms.TextBox();
            this.txtMacAdd = new System.Windows.Forms.TextBox();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtTaskId = new System.Windows.Forms.TextBox();
            this.txtProcess = new System.Windows.Forms.TextBox();
            this.radioUnRegistration = new System.Windows.Forms.RadioButton();
            this.radioRegistration = new System.Windows.Forms.RadioButton();
            this.radioVerification = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.cmbConnectionType = new System.Windows.Forms.ComboBox();
            this.lblConnection = new System.Windows.Forms.Label();
            this.radioYard = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItemHome);
            this.mainMenu1.MenuItems.Add(this.menuItemExit);
            // 
            // menuItemHome
            // 
            this.menuItemHome.MenuItems.Add(this.menuItem1);
            this.menuItemHome.MenuItems.Add(this.menuItem2);
            this.menuItemHome.Text = "Home";
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Connect";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Sync";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click_1);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Text = "Logout";
            this.menuItemExit.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // btnRequestConfig
            // 
            this.btnRequestConfig.BackColor = System.Drawing.Color.White;
            this.btnRequestConfig.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.btnRequestConfig.Location = new System.Drawing.Point(3, 3);
            this.btnRequestConfig.Name = "btnRequestConfig";
            this.btnRequestConfig.Size = new System.Drawing.Size(70, 25);
            this.btnRequestConfig.TabIndex = 2;
            this.btnRequestConfig.Text = "Request";
            this.btnRequestConfig.Click += new System.EventHandler(this.btnRequestConfig_Click);
            // 
            // lblReaderNo
            // 
            this.lblReaderNo.Location = new System.Drawing.Point(3, 35);
            this.lblReaderNo.Name = "lblReaderNo";
            this.lblReaderNo.Size = new System.Drawing.Size(69, 20);
            this.lblReaderNo.Text = "Reader No";
            // 
            // lblReaderName
            // 
            this.lblReaderName.Location = new System.Drawing.Point(3, 62);
            this.lblReaderName.Name = "lblReaderName";
            this.lblReaderName.Size = new System.Drawing.Size(78, 20);
            this.lblReaderName.Text = "Reader Name";
            // 
            // lblReaderIP
            // 
            this.lblReaderIP.Location = new System.Drawing.Point(4, 87);
            this.lblReaderIP.Name = "lblReaderIP";
            this.lblReaderIP.Size = new System.Drawing.Size(69, 20);
            this.lblReaderIP.Text = "Reader IP";
            // 
            // lblMacAdd
            // 
            this.lblMacAdd.Location = new System.Drawing.Point(4, 114);
            this.lblMacAdd.Name = "lblMacAdd";
            this.lblMacAdd.Size = new System.Drawing.Size(69, 20);
            this.lblMacAdd.Text = "Mac Add";
            // 
            // lblServerIP
            // 
            this.lblServerIP.Location = new System.Drawing.Point(4, 141);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(69, 20);
            this.lblServerIP.Text = "ServerIP";
            // 
            // lblServerPort
            // 
            this.lblServerPort.Location = new System.Drawing.Point(3, 168);
            this.lblServerPort.Name = "lblServerPort";
            this.lblServerPort.Size = new System.Drawing.Size(69, 20);
            this.lblServerPort.Text = "Server Port";
            // 
            // lblTaskNo
            // 
            this.lblTaskNo.Location = new System.Drawing.Point(3, 245);
            this.lblTaskNo.Name = "lblTaskNo";
            this.lblTaskNo.Size = new System.Drawing.Size(53, 20);
            this.lblTaskNo.Text = "Task No";
            // 
            // lblProc
            // 
            this.lblProc.Location = new System.Drawing.Point(122, 245);
            this.lblProc.Name = "lblProc";
            this.lblProc.Size = new System.Drawing.Size(34, 20);
            this.lblProc.Text = "Proc";
            // 
            // txtReaderNo
            // 
            this.txtReaderNo.Location = new System.Drawing.Point(87, 34);
            this.txtReaderNo.Name = "txtReaderNo";
            this.txtReaderNo.ReadOnly = true;
            this.txtReaderNo.Size = new System.Drawing.Size(59, 21);
            this.txtReaderNo.TabIndex = 18;
            // 
            // txtReaderName
            // 
            this.txtReaderName.Location = new System.Drawing.Point(87, 61);
            this.txtReaderName.Name = "txtReaderName";
            this.txtReaderName.ReadOnly = true;
            this.txtReaderName.Size = new System.Drawing.Size(119, 21);
            this.txtReaderName.TabIndex = 19;
            // 
            // txtReaderIP
            // 
            this.txtReaderIP.Location = new System.Drawing.Point(87, 86);
            this.txtReaderIP.Name = "txtReaderIP";
            this.txtReaderIP.ReadOnly = true;
            this.txtReaderIP.Size = new System.Drawing.Size(119, 21);
            this.txtReaderIP.TabIndex = 20;
            // 
            // txtMacAdd
            // 
            this.txtMacAdd.Location = new System.Drawing.Point(87, 113);
            this.txtMacAdd.Name = "txtMacAdd";
            this.txtMacAdd.ReadOnly = true;
            this.txtMacAdd.Size = new System.Drawing.Size(119, 21);
            this.txtMacAdd.TabIndex = 21;
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(87, 140);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.ReadOnly = true;
            this.txtServerIP.Size = new System.Drawing.Size(119, 21);
            this.txtServerIP.TabIndex = 22;
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(87, 167);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.ReadOnly = true;
            this.txtServerPort.Size = new System.Drawing.Size(119, 21);
            this.txtServerPort.TabIndex = 23;
            // 
            // txtTaskId
            // 
            this.txtTaskId.Location = new System.Drawing.Point(52, 244);
            this.txtTaskId.Name = "txtTaskId";
            this.txtTaskId.ReadOnly = true;
            this.txtTaskId.Size = new System.Drawing.Size(69, 21);
            this.txtTaskId.TabIndex = 32;
            // 
            // txtProcess
            // 
            this.txtProcess.Location = new System.Drawing.Point(162, 244);
            this.txtProcess.Name = "txtProcess";
            this.txtProcess.ReadOnly = true;
            this.txtProcess.Size = new System.Drawing.Size(75, 21);
            this.txtProcess.TabIndex = 33;
            // 
            // radioUnRegistration
            // 
            this.radioUnRegistration.Location = new System.Drawing.Point(123, 8);
            this.radioUnRegistration.Name = "radioUnRegistration";
            this.radioUnRegistration.Size = new System.Drawing.Size(61, 20);
            this.radioUnRegistration.TabIndex = 58;
            this.radioUnRegistration.Text = "UnReg";
            // 
            // radioRegistration
            // 
            this.radioRegistration.Location = new System.Drawing.Point(75, 8);
            this.radioRegistration.Name = "radioRegistration";
            this.radioRegistration.Size = new System.Drawing.Size(45, 20);
            this.radioRegistration.TabIndex = 57;
            this.radioRegistration.Text = "Reg";
            // 
            // radioVerification
            // 
            this.radioVerification.Enabled = false;
            this.radioVerification.Location = new System.Drawing.Point(154, 35);
            this.radioVerification.Name = "radioVerification";
            this.radioVerification.Size = new System.Drawing.Size(56, 20);
            this.radioVerification.TabIndex = 67;
            this.radioVerification.Text = "Verify";
            this.radioVerification.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.Text = "Location";
            this.label1.Visible = false;
            // 
            // cmbLocation
            // 
            this.cmbLocation.Items.Add("CT Gate");
            this.cmbLocation.Items.Add("Warehouse");
            this.cmbLocation.Location = new System.Drawing.Point(87, 192);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(119, 22);
            this.cmbLocation.TabIndex = 78;
            this.cmbLocation.Visible = false;
            // 
            // cmbConnectionType
            // 
            this.cmbConnectionType.Items.Add("Wifi");
            this.cmbConnectionType.Items.Add("GPRS");
            this.cmbConnectionType.Location = new System.Drawing.Point(87, 218);
            this.cmbConnectionType.Name = "cmbConnectionType";
            this.cmbConnectionType.Size = new System.Drawing.Size(119, 22);
            this.cmbConnectionType.TabIndex = 89;
            this.cmbConnectionType.Visible = false;
            // 
            // lblConnection
            // 
            this.lblConnection.Location = new System.Drawing.Point(4, 219);
            this.lblConnection.Name = "lblConnection";
            this.lblConnection.Size = new System.Drawing.Size(77, 20);
            this.lblConnection.Text = "Conn. Type";
            this.lblConnection.Visible = false;
            // 
            // radioYard
            // 
            this.radioYard.Location = new System.Drawing.Point(184, 9);
            this.radioYard.Name = "radioYard";
            this.radioYard.Size = new System.Drawing.Size(56, 20);
            this.radioYard.TabIndex = 100;
            this.radioYard.Text = "Yard";
            this.radioYard.Visible = false;
            // 
            // formConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.radioYard);
            this.Controls.Add(this.cmbConnectionType);
            this.Controls.Add(this.lblConnection);
            this.Controls.Add(this.cmbLocation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioVerification);
            this.Controls.Add(this.radioUnRegistration);
            this.Controls.Add(this.radioRegistration);
            this.Controls.Add(this.txtProcess);
            this.Controls.Add(this.txtTaskId);
            this.Controls.Add(this.txtServerPort);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.txtMacAdd);
            this.Controls.Add(this.txtReaderIP);
            this.Controls.Add(this.txtReaderName);
            this.Controls.Add(this.txtReaderNo);
            this.Controls.Add(this.lblProc);
            this.Controls.Add(this.lblTaskNo);
            this.Controls.Add(this.lblServerPort);
            this.Controls.Add(this.lblServerIP);
            this.Controls.Add(this.lblMacAdd);
            this.Controls.Add(this.lblReaderIP);
            this.Controls.Add(this.lblReaderName);
            this.Controls.Add(this.lblReaderNo);
            this.Controls.Add(this.btnRequestConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "formConfig";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.formConfig_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formConfig_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRequestConfig;
        private System.Windows.Forms.Label lblReaderNo;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.Label lblReaderIP;
        private System.Windows.Forms.Label lblMacAdd;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.Label lblServerPort;
        private System.Windows.Forms.Label lblTaskNo;
        private System.Windows.Forms.Label lblProc;
        private System.Windows.Forms.TextBox txtReaderNo;
        private System.Windows.Forms.TextBox txtReaderName;
        private System.Windows.Forms.TextBox txtReaderIP;
        private System.Windows.Forms.TextBox txtMacAdd;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.TextBox txtTaskId;
        private System.Windows.Forms.TextBox txtProcess;
        private System.Windows.Forms.MenuItem menuItemHome;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.RadioButton radioUnRegistration;
        private System.Windows.Forms.RadioButton radioRegistration;
        private System.Windows.Forms.RadioButton radioVerification;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.ComboBox cmbConnectionType;
        private System.Windows.Forms.Label lblConnection;
        private System.Windows.Forms.RadioButton radioYard;
    }
}