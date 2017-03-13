namespace WindowsApp
{
    partial class formHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formHome));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DiscrepancyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityPermitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iALDiscrepancyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.billableEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mastersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ChangePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogouttoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSubStaffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eALToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 594);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(842, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkGray;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DiscrepancyToolStripMenuItem,
            this.toolStripMenuItem1,
            this.mastersToolStripMenuItem,
            this.LogouttoolStripMenuItem,
            this.addSubStaffToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(842, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DiscrepancyToolStripMenuItem
            // 
            this.DiscrepancyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.securityPermitToolStripMenuItem,
            this.iALDiscrepancyToolStripMenuItem,
            this.eALToolStripMenuItem});
            this.DiscrepancyToolStripMenuItem.Name = "DiscrepancyToolStripMenuItem";
            this.DiscrepancyToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.DiscrepancyToolStripMenuItem.Text = "&Discrepancy";
            this.DiscrepancyToolStripMenuItem.MouseHover += new System.EventHandler(this.DiscrepancyToolStripMenuItem_MouseHover);
            // 
            // securityPermitToolStripMenuItem
            // 
            this.securityPermitToolStripMenuItem.Name = "securityPermitToolStripMenuItem";
            this.securityPermitToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.securityPermitToolStripMenuItem.Text = "EAL Discrepancy";
            this.securityPermitToolStripMenuItem.Click += new System.EventHandler(this.securityPermitToolStripMenuItem_Click);
            // 
            // iALDiscrepancyToolStripMenuItem
            // 
            this.iALDiscrepancyToolStripMenuItem.Name = "iALDiscrepancyToolStripMenuItem";
            this.iALDiscrepancyToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.iALDiscrepancyToolStripMenuItem.Text = "IAL Discrepancy";
            this.iALDiscrepancyToolStripMenuItem.Click += new System.EventHandler(this.iALDiscrepancyToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.billableEventsToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(59, 20);
            this.toolStripMenuItem1.Text = "Reports";
            // 
            // billableEventsToolStripMenuItem
            // 
            this.billableEventsToolStripMenuItem.Name = "billableEventsToolStripMenuItem";
            this.billableEventsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.billableEventsToolStripMenuItem.Text = "Billable Events Report";
            this.billableEventsToolStripMenuItem.Click += new System.EventHandler(this.billableEventsToolStripMenuItem_Click);
            // 
            // mastersToolStripMenuItem
            // 
            this.mastersToolStripMenuItem.DoubleClickEnabled = true;
            this.mastersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createUserToolStripMenuItem,
            this.ChangePasswordToolStripMenuItem});
            this.mastersToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mastersToolStripMenuItem.Name = "mastersToolStripMenuItem";
            this.mastersToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.mastersToolStripMenuItem.Text = "&User Management";
            // 
            // createUserToolStripMenuItem
            // 
            this.createUserToolStripMenuItem.Name = "createUserToolStripMenuItem";
            this.createUserToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.createUserToolStripMenuItem.Text = "Create User";
            this.createUserToolStripMenuItem.Click += new System.EventHandler(this.createUserToolStripMenuItem_Click);
            // 
            // ChangePasswordToolStripMenuItem
            // 
            this.ChangePasswordToolStripMenuItem.Name = "ChangePasswordToolStripMenuItem";
            this.ChangePasswordToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.ChangePasswordToolStripMenuItem.Text = "Change Password";
            this.ChangePasswordToolStripMenuItem.Click += new System.EventHandler(this.ChangePasswordToolStripMenuItem_Click);
            // 
            // LogouttoolStripMenuItem
            // 
            this.LogouttoolStripMenuItem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogouttoolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.LogouttoolStripMenuItem.Name = "LogouttoolStripMenuItem";
            this.LogouttoolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.LogouttoolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.LogouttoolStripMenuItem.Text = "&Log Out";
            this.LogouttoolStripMenuItem.Click += new System.EventHandler(this.LogouttoolStripMenuItem_Click);
            // 
            // addSubStaffToolStripMenuItem
            // 
            this.addSubStaffToolStripMenuItem.Name = "addSubStaffToolStripMenuItem";
            this.addSubStaffToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.addSubStaffToolStripMenuItem.Text = "Add Sub Staff";
            this.addSubStaffToolStripMenuItem.Click += new System.EventHandler(this.addSubStaffToolStripMenuItem_Click);
            // 
            // eALToolStripMenuItem
            // 
            this.eALToolStripMenuItem.Name = "eALToolStripMenuItem";
            this.eALToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.eALToolStripMenuItem.Text = "EAL Old";
            this.eALToolStripMenuItem.Click += new System.EventHandler(this.eALToolStripMenuItem_Click);
            // 
            // formHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "formHome";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.Load += new System.EventHandler(this.formHome_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.formHome_FormClosed);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DiscrepancyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityPermitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mastersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LogouttoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iALDiscrepancyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ChangePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem billableEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSubStaffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eALToolStripMenuItem;
    }
}



