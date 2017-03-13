namespace WAP3
{
    partial class formLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtLoginId = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblLoginId = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this._lblVersion = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.lblKPCL = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(101, 101);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(51, 21);
            this.btnLogin.TabIndex = 60;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            this.btnLogin.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnLogin_KeyDown);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.txtPassword.Location = new System.Drawing.Point(101, 76);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(126, 19);
            this.txtPassword.TabIndex = 59;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // txtLoginId
            // 
            this.txtLoginId.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular);
            this.txtLoginId.Location = new System.Drawing.Point(101, 55);
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Size = new System.Drawing.Size(126, 19);
            this.txtLoginId.TabIndex = 58;
            this.txtLoginId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLoginId_KeyDown);
            // 
            // lblPassword
            // 
            this.lblPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.lblPassword.Location = new System.Drawing.Point(3, 76);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(87, 20);
            this.lblPassword.Text = "Password";
            // 
            // lblLoginId
            // 
            this.lblLoginId.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular);
            this.lblLoginId.Location = new System.Drawing.Point(3, 55);
            this.lblLoginId.Name = "lblLoginId";
            this.lblLoginId.Size = new System.Drawing.Size(87, 20);
            this.lblLoginId.Text = "Login Id";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // _lblVersion
            // 
            this._lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._lblVersion.ForeColor = System.Drawing.Color.Black;
            this._lblVersion.Location = new System.Drawing.Point(35, 8);
            this._lblVersion.Name = "_lblVersion";
            this._lblVersion.Size = new System.Drawing.Size(180, 18);
            this._lblVersion.Text = "Krishnapatnam Port Co. Ltd.";
            this._lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(35, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 16);
            this.label1.Text = "EPMS - RFID Application";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Panel1.Location = new System.Drawing.Point(3, 43);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(216, 2);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(176, 101);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(51, 21);
            this.btnExit.TabIndex = 63;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnExit_KeyDown);
            // 
            // lblMsg
            // 
            this.lblMsg.Location = new System.Drawing.Point(12, 125);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(215, 38);
            // 
            // lblKPCL
            // 
            this.lblKPCL.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblKPCL.ForeColor = System.Drawing.Color.Cornsilk;
            this.lblKPCL.Location = new System.Drawing.Point(3, 249);
            this.lblKPCL.Name = "lblKPCL";
            this.lblKPCL.Size = new System.Drawing.Size(234, 16);
            this.lblKPCL.Text = "Krishnapatnam Port Co. Ltd. © 2016";
            this.lblKPCL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblKPCL);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lblVersion);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtLoginId);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblLoginId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "formLogin";
            this.Text = "Login";
            this.Load += new System.EventHandler(this.formLogin_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formLogin_Closing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.formLogin_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtLoginId;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblLoginId;
        internal System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Label _lblVersion;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label lblMsg;
        internal System.Windows.Forms.Label lblKPCL;
    }
}