namespace WindowsApp
{
    partial class formAddUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formAddUser));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.gdvUserDetails = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.EMPNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMPName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoginID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.kryptonComboBox1 = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.lblEMPStatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtEMPName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblEMPName = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtEMPNo = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblEMPNo = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtRetypePassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblRetypePassword = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtPassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtUserID = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblPassword = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblLoginID = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvUserDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.gdvUserDetails);
            this.kryptonPanel.Controls.Add(this.btnClear);
            this.kryptonPanel.Controls.Add(this.btnSave);
            this.kryptonPanel.Controls.Add(this.kryptonComboBox1);
            this.kryptonPanel.Controls.Add(this.lblEMPStatus);
            this.kryptonPanel.Controls.Add(this.txtEMPName);
            this.kryptonPanel.Controls.Add(this.lblEMPName);
            this.kryptonPanel.Controls.Add(this.txtEMPNo);
            this.kryptonPanel.Controls.Add(this.lblEMPNo);
            this.kryptonPanel.Controls.Add(this.txtRetypePassword);
            this.kryptonPanel.Controls.Add(this.lblRetypePassword);
            this.kryptonPanel.Controls.Add(this.txtPassword);
            this.kryptonPanel.Controls.Add(this.txtUserID);
            this.kryptonPanel.Controls.Add(this.lblPassword);
            this.kryptonPanel.Controls.Add(this.lblLoginID);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel.TabIndex = 0;
            this.kryptonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonPanel_Paint);
            // 
            // gdvUserDetails
            // 
            this.gdvUserDetails.AllowUserToAddRows = false;
            this.gdvUserDetails.AllowUserToDeleteRows = false;
            this.gdvUserDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gdvUserDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EMPNO,
            this.EMPName,
            this.LoginID,
            this.Status});
            this.gdvUserDetails.Location = new System.Drawing.Point(122, 173);
            this.gdvUserDetails.Name = "gdvUserDetails";
            this.gdvUserDetails.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.gdvUserDetails.ReadOnly = true;
            this.gdvUserDetails.Size = new System.Drawing.Size(580, 247);
            this.gdvUserDetails.TabIndex = 18;
            this.gdvUserDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvUserDetails_CellContentClick);
            // 
            // EMPNO
            // 
            this.EMPNO.DataPropertyName = "Emp_No";
            this.EMPNO.HeaderText = "EMP No";
            this.EMPNO.Name = "EMPNO";
            this.EMPNO.ReadOnly = true;
            // 
            // EMPName
            // 
            this.EMPName.DataPropertyName = "Emp_Name";
            this.EMPName.HeaderText = "User Name";
            this.EMPName.Name = "EMPName";
            this.EMPName.ReadOnly = true;
            // 
            // LoginID
            // 
            this.LoginID.DataPropertyName = "User_Name";
            this.LoginID.HeaderText = "Login Id";
            this.LoginID.Name = "LoginID";
            this.LoginID.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(630, 131);
            this.btnClear.Name = "btnClear";
            this.btnClear.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnClear.Size = new System.Drawing.Size(72, 25);
            this.btnClear.TabIndex = 17;
            this.btnClear.Values.Text = "Clear";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(546, 131);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnSave.Size = new System.Drawing.Size(72, 25);
            this.btnSave.TabIndex = 16;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // kryptonComboBox1
            // 
            this.kryptonComboBox1.DropDownWidth = 156;
            this.kryptonComboBox1.Items.AddRange(new object[] {
            "Active",
            "Inactive"});
            this.kryptonComboBox1.Location = new System.Drawing.Point(226, 103);
            this.kryptonComboBox1.Name = "kryptonComboBox1";
            this.kryptonComboBox1.Size = new System.Drawing.Size(156, 21);
            this.kryptonComboBox1.TabIndex = 15;
            this.kryptonComboBox1.Text = "Active";
            // 
            // lblEMPStatus
            // 
            this.lblEMPStatus.Location = new System.Drawing.Point(122, 103);
            this.lblEMPStatus.Name = "lblEMPStatus";
            this.lblEMPStatus.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblEMPStatus.Size = new System.Drawing.Size(78, 20);
            this.lblEMPStatus.TabIndex = 14;
            this.lblEMPStatus.Values.Text = "EMP Status :";
            // 
            // txtEMPName
            // 
            this.txtEMPName.Location = new System.Drawing.Point(226, 75);
            this.txtEMPName.Name = "txtEMPName";
            this.txtEMPName.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtEMPName.Size = new System.Drawing.Size(156, 20);
            this.txtEMPName.TabIndex = 13;
            // 
            // lblEMPName
            // 
            this.lblEMPName.Location = new System.Drawing.Point(122, 78);
            this.lblEMPName.Name = "lblEMPName";
            this.lblEMPName.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblEMPName.Size = new System.Drawing.Size(77, 20);
            this.lblEMPName.TabIndex = 12;
            this.lblEMPName.Values.Text = "EMP Name :";
            // 
            // txtEMPNo
            // 
            this.txtEMPNo.Location = new System.Drawing.Point(226, 47);
            this.txtEMPNo.Name = "txtEMPNo";
            this.txtEMPNo.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtEMPNo.Size = new System.Drawing.Size(156, 20);
            this.txtEMPNo.TabIndex = 11;
            // 
            // lblEMPNo
            // 
            this.lblEMPNo.Location = new System.Drawing.Point(122, 50);
            this.lblEMPNo.Name = "lblEMPNo";
            this.lblEMPNo.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblEMPNo.Size = new System.Drawing.Size(61, 20);
            this.lblEMPNo.TabIndex = 10;
            this.lblEMPNo.Values.Text = "EMP No :";
            // 
            // txtRetypePassword
            // 
            this.txtRetypePassword.Location = new System.Drawing.Point(546, 103);
            this.txtRetypePassword.Name = "txtRetypePassword";
            this.txtRetypePassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtRetypePassword.PasswordChar = '*';
            this.txtRetypePassword.Size = new System.Drawing.Size(156, 20);
            this.txtRetypePassword.TabIndex = 9;
            // 
            // lblRetypePassword
            // 
            this.lblRetypePassword.Location = new System.Drawing.Point(433, 103);
            this.lblRetypePassword.Name = "lblRetypePassword";
            this.lblRetypePassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblRetypePassword.Size = new System.Drawing.Size(115, 20);
            this.lblRetypePassword.TabIndex = 8;
            this.lblRetypePassword.Values.Text = "Confirm Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(546, 75);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(156, 20);
            this.txtPassword.TabIndex = 7;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(546, 47);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtUserID.Size = new System.Drawing.Size(156, 20);
            this.txtUserID.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(433, 78);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblPassword.Size = new System.Drawing.Size(68, 20);
            this.lblPassword.TabIndex = 5;
            this.lblPassword.Values.Text = "Password :";
            // 
            // lblLoginID
            // 
            this.lblLoginID.Location = new System.Drawing.Point(433, 50);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblLoginID.Size = new System.Drawing.Size(61, 20);
            this.lblLoginID.TabIndex = 4;
            this.lblLoginID.Values.Text = "Login Id :";
            // 
            // formAddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formAddUser";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add User";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formAddUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvUserDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonComboBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtUserID;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblLoginID;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblRetypePassword;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtRetypePassword;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEMPNo;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEMPNo;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEMPName;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEMPName;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEMPStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox kryptonComboBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView gdvUserDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMPNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoginID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}

