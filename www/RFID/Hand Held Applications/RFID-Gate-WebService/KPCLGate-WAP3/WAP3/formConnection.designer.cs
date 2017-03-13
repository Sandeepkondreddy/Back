namespace WAP3
{
    partial class formConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formConnection));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblHdr = new System.Windows.Forms.Label();
            this._lblVersion = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lbDriverStatus = new System.Windows.Forms.Label();
            this.btnDriverInfo = new System.Windows.Forms.Button();
            this.btnConnection = new System.Windows.Forms.Button();
            this._lv = new System.Windows.Forms.ListView();
            this.lblKPCL = new System.Windows.Forms.Label();
            this._imageList = new System.Windows.Forms.ImageList();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = " ";
            // 
            // menuItem2
            // 
            this.menuItem2.Text = " ";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(29, 37);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblHdr
            // 
            this.lblHdr.Font = new System.Drawing.Font("Tahoma", 13.25F, System.Drawing.FontStyle.Bold);
            this.lblHdr.ForeColor = System.Drawing.Color.Black;
            this.lblHdr.Location = new System.Drawing.Point(32, 3);
            this.lblHdr.Name = "lblHdr";
            this.lblHdr.Size = new System.Drawing.Size(195, 24);
            this.lblHdr.Text = "KPCL-RFID-WS  APP.";
            // 
            // _lblVersion
            // 
            this._lblVersion.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this._lblVersion.ForeColor = System.Drawing.Color.DimGray;
            this._lblVersion.Location = new System.Drawing.Point(32, 25);
            this._lblVersion.Name = "_lblVersion";
            this._lblVersion.Size = new System.Drawing.Size(161, 15);
            this._lblVersion.Text = "Version: 1.00";
            this._lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Panel1.Location = new System.Drawing.Point(3, 43);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(234, 2);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Location = new System.Drawing.Point(3, 63);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(234, 2);
            // 
            // _lbDriverStatus
            // 
            this._lbDriverStatus.Location = new System.Drawing.Point(6, 48);
            this._lbDriverStatus.Name = "_lbDriverStatus";
            this._lbDriverStatus.Size = new System.Drawing.Size(228, 12);
            this._lbDriverStatus.Text = "Driver Status";
            this._lbDriverStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDriverInfo
            // 
            this.btnDriverInfo.Location = new System.Drawing.Point(9, 75);
            this.btnDriverInfo.Name = "btnDriverInfo";
            this.btnDriverInfo.Size = new System.Drawing.Size(79, 31);
            this.btnDriverInfo.TabIndex = 12;
            this.btnDriverInfo.Text = "Driver Infos";
            this.btnDriverInfo.Click += new System.EventHandler(this.btnDriverInfo_Click);
            // 
            // btnConnection
            // 
            this.btnConnection.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.btnConnection.Location = new System.Drawing.Point(94, 75);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(134, 31);
            this.btnConnection.TabIndex = 13;
            this.btnConnection.Text = "Connection";
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // _lv
            // 
            this._lv.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            listViewItem1.ImageIndex = 1;
            listViewItem1.Text = "";
            this._lv.Items.Add(listViewItem1);
            this._lv.Location = new System.Drawing.Point(3, 115);
            this._lv.Name = "_lv";
            this._lv.Size = new System.Drawing.Size(234, 132);
            this._lv.TabIndex = 14;
            this._lv.View = System.Windows.Forms.View.SmallIcon;
            // 
            // lblKPCL
            // 
            this.lblKPCL.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblKPCL.ForeColor = System.Drawing.Color.Cornsilk;
            this.lblKPCL.Location = new System.Drawing.Point(8, 250);
            this.lblKPCL.Name = "lblKPCL";
            this.lblKPCL.Size = new System.Drawing.Size(208, 16);
            this.lblKPCL.Text = "Krishnapatnam Port Co. Ltd. © 2015";
            this.lblKPCL.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._imageList.Images.Clear();
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource"))));
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource1"))));
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource2"))));
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource3"))));
            // 
            // formConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.lblKPCL);
            this.Controls.Add(this._lv);
            this.Controls.Add(this.btnDriverInfo);
            this.Controls.Add(this.btnConnection);
            this.Controls.Add(this._lbDriverStatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this._lblVersion);
            this.Controls.Add(this.lblHdr);
            this.Controls.Add(this.pictureBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "formConnection";
            this.Text = "WAPG3 Device Connection";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pictureBox2;
        internal System.Windows.Forms.Label lblHdr;
        internal System.Windows.Forms.Label _lblVersion;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.Label _lbDriverStatus;
        internal System.Windows.Forms.Button btnDriverInfo;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.ListView _lv;
        internal System.Windows.Forms.Label lblKPCL;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ImageList _imageList;
    }
}