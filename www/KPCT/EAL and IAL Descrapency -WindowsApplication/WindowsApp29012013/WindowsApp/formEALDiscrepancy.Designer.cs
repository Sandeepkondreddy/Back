namespace WindowsApp
{
    partial class formEALDiscrepancy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formEALDiscrepancy));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.rptEALReport = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.CrystalReport11 = new WindowsApp.CrystalReport1();
            this.lblVesRef = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblVesRefVal = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.btnBowse = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.txtEALfilePath = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblEALfile = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.rptEALReport);
            this.kryptonPanel.Controls.Add(this.lblVesRef);
            this.kryptonPanel.Controls.Add(this.lblVesRefVal);
            this.kryptonPanel.Controls.Add(this.btnBowse);
            this.kryptonPanel.Controls.Add(this.txtEALfilePath);
            this.kryptonPanel.Controls.Add(this.lblEALfile);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.kryptonPanel.Size = new System.Drawing.Size(842, 616);
            this.kryptonPanel.TabIndex = 0;
            this.kryptonPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.kryptonPanel_Paint);
            // 
            // rptEALReport
            // 
            this.rptEALReport.ActiveViewIndex = 0;
            this.rptEALReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rptEALReport.DisplayGroupTree = false;
            this.rptEALReport.Location = new System.Drawing.Point(3, 91);
            this.rptEALReport.Name = "rptEALReport";
            this.rptEALReport.ReportSource = this.CrystalReport11;
            this.rptEALReport.Size = new System.Drawing.Size(836, 522);
            this.rptEALReport.TabIndex = 20;
            this.rptEALReport.Visible = false;
            // 
            // lblVesRef
            // 
            this.lblVesRef.Location = new System.Drawing.Point(38, 66);
            this.lblVesRef.Name = "lblVesRef";
            this.lblVesRef.Size = new System.Drawing.Size(80, 19);
            this.lblVesRef.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblVesRef.TabIndex = 19;
            this.lblVesRef.Values.Text = "Vessel Name :";
            // 
            // lblVesRefVal
            // 
            this.lblVesRefVal.Location = new System.Drawing.Point(129, 66);
            this.lblVesRefVal.Name = "lblVesRefVal";
            this.lblVesRefVal.Size = new System.Drawing.Size(6, 2);
            this.lblVesRefVal.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblVesRefVal.TabIndex = 18;
            this.lblVesRefVal.Values.Text = "";
            // 
            // btnBowse
            // 
            this.btnBowse.Location = new System.Drawing.Point(613, 38);
            this.btnBowse.Name = "btnBowse";
            this.btnBowse.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.btnBowse.Size = new System.Drawing.Size(70, 23);
            this.btnBowse.TabIndex = 17;
            this.btnBowse.Values.Text = "Browse . . .";
            this.btnBowse.Click += new System.EventHandler(this.btnBowse_Click);
            // 
            // txtEALfilePath
            // 
            this.txtEALfilePath.Enabled = false;
            this.txtEALfilePath.Location = new System.Drawing.Point(129, 38);
            this.txtEALfilePath.Name = "txtEALfilePath";
            this.txtEALfilePath.Size = new System.Drawing.Size(488, 22);
            this.txtEALfilePath.TabIndex = 1;
            // 
            // lblEALfile
            // 
            this.lblEALfile.Location = new System.Drawing.Point(38, 41);
            this.lblEALfile.Name = "lblEALfile";
            this.lblEALfile.Size = new System.Drawing.Size(54, 19);
            this.lblEALfile.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.lblEALfile.TabIndex = 0;
            this.lblEALfile.Values.Text = "EAL File :";
            // 
            // formEALDiscrepancy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 616);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formEALDiscrepancy";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.Text = "EAL - Discrepancy Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.formEALDiscrepancy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblEALfile;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtEALfilePath;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnBowse;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblVesRefVal;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblVesRef;
        private CrystalReport1 CrystalReport11;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer rptEALReport;
    }
}

