namespace WindowsApp
{
    partial class formChangePassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formChangePassword));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.cmbUserId = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.btnClear = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnSave = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtNewPassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblNewPassword = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtOldPassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblOldPassword = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblLoginID = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserId)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.cmbUserId);
            this.kryptonPanel.Controls.Add(this.btnClear);
            this.kryptonPanel.Controls.Add(this.btnSave);
            this.kryptonPanel.Controls.Add(this.txtNewPassword);
            this.kryptonPanel.Controls.Add(this.lblNewPassword);
            this.kryptonPanel.Controls.Add(this.txtOldPassword);
            this.kryptonPanel.Controls.Add(this.lblOldPassword);
            this.kryptonPanel.Controls.Add(this.lblLoginID);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel.TabIndex = 0;
            // 
            // cmbUserId
            // 
            this.cmbUserId.DropDownWidth = 156;
            this.cmbUserId.Location = new System.Drawing.Point(198, 61);
            this.cmbUserId.Name = "cmbUserId";
            this.cmbUserId.Size = new System.Drawing.Size(156, 22);
            this.cmbUserId.TabIndex = 26;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(282, 145);
            this.btnClear.Name = "btnClear";
            this.btnClear.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnClear.Size = new System.Drawing.Size(72, 25);
            this.btnClear.TabIndex = 25;
            this.btnClear.Values.Text = "Clear";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(198, 145);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnSave.Size = new System.Drawing.Size(72, 25);
            this.btnSave.TabIndex = 24;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(198, 117);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(156, 22);
            this.txtNewPassword.TabIndex = 23;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.Location = new System.Drawing.Point(85, 117);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblNewPassword.Size = new System.Drawing.Size(89, 19);
            this.lblNewPassword.TabIndex = 22;
            this.lblNewPassword.Values.Text = "New Password :";
            // 
            // txtOldPassword
            // 
            this.txtOldPassword.Location = new System.Drawing.Point(198, 89);
            this.txtOldPassword.Name = "txtOldPassword";
            this.txtOldPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtOldPassword.PasswordChar = '*';
            this.txtOldPassword.Size = new System.Drawing.Size(156, 22);
            this.txtOldPassword.TabIndex = 21;
            // 
            // lblOldPassword
            // 
            this.lblOldPassword.Location = new System.Drawing.Point(85, 92);
            this.lblOldPassword.Name = "lblOldPassword";
            this.lblOldPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblOldPassword.Size = new System.Drawing.Size(84, 19);
            this.lblOldPassword.TabIndex = 19;
            this.lblOldPassword.Values.Text = "Old Password :";
            // 
            // lblLoginID
            // 
            this.lblLoginID.Location = new System.Drawing.Point(85, 64);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblLoginID.Size = new System.Drawing.Size(56, 19);
            this.lblLoginID.TabIndex = 18;
            this.lblLoginID.Values.Text = "Login Id :";
            // 
            // formChangePassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formChangePassword";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.Text = "Change Password";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formChangePassword_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbUserId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnClear;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnSave;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtNewPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblNewPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtOldPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblOldPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblLoginID;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cmbUserId;
    }
}

