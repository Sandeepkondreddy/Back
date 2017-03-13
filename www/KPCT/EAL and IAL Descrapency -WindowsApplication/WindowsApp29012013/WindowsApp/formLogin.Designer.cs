namespace WindowsApp
{
    partial class formLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.lblLoginID = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblPassword = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnLogin = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnExit = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblKPCLHeader = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblSubHead = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.panelLogin = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblLoginHeader = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblCopyright = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.lblMsg = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblVersion = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtPassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtUserID = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelLogin)).BeginInit();
            this.panelLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLoginID
            // 
            this.lblLoginID.Location = new System.Drawing.Point(146, 109);
            this.lblLoginID.Name = "lblLoginID";
            this.lblLoginID.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblLoginID.Size = new System.Drawing.Size(71, 19);
            this.lblLoginID.TabIndex = 0;
            this.lblLoginID.Values.Text = "User Name :";
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(146, 137);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblPassword.Size = new System.Drawing.Size(63, 19);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Values.Text = "Password :";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(259, 162);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnLogin.Size = new System.Drawing.Size(72, 25);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Values.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(343, 162);
            this.btnExit.Name = "btnExit";
            this.btnExit.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnExit.Size = new System.Drawing.Size(72, 25);
            this.btnExit.TabIndex = 5;
            this.btnExit.Values.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblKPCLHeader
            // 
            this.lblKPCLHeader.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.TitleControl;
            this.lblKPCLHeader.Location = new System.Drawing.Point(73, 8);
            this.lblKPCLHeader.Name = "lblKPCLHeader";
            this.lblKPCLHeader.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblKPCLHeader.Size = new System.Drawing.Size(340, 27);
            this.lblKPCLHeader.StateCommon.LongText.Font = new System.Drawing.Font("Tw Cen MT Condensed Extra Bold", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblKPCLHeader.StateNormal.ShortText.Color1 = System.Drawing.SystemColors.WindowFrame;
            this.lblKPCLHeader.TabIndex = 7;
            this.lblKPCLHeader.Values.Text = "Krishnapatnam Port Container Terminal";
            // 
            // lblSubHead
            // 
            this.lblSubHead.Location = new System.Drawing.Point(252, 33);
            this.lblSubHead.Name = "lblSubHead";
            this.lblSubHead.Size = new System.Drawing.Size(195, 19);
            this.lblSubHead.StateNormal.ShortText.Color1 = System.Drawing.Color.White;
            this.lblSubHead.TabIndex = 8;
            this.lblSubHead.Values.Text = "A  port ready to reackon the future ...";
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.lblLoginHeader);
            this.panelLogin.Location = new System.Drawing.Point(0, 67);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(582, 27);
            this.panelLogin.StateNormal.Color1 = System.Drawing.Color.LightGray;
            this.panelLogin.TabIndex = 9;
            // 
            // lblLoginHeader
            // 
            this.lblLoginHeader.Location = new System.Drawing.Point(242, 4);
            this.lblLoginHeader.Name = "lblLoginHeader";
            this.lblLoginHeader.Size = new System.Drawing.Size(89, 18);
            this.lblLoginHeader.StateNormal.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoginHeader.TabIndex = 0;
            this.lblLoginHeader.Values.Text = "USER LOGIN";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(114, 226);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(339, 19);
            this.lblCopyright.StateNormal.ShortText.Color1 = System.Drawing.Color.White;
            this.lblCopyright.TabIndex = 10;
            this.lblCopyright.Values.Text = "© 2011 Krishnapatnam Port Company Limited, All rights reserved.";
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.lblMsg);
            this.kryptonPanel.Controls.Add(this.lblVersion);
            this.kryptonPanel.Controls.Add(this.lblCopyright);
            this.kryptonPanel.Controls.Add(this.panelLogin);
            this.kryptonPanel.Controls.Add(this.lblSubHead);
            this.kryptonPanel.Controls.Add(this.lblKPCLHeader);
            this.kryptonPanel.Controls.Add(this.pictureBox1);
            this.kryptonPanel.Controls.Add(this.btnExit);
            this.kryptonPanel.Controls.Add(this.btnLogin);
            this.kryptonPanel.Controls.Add(this.txtPassword);
            this.kryptonPanel.Controls.Add(this.txtUserID);
            this.kryptonPanel.Controls.Add(this.lblPassword);
            this.kryptonPanel.Controls.Add(this.lblLoginID);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel.Size = new System.Drawing.Size(582, 244);
            this.kryptonPanel.TabIndex = 0;
            // 
            // lblMsg
            // 
            this.lblMsg.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.NormalPanel;
            this.lblMsg.Location = new System.Drawing.Point(146, 196);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.lblMsg.Size = new System.Drawing.Size(324, 18);
            this.lblMsg.StateNormal.ShortText.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.lblMsg.StateNormal.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.TabIndex = 12;
            this.lblMsg.Values.Text = "Database Disconnected. Please Contact Administrator.";
            this.lblMsg.Visible = false;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(540, 226);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(33, 19);
            this.lblVersion.StateNormal.ShortText.Color1 = System.Drawing.Color.White;
            this.lblVersion.TabIndex = 11;
            this.lblVersion.Values.Text = "v 1.0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::WindowsApp.Properties.Resources.kpcl_Logo;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 58);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(259, 134);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(156, 22);
            this.txtPassword.TabIndex = 3;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(259, 106);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.txtUserID.Size = new System.Drawing.Size(156, 22);
            this.txtUserID.TabIndex = 2;
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 244);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formLogin";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.formLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelLogin)).EndInit();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblLoginID;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnLogin;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnExit;
        private System.Windows.Forms.PictureBox pictureBox1;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblKPCLHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblSubHead;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel panelLogin;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblLoginHeader;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblCopyright;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPassword;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtUserID;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblVersion;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblMsg;
    }
}

