namespace WAP3
{
    partial class formSyncMasterData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formSyncMasterData));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.cmbSyncData = new System.Windows.Forms.ComboBox();
            this.lblSyncData = new System.Windows.Forms.Label();
            this.btnRequestSync = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this._lblVersion = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "Back";
            this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "Logout";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // cmbSyncData
            // 
            this.cmbSyncData.Items.Add("----Select----");
            this.cmbSyncData.Items.Add("User Master");
            this.cmbSyncData.Items.Add("Loader Master");
            this.cmbSyncData.Items.Add("Truck Master");
            this.cmbSyncData.Location = new System.Drawing.Point(74, 53);
            this.cmbSyncData.Name = "cmbSyncData";
            this.cmbSyncData.Size = new System.Drawing.Size(151, 22);
            this.cmbSyncData.TabIndex = 5;
            this.cmbSyncData.SelectedIndexChanged += new System.EventHandler(this.cmbSyncData_SelectedIndexChanged);
            // 
            // lblSyncData
            // 
            this.lblSyncData.Location = new System.Drawing.Point(2, 56);
            this.lblSyncData.Name = "lblSyncData";
            this.lblSyncData.Size = new System.Drawing.Size(72, 20);
            this.lblSyncData.Text = "Sync Data";
            // 
            // btnRequestSync
            // 
            this.btnRequestSync.BackColor = System.Drawing.Color.White;
            this.btnRequestSync.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.btnRequestSync.Location = new System.Drawing.Point(155, 81);
            this.btnRequestSync.Name = "btnRequestSync";
            this.btnRequestSync.Size = new System.Drawing.Size(70, 25);
            this.btnRequestSync.TabIndex = 7;
            this.btnRequestSync.Text = "Request";
            this.btnRequestSync.Click += new System.EventHandler(this.btnRequestSync_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Location = new System.Drawing.Point(3, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(216, 2);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(35, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 16);
            this.label2.Text = "EPMS - RFID Application";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _lblVersion
            // 
            this._lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._lblVersion.ForeColor = System.Drawing.Color.Black;
            this._lblVersion.Location = new System.Drawing.Point(35, 0);
            this._lblVersion.Name = "_lblVersion";
            this._lblVersion.Size = new System.Drawing.Size(180, 18);
            this._lblVersion.Text = "Krishnapatnam Port Co. Ltd.";
            this._lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, -5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 35);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.Firebrick;
            this.lblStatus.Location = new System.Drawing.Point(3, 129);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(234, 20);
            // 
            // formSyncMasterData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._lblVersion);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.btnRequestSync);
            this.Controls.Add(this.cmbSyncData);
            this.Controls.Add(this.lblSyncData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "formSyncMasterData";
            this.Text = "Sync MasterData";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formSyncMasterData_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSyncData;
        private System.Windows.Forms.Label lblSyncData;
        private System.Windows.Forms.Button btnRequestSync;
        internal System.Windows.Forms.Panel panel5;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label _lblVersion;
        internal System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
    }
}