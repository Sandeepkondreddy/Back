namespace WAP3
{
    partial class formConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formConnection));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            this._bt = new System.Windows.Forms.Button();
            this._imageList = new System.Windows.Forms.ImageList();
            this._lv = new System.Windows.Forms.ListView();
            this.Panel1 = new System.Windows.Forms.Panel();
            this._lblVersion = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this._lbDriverStatus = new System.Windows.Forms.Label();
            this._btDriverInfos = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _bt
            // 
            this._bt.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this._bt.Location = new System.Drawing.Point(93, 81);
            this._bt.Name = "_bt";
            this._bt.Size = new System.Drawing.Size(134, 31);
            this._bt.TabIndex = 2;
            this._bt.Text = "Connection";
            this._bt.Click += new System.EventHandler(this._bt_Click);
            this._imageList.Images.Clear();
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource"))));
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource1"))));
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource2"))));
            this._imageList.Images.Add(((System.Drawing.Icon)(resources.GetObject("resource3"))));
            // 
            // _lv
            // 
            this._lv.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            listViewItem1.ImageIndex = 1;
            listViewItem1.Text = "";
            this._lv.Items.Add(listViewItem1);
            this._lv.Location = new System.Drawing.Point(4, 118);
            this._lv.Name = "_lv";
            this._lv.Size = new System.Drawing.Size(228, 120);
            this._lv.SmallImageList = this._imageList;
            this._lv.TabIndex = 9;
            this._lv.View = System.Windows.Forms.View.SmallIcon;
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Panel1.Location = new System.Drawing.Point(8, 46);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(216, 2);
            // 
            // _lblVersion
            // 
            this._lblVersion.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this._lblVersion.ForeColor = System.Drawing.Color.DimGray;
            this._lblVersion.Location = new System.Drawing.Point(16, 21);
            this._lblVersion.Name = "_lblVersion";
            this._lblVersion.Size = new System.Drawing.Size(208, 23);
            this._lblVersion.Text = "Version: 2.3";
            this._lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Label3
            // 
            this.Label3.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold);
            this.Label3.ForeColor = System.Drawing.Color.Black;
            this.Label3.Location = new System.Drawing.Point(8, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(224, 24);
            this.Label3.Text = "KPCL RFID APP";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Panel2.Location = new System.Drawing.Point(11, 73);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(216, 2);
            // 
            // _lbDriverStatus
            // 
            this._lbDriverStatus.Location = new System.Drawing.Point(4, 51);
            this._lbDriverStatus.Name = "_lbDriverStatus";
            this._lbDriverStatus.Size = new System.Drawing.Size(228, 23);
            this._lbDriverStatus.Text = "driver status";
            this._lbDriverStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // _btDriverInfos
            // 
            this._btDriverInfos.Location = new System.Drawing.Point(8, 81);
            this._btDriverInfos.Name = "_btDriverInfos";
            this._btDriverInfos.Size = new System.Drawing.Size(79, 31);
            this._btDriverInfos.TabIndex = 0;
            this._btDriverInfos.Text = "Driver Infos";
            this._btDriverInfos.Click += new System.EventHandler(this._btDriverInfos_Click);
            // 
            // formConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this._btDriverInfos);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this._lblVersion);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this._lv);
            this.Controls.Add(this._bt);
            this.Controls.Add(this._lbDriverStatus);
            this.MinimizeBox = false;
            this.Name = "formConnection";
            this.Text = "Connection";
            this.Load += new System.EventHandler(this.formConnection_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.formConnection_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _bt;
        private System.Windows.Forms.ImageList _imageList;
        private System.Windows.Forms.ListView _lv;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label _lblVersion;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.Label _lbDriverStatus;
        internal System.Windows.Forms.Button _btDriverInfos;
    }
}