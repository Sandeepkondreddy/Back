namespace DelegateMgtSystem
{
    partial class MainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pnlTruck = new System.Windows.Forms.Panel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delegateDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delegateEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vPassEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.monitoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delegateMonitoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trackingMonitoringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.delegateTrackingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDataSyncToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTruck
            // 
            this.pnlTruck.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlTruck.BackgroundImage")));
            this.pnlTruck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlTruck.Location = new System.Drawing.Point(1, 26);
            this.pnlTruck.Name = "pnlTruck";
            this.pnlTruck.Size = new System.Drawing.Size(1293, 59);
            this.pnlTruck.TabIndex = 10;
            // 
            // menuStrip
            // 
            this.menuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("menuStrip.BackgroundImage")));
            this.menuStrip.Font = new System.Drawing.Font("Calibri", 10F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoutToolStripMenuItem,
            this.delegateDetailsToolStripMenuItem,
            this.monitoringToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip.Size = new System.Drawing.Size(1284, 25);
            this.menuStrip.TabIndex = 9;
            this.menuStrip.Text = "MenuStrip";
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.logoutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("logoutToolStripMenuItem.Image")));
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(75, 21);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // delegateDetailsToolStripMenuItem
            // 
            this.delegateDetailsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delegateEntryToolStripMenuItem,
            this.vPassEventToolStripMenuItem,
            this.localDataSyncToolStripMenuItem});
            this.delegateDetailsToolStripMenuItem.Name = "delegateDetailsToolStripMenuItem";
            this.delegateDetailsToolStripMenuItem.Size = new System.Drawing.Size(111, 21);
            this.delegateDetailsToolStripMenuItem.Text = "DelegateDetails";
            this.delegateDetailsToolStripMenuItem.Click += new System.EventHandler(this.delegateDetailsToolStripMenuItem_Click);
            // 
            // delegateEntryToolStripMenuItem
            // 
            this.delegateEntryToolStripMenuItem.Name = "delegateEntryToolStripMenuItem";
            this.delegateEntryToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.delegateEntryToolStripMenuItem.Text = "Delegate Entry";
            this.delegateEntryToolStripMenuItem.Click += new System.EventHandler(this.delegateEntryToolStripMenuItem_Click);
            // 
            // vPassEventToolStripMenuItem
            // 
            this.vPassEventToolStripMenuItem.Name = "vPassEventToolStripMenuItem";
            this.vPassEventToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.vPassEventToolStripMenuItem.Text = "VPassEventMapping";
            this.vPassEventToolStripMenuItem.Click += new System.EventHandler(this.vPassEventToolStripMenuItem_Click);
            // 
            // monitoringToolStripMenuItem
            // 
            this.monitoringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.delegateMonitoringToolStripMenuItem,
            this.trackingMonitoringToolStripMenuItem,
            this.delegateTrackingToolStripMenuItem});
            this.monitoringToolStripMenuItem.Name = "monitoringToolStripMenuItem";
            this.monitoringToolStripMenuItem.Size = new System.Drawing.Size(83, 21);
            this.monitoringToolStripMenuItem.Text = "Monitoring";
            // 
            // delegateMonitoringToolStripMenuItem
            // 
            this.delegateMonitoringToolStripMenuItem.Name = "delegateMonitoringToolStripMenuItem";
            this.delegateMonitoringToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.delegateMonitoringToolStripMenuItem.Text = "EventMonitoring";
            this.delegateMonitoringToolStripMenuItem.Click += new System.EventHandler(this.EventMonitoringToolStripMenuItem_Click);
            // 
            // trackingMonitoringToolStripMenuItem
            // 
            this.trackingMonitoringToolStripMenuItem.Name = "trackingMonitoringToolStripMenuItem";
            this.trackingMonitoringToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.trackingMonitoringToolStripMenuItem.Text = "TrackingMonitoring";
            this.trackingMonitoringToolStripMenuItem.Click += new System.EventHandler(this.trackingMonitoringToolStripMenuItem_Click);
            // 
            // delegateTrackingToolStripMenuItem
            // 
            this.delegateTrackingToolStripMenuItem.Name = "delegateTrackingToolStripMenuItem";
            this.delegateTrackingToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.delegateTrackingToolStripMenuItem.Text = "DelegateMonitoring";
            this.delegateTrackingToolStripMenuItem.Click += new System.EventHandler(this.delegateTrackingToolStripMenuItem_Click);
            // 
            // localDataSyncToolStripMenuItem
            // 
            this.localDataSyncToolStripMenuItem.Name = "localDataSyncToolStripMenuItem";
            this.localDataSyncToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.localDataSyncToolStripMenuItem.Text = "LocalDataSync";
            this.localDataSyncToolStripMenuItem.Click += new System.EventHandler(this.localDataSyncToolStripMenuItem_Click);
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(219)))), ((int)(((byte)(219)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1284, 672);
            this.Controls.Add(this.pnlTruck);
            this.Controls.Add(this.menuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 700);
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainPage";
            this.Load += new System.EventHandler(this.MainPage_Load);
            this.Shown += new System.EventHandler(this.MainPage_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainPage_FormClosed);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Panel pnlTruck;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delegateDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delegateEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem monitoringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delegateMonitoringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vPassEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trackingMonitoringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delegateTrackingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDataSyncToolStripMenuItem;
    }
}



