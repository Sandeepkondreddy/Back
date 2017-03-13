namespace KPCLVisitor
{
    partial class formSQL_DBConnection
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
            this.txtDataSource = new System.Windows.Forms.TextBox();
            this.lblServerIP = new System.Windows.Forms.Label();
            this.txtInitialCatalog = new System.Windows.Forms.TextBox();
            this.lblInitialCatalog = new System.Windows.Forms.Label();
            this.txtLoginId = new System.Windows.Forms.TextBox();
            this.lblLoginId = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this._lbDriverStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            // 
            // txtDataSource
            // 
            this.txtDataSource.Location = new System.Drawing.Point(98, 43);
            this.txtDataSource.Name = "txtDataSource";
            this.txtDataSource.Size = new System.Drawing.Size(126, 21);
            this.txtDataSource.TabIndex = 0;
            // 
            // lblServerIP
            // 
            this.lblServerIP.Location = new System.Drawing.Point(9, 46);
            this.lblServerIP.Name = "lblServerIP";
            this.lblServerIP.Size = new System.Drawing.Size(77, 20);
            this.lblServerIP.Text = "SQL ServerIP";
            // 
            // txtInitialCatalog
            // 
            this.txtInitialCatalog.Location = new System.Drawing.Point(98, 70);
            this.txtInitialCatalog.Name = "txtInitialCatalog";
            this.txtInitialCatalog.Size = new System.Drawing.Size(126, 21);
            this.txtInitialCatalog.TabIndex = 1;
            // 
            // lblInitialCatalog
            // 
            this.lblInitialCatalog.Location = new System.Drawing.Point(9, 73);
            this.lblInitialCatalog.Name = "lblInitialCatalog";
            this.lblInitialCatalog.Size = new System.Drawing.Size(87, 20);
            this.lblInitialCatalog.Text = "Initial Catalog";
            // 
            // txtLoginId
            // 
            this.txtLoginId.Location = new System.Drawing.Point(98, 97);
            this.txtLoginId.Name = "txtLoginId";
            this.txtLoginId.Size = new System.Drawing.Size(126, 21);
            this.txtLoginId.TabIndex = 2;
            // 
            // lblLoginId
            // 
            this.lblLoginId.Location = new System.Drawing.Point(9, 100);
            this.lblLoginId.Name = "lblLoginId";
            this.lblLoginId.Size = new System.Drawing.Size(87, 20);
            this.lblLoginId.Text = "Login Id";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(98, 123);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(126, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(9, 126);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(87, 20);
            this.lblPassword.Text = "Password";
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.White;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.btnSubmit.Location = new System.Drawing.Point(154, 150);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(70, 25);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // _lbDriverStatus
            // 
            this._lbDriverStatus.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._lbDriverStatus.ForeColor = System.Drawing.Color.Maroon;
            this._lbDriverStatus.Location = new System.Drawing.Point(6, 8);
            this._lbDriverStatus.Name = "_lbDriverStatus";
            this._lbDriverStatus.Size = new System.Drawing.Size(228, 17);
            this._lbDriverStatus.Text = "SQL2-DB Connection Details";
            this._lbDriverStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(3, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Back";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // formSQL_DBConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this._lbDriverStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtLoginId);
            this.Controls.Add(this.lblLoginId);
            this.Controls.Add(this.txtInitialCatalog);
            this.Controls.Add(this.lblInitialCatalog);
            this.Controls.Add(this.txtDataSource);
            this.Controls.Add(this.lblServerIP);
            this.Menu = this.mainMenu1;
            this.Name = "formSQL_DBConnection";
            this.Text = "SQL Connection";
            this.Load += new System.EventHandler(this.formSQL_DBConnection_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDataSource;
        private System.Windows.Forms.Label lblServerIP;
        private System.Windows.Forms.TextBox txtInitialCatalog;
        private System.Windows.Forms.Label lblInitialCatalog;
        private System.Windows.Forms.TextBox txtLoginId;
        private System.Windows.Forms.Label lblLoginId;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label _lbDriverStatus;
        internal System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.MenuItem menuItem1;
    }
}