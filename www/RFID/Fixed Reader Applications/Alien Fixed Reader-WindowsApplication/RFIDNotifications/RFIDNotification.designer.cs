namespace RFIDNotifications
{
    partial class RFIDNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RFIDNotification));
            this.btnAddReader = new System.Windows.Forms.Button();
            this.chkAddDiscovered = new System.Windows.Forms.CheckBox();
            this.lblReadersCount = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chlServers = new System.Windows.Forms.CheckedListBox();
            this.lblConnections = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtTagno = new System.Windows.Forms.TextBox();
            this.txtTruckno = new System.Windows.Forms.TextBox();
            this.txtremarks = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNotifications = new System.Windows.Forms.TextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.lblReaderName = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddReader
            // 
            this.btnAddReader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddReader.BackColor = System.Drawing.Color.MintCream;
            this.btnAddReader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddReader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnAddReader.Location = new System.Drawing.Point(56, 64);
            this.btnAddReader.Name = "btnAddReader";
            this.btnAddReader.Size = new System.Drawing.Size(104, 24);
            this.btnAddReader.TabIndex = 60;
            this.btnAddReader.Text = "&Add Reader ...";
            this.btnAddReader.UseVisualStyleBackColor = false;
            this.btnAddReader.Click += new System.EventHandler(this.btnAddReader_Click);
            // 
            // chkAddDiscovered
            // 
            this.chkAddDiscovered.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAddDiscovered.BackColor = System.Drawing.Color.CadetBlue;
            this.chkAddDiscovered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAddDiscovered.ForeColor = System.Drawing.Color.MidnightBlue;
            this.chkAddDiscovered.Location = new System.Drawing.Point(24, 24);
            this.chkAddDiscovered.Name = "chkAddDiscovered";
            this.chkAddDiscovered.Size = new System.Drawing.Size(176, 32);
            this.chkAddDiscovered.TabIndex = 64;
            this.chkAddDiscovered.Text = "Add all &Discovered Readers";
            this.toolTip1.SetToolTip(this.chkAddDiscovered, "Use default discovery settings \\r\\nand attempt to connect to reader with default " +
                    "settings");
            this.chkAddDiscovered.UseVisualStyleBackColor = false;
            this.chkAddDiscovered.CheckedChanged += new System.EventHandler(this.chkAddDiscovered_CheckedChanged);
            // 
            // lblReadersCount
            // 
            this.lblReadersCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReadersCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblReadersCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReadersCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReadersCount.ForeColor = System.Drawing.Color.Black;
            this.lblReadersCount.Location = new System.Drawing.Point(152, 96);
            this.lblReadersCount.Name = "lblReadersCount";
            this.lblReadersCount.Size = new System.Drawing.Size(48, 16);
            this.lblReadersCount.TabIndex = 61;
            this.lblReadersCount.Text = "0";
            this.lblReadersCount.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.chlServers);
            this.groupBox5.Controls.Add(this.lblConnections);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox5.Location = new System.Drawing.Point(633, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(216, 128);
            this.groupBox5.TabIndex = 66;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = " Servers: ";
            // 
            // chlServers
            // 
            this.chlServers.CheckOnClick = true;
            this.chlServers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chlServers.Location = new System.Drawing.Point(16, 24);
            this.chlServers.Name = "chlServers";
            this.chlServers.Size = new System.Drawing.Size(184, 52);
            this.chlServers.TabIndex = 67;
            this.toolTip1.SetToolTip(this.chlServers, "Check / Uncheck to Start / Stop Listening");
            this.chlServers.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chlServers_ItemCheck);
            // 
            // lblConnections
            // 
            this.lblConnections.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblConnections.BackColor = System.Drawing.SystemColors.Control;
            this.lblConnections.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblConnections.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnections.ForeColor = System.Drawing.Color.Black;
            this.lblConnections.Location = new System.Drawing.Point(152, 96);
            this.lblConnections.Name = "lblConnections";
            this.lblConnections.Size = new System.Drawing.Size(48, 16);
            this.lblConnections.TabIndex = 65;
            this.lblConnections.Text = "0";
            this.lblConnections.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.BackColor = System.Drawing.Color.CadetBlue;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(24, 96);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 24);
            this.label5.TabIndex = 66;
            this.label5.Text = "Current Connections Count: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.CheckBoxes = true;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(16, 128);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(184, 91);
            this.treeView1.TabIndex = 66;
            this.toolTip1.SetToolTip(this.treeView1, "Check reader to configure.");
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // txtTagno
            // 
            this.txtTagno.Location = new System.Drawing.Point(142, -1);
            this.txtTagno.Name = "txtTagno";
            this.txtTagno.Size = new System.Drawing.Size(100, 22);
            this.txtTagno.TabIndex = 63;
            this.txtTagno.Visible = false;
            // 
            // txtTruckno
            // 
            this.txtTruckno.Location = new System.Drawing.Point(142, 2);
            this.txtTruckno.Name = "txtTruckno";
            this.txtTruckno.Size = new System.Drawing.Size(100, 22);
            this.txtTruckno.TabIndex = 64;
            this.txtTruckno.Visible = false;
            // 
            // txtremarks
            // 
            this.txtremarks.Location = new System.Drawing.Point(142, 0);
            this.txtremarks.Name = "txtremarks";
            this.txtremarks.Size = new System.Drawing.Size(100, 22);
            this.txtremarks.TabIndex = 65;
            this.txtremarks.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Current Active Readers:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.btnAddReader);
            this.groupBox1.Controls.Add(this.chkAddDiscovered);
            this.groupBox1.Controls.Add(this.lblReadersCount);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox1.Location = new System.Drawing.Point(633, 158);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 234);
            this.groupBox1.TabIndex = 68;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clients: ";
            // 
            // txtNotifications
            // 
            this.txtNotifications.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotifications.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txtNotifications.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotifications.ForeColor = System.Drawing.Color.Black;
            this.txtNotifications.Location = new System.Drawing.Point(16, 24);
            this.txtNotifications.Multiline = true;
            this.txtNotifications.Name = "txtNotifications";
            this.txtNotifications.ReadOnly = true;
            this.txtNotifications.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotifications.Size = new System.Drawing.Size(584, 331);
            this.txtNotifications.TabIndex = 36;
            // 
            // groupBox9
            // 
            this.groupBox9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox9.Controls.Add(this.txtremarks);
            this.groupBox9.Controls.Add(this.txtTruckno);
            this.groupBox9.Controls.Add(this.txtTagno);
            this.groupBox9.Controls.Add(this.lblReaderName);
            this.groupBox9.Controls.Add(this.btnClear);
            this.groupBox9.Controls.Add(this.txtNotifications);
            this.groupBox9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox9.ForeColor = System.Drawing.Color.MidnightBlue;
            this.groupBox9.Location = new System.Drawing.Point(9, 22);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(608, 370);
            this.groupBox9.TabIndex = 67;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Last Notification from:  ";
            // 
            // lblReaderName
            // 
            this.lblReaderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderName.ForeColor = System.Drawing.Color.Lime;
            this.lblReaderName.Location = new System.Drawing.Point(152, 2);
            this.lblReaderName.Name = "lblReaderName";
            this.lblReaderName.Size = new System.Drawing.Size(160, 16);
            this.lblReaderName.TabIndex = 62;
            this.lblReaderName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.BackColor = System.Drawing.Color.MintCream;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnClear.Location = new System.Drawing.Point(311, -1);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(56, 24);
            this.btnClear.TabIndex = 61;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 0;
            // 
            // RFIDNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(859, 404);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox9);
            this.ForeColor = System.Drawing.Color.Orange;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RFIDNotification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFIDNotification";
            this.Load += new System.EventHandler(this.RFIDNotification_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RFIDNotification_FormClosing);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddReader;
        private System.Windows.Forms.CheckBox chkAddDiscovered;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblReadersCount;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckedListBox chlServers;
        private System.Windows.Forms.Label lblConnections;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox txtTagno;
        private System.Windows.Forms.TextBox txtTruckno;
        private System.Windows.Forms.TextBox txtremarks;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtNotifications;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label lblReaderName;
        private System.Windows.Forms.Button btnClear;
    }
}