namespace WindowsApp
{
    partial class formIALDiscrepancy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formIALDiscrepancy));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.rptIALReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.IALReport1 = new WindowsApp.IALReport();
            this.lblVesRef = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblVesRefVal = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnBowse = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtIALfilePath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblIALfile = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.rptIALReport);
            this.kryptonPanel.Controls.Add(this.lblVesRef);
            this.kryptonPanel.Controls.Add(this.lblVesRefVal);
            this.kryptonPanel.Controls.Add(this.btnBowse);
            this.kryptonPanel.Controls.Add(this.txtIALfilePath);
            this.kryptonPanel.Controls.Add(this.lblIALfile);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel.TabIndex = 0;
            // 
            // rptIALReport
            // 
            this.rptIALReport.ActiveViewIndex = 0;
            this.rptIALReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rptIALReport.DisplayGroupTree = false;
            this.rptIALReport.Location = new System.Drawing.Point(3, 94);
            this.rptIALReport.Name = "rptIALReport";
            this.rptIALReport.ReportSource = this.IALReport1;
            this.rptIALReport.Size = new System.Drawing.Size(836, 522);
            this.rptIALReport.TabIndex = 25;
            this.rptIALReport.Visible = false;
            // 
            // lblVesRef
            // 
            this.lblVesRef.Location = new System.Drawing.Point(56, 72);
            this.lblVesRef.Name = "lblVesRef";
            this.lblVesRef.Size = new System.Drawing.Size(87, 20);
            this.lblVesRef.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblVesRef.TabIndex = 24;
            this.lblVesRef.Values.Text = "Vessel Name :";
            // 
            // lblVesRefVal
            // 
            this.lblVesRefVal.Location = new System.Drawing.Point(147, 72);
            this.lblVesRefVal.Name = "lblVesRefVal";
            this.lblVesRefVal.Size = new System.Drawing.Size(6, 2);
            this.lblVesRefVal.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblVesRefVal.TabIndex = 23;
            this.lblVesRefVal.Values.Text = "";
            // 
            // btnBowse
            // 
            this.btnBowse.Location = new System.Drawing.Point(631, 42);
            this.btnBowse.Name = "btnBowse";
            this.btnBowse.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnBowse.Size = new System.Drawing.Size(70, 24);
            this.btnBowse.TabIndex = 22;
            this.btnBowse.Values.Text = "Browse . . .";
            this.btnBowse.Click += new System.EventHandler(this.btnBowse_Click);
            // 
            // txtIALfilePath
            // 
            this.txtIALfilePath.Enabled = false;
            this.txtIALfilePath.Location = new System.Drawing.Point(147, 44);
            this.txtIALfilePath.Name = "txtIALfilePath";
            this.txtIALfilePath.Size = new System.Drawing.Size(488, 20);
            this.txtIALfilePath.TabIndex = 21;
            // 
            // lblIALfile
            // 
            this.lblIALfile.Location = new System.Drawing.Point(56, 47);
            this.lblIALfile.Name = "lblIALfile";
            this.lblIALfile.Size = new System.Drawing.Size(56, 20);
            this.lblIALfile.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblIALfile.StateNormal.LongText.Color1 = System.Drawing.Color.White;
            this.lblIALfile.TabIndex = 20;
            this.lblIALfile.Values.Text = "IAL File :";
            // 
            // formIALDiscrepancy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formIALDiscrepancy";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.Text = "IAL - Discrepancy Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formIALDiscrepancy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblVesRef;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblVesRefVal;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBowse;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtIALfilePath;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblIALfile;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rptIALReport;
        private IALReport IALReport1;
    }
}

