namespace QuickStartDemo
{
    partial class frmEM01Demo
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbIp1 = new System.Windows.Forms.TextBox();
            this.tbIp2 = new System.Windows.Forms.TextBox();
            this.tbIp4 = new System.Windows.Forms.TextBox();
            this.tbIp3 = new System.Windows.Forms.TextBox();
            this.gbConnect = new System.Windows.Forms.GroupBox();
            this.gbControl = new System.Windows.Forms.GroupBox();
            this.gbParameter = new System.Windows.Forms.GroupBox();
            this.laProgMode = new System.Windows.Forms.Label();
            this.btnProgMode = new System.Windows.Forms.Button();
            this.udSpeed = new System.Windows.Forms.NumericUpDown();
            this.btnSpeed = new System.Windows.Forms.Button();
            this.gbStatus = new System.Windows.Forms.GroupBox();
            this.laBoomAngle = new System.Windows.Forms.Label();
            this.laStatus = new System.Windows.Forms.Label();
            this.btnStatus = new System.Windows.Forms.Button();
            this.gbOutputs = new System.Windows.Forms.GroupBox();
            this.cbToggleRelay6 = new System.Windows.Forms.CheckBox();
            this.btnSetOut6Ext = new System.Windows.Forms.Button();
            this.gbErrors = new System.Windows.Forms.GroupBox();
            this.laErrText = new System.Windows.Forms.Label();
            this.laErrNr = new System.Windows.Forms.Label();
            this.btnReadErrors = new System.Windows.Forms.Button();
            this.udErrIdx = new System.Windows.Forms.NumericUpDown();
            this.laErrorNode = new System.Windows.Forms.Label();
            this.laErrorTimeStamp = new System.Windows.Forms.Label();
            this.gbConnect.SuspendLayout();
            this.gbControl.SuspendLayout();
            this.gbParameter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).BeginInit();
            this.gbStatus.SuspendLayout();
            this.gbOutputs.SuspendLayout();
            this.gbErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udErrIdx)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(16, 32);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(48, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(138, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tbIp1
            // 
            this.tbIp1.Location = new System.Drawing.Point(105, 35);
            this.tbIp1.Name = "tbIp1";
            this.tbIp1.Size = new System.Drawing.Size(28, 20);
            this.tbIp1.TabIndex = 3;
            this.tbIp1.Text = "192";
            // 
            // tbIp2
            // 
            this.tbIp2.Location = new System.Drawing.Point(139, 35);
            this.tbIp2.Name = "tbIp2";
            this.tbIp2.Size = new System.Drawing.Size(28, 20);
            this.tbIp2.TabIndex = 4;
            this.tbIp2.Text = "168";
            // 
            // tbIp4
            // 
            this.tbIp4.Location = new System.Drawing.Point(207, 35);
            this.tbIp4.Name = "tbIp4";
            this.tbIp4.Size = new System.Drawing.Size(28, 20);
            this.tbIp4.TabIndex = 6;
            this.tbIp4.Text = "168";
            // 
            // tbIp3
            // 
            this.tbIp3.Location = new System.Drawing.Point(173, 35);
            this.tbIp3.Name = "tbIp3";
            this.tbIp3.Size = new System.Drawing.Size(28, 20);
            this.tbIp3.TabIndex = 7;
            this.tbIp3.Text = "111";
            // 
            // gbConnect
            // 
            this.gbConnect.Controls.Add(this.btnConnect);
            this.gbConnect.Controls.Add(this.tbIp3);
            this.gbConnect.Controls.Add(this.tbIp4);
            this.gbConnect.Controls.Add(this.tbIp1);
            this.gbConnect.Controls.Add(this.tbIp2);
            this.gbConnect.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbConnect.Location = new System.Drawing.Point(10, 10);
            this.gbConnect.Name = "gbConnect";
            this.gbConnect.Size = new System.Drawing.Size(248, 70);
            this.gbConnect.TabIndex = 8;
            this.gbConnect.TabStop = false;
            this.gbConnect.Text = "Connection";
            // 
            // gbControl
            // 
            this.gbControl.Controls.Add(this.btnClose);
            this.gbControl.Controls.Add(this.btnOpen);
            this.gbControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbControl.Location = new System.Drawing.Point(10, 80);
            this.gbControl.Name = "gbControl";
            this.gbControl.Size = new System.Drawing.Size(248, 59);
            this.gbControl.TabIndex = 9;
            this.gbControl.TabStop = false;
            this.gbControl.Text = "Control";
            // 
            // gbParameter
            // 
            this.gbParameter.Controls.Add(this.laProgMode);
            this.gbParameter.Controls.Add(this.btnProgMode);
            this.gbParameter.Controls.Add(this.udSpeed);
            this.gbParameter.Controls.Add(this.btnSpeed);
            this.gbParameter.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbParameter.Location = new System.Drawing.Point(10, 298);
            this.gbParameter.Name = "gbParameter";
            this.gbParameter.Size = new System.Drawing.Size(248, 94);
            this.gbParameter.TabIndex = 10;
            this.gbParameter.TabStop = false;
            this.gbParameter.Text = "Parameter";
            // 
            // laProgMode
            // 
            this.laProgMode.AutoSize = true;
            this.laProgMode.Location = new System.Drawing.Point(170, 62);
            this.laProgMode.Name = "laProgMode";
            this.laProgMode.Size = new System.Drawing.Size(16, 13);
            this.laProgMode.TabIndex = 3;
            this.laProgMode.Text = "...";
            // 
            // btnProgMode
            // 
            this.btnProgMode.Location = new System.Drawing.Point(19, 57);
            this.btnProgMode.Name = "btnProgMode";
            this.btnProgMode.Size = new System.Drawing.Size(118, 23);
            this.btnProgMode.TabIndex = 2;
            this.btnProgMode.Text = "Read Prog. Mode";
            this.btnProgMode.UseVisualStyleBackColor = true;
            this.btnProgMode.Click += new System.EventHandler(this.btnProgMode_Click);
            // 
            // udSpeed
            // 
            this.udSpeed.Location = new System.Drawing.Point(160, 31);
            this.udSpeed.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udSpeed.Name = "udSpeed";
            this.udSpeed.Size = new System.Drawing.Size(54, 20);
            this.udSpeed.TabIndex = 1;
            this.udSpeed.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // btnSpeed
            // 
            this.btnSpeed.Location = new System.Drawing.Point(19, 28);
            this.btnSpeed.Name = "btnSpeed";
            this.btnSpeed.Size = new System.Drawing.Size(118, 23);
            this.btnSpeed.TabIndex = 0;
            this.btnSpeed.Text = "Set Close Speed";
            this.btnSpeed.UseVisualStyleBackColor = true;
            this.btnSpeed.Click += new System.EventHandler(this.btnSpeed_Click);
            // 
            // gbStatus
            // 
            this.gbStatus.Controls.Add(this.laBoomAngle);
            this.gbStatus.Controls.Add(this.laStatus);
            this.gbStatus.Controls.Add(this.btnStatus);
            this.gbStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbStatus.Location = new System.Drawing.Point(10, 139);
            this.gbStatus.Name = "gbStatus";
            this.gbStatus.Size = new System.Drawing.Size(248, 75);
            this.gbStatus.TabIndex = 2;
            this.gbStatus.TabStop = false;
            this.gbStatus.Text = "Status";
            // 
            // laBoomAngle
            // 
            this.laBoomAngle.AutoSize = true;
            this.laBoomAngle.Location = new System.Drawing.Point(136, 24);
            this.laBoomAngle.Name = "laBoomAngle";
            this.laBoomAngle.Size = new System.Drawing.Size(16, 13);
            this.laBoomAngle.TabIndex = 2;
            this.laBoomAngle.Text = "...";
            // 
            // laStatus
            // 
            this.laStatus.AutoSize = true;
            this.laStatus.Location = new System.Drawing.Point(31, 47);
            this.laStatus.Name = "laStatus";
            this.laStatus.Size = new System.Drawing.Size(16, 13);
            this.laStatus.TabIndex = 1;
            this.laStatus.Text = "...";
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(19, 19);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(87, 23);
            this.btnStatus.TabIndex = 0;
            this.btnStatus.Text = "Update Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // gbOutputs
            // 
            this.gbOutputs.Controls.Add(this.cbToggleRelay6);
            this.gbOutputs.Controls.Add(this.btnSetOut6Ext);
            this.gbOutputs.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbOutputs.Location = new System.Drawing.Point(10, 214);
            this.gbOutputs.Name = "gbOutputs";
            this.gbOutputs.Size = new System.Drawing.Size(248, 84);
            this.gbOutputs.TabIndex = 4;
            this.gbOutputs.TabStop = false;
            this.gbOutputs.Text = "Output Control";
            // 
            // cbToggleRelay6
            // 
            this.cbToggleRelay6.AutoSize = true;
            this.cbToggleRelay6.Location = new System.Drawing.Point(61, 60);
            this.cbToggleRelay6.Name = "cbToggleRelay6";
            this.cbToggleRelay6.Size = new System.Drawing.Size(98, 17);
            this.cbToggleRelay6.TabIndex = 1;
            this.cbToggleRelay6.Text = "Toggle Relay 6";
            this.cbToggleRelay6.UseVisualStyleBackColor = true;
            this.cbToggleRelay6.CheckedChanged += new System.EventHandler(this.cbToggleRelay6_CheckedChanged);
            // 
            // btnSetOut6Ext
            // 
            this.btnSetOut6Ext.Location = new System.Drawing.Point(48, 20);
            this.btnSetOut6Ext.Name = "btnSetOut6Ext";
            this.btnSetOut6Ext.Size = new System.Drawing.Size(165, 23);
            this.btnSetOut6Ext.TabIndex = 0;
            this.btnSetOut6Ext.Text = "Set Out 6 External";
            this.btnSetOut6Ext.UseVisualStyleBackColor = true;
            this.btnSetOut6Ext.Click += new System.EventHandler(this.btnSetOut6Ext_Click);
            // 
            // gbErrors
            // 
            this.gbErrors.Controls.Add(this.laErrorTimeStamp);
            this.gbErrors.Controls.Add(this.laErrorNode);
            this.gbErrors.Controls.Add(this.udErrIdx);
            this.gbErrors.Controls.Add(this.laErrText);
            this.gbErrors.Controls.Add(this.laErrNr);
            this.gbErrors.Controls.Add(this.btnReadErrors);
            this.gbErrors.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbErrors.Location = new System.Drawing.Point(10, 392);
            this.gbErrors.Name = "gbErrors";
            this.gbErrors.Size = new System.Drawing.Size(248, 104);
            this.gbErrors.TabIndex = 11;
            this.gbErrors.TabStop = false;
            this.gbErrors.Text = "Errors";
            // 
            // laErrText
            // 
            this.laErrText.AutoSize = true;
            this.laErrText.Location = new System.Drawing.Point(58, 79);
            this.laErrText.Name = "laErrText";
            this.laErrText.Size = new System.Drawing.Size(16, 13);
            this.laErrText.TabIndex = 2;
            this.laErrText.Text = "...";
            // 
            // laErrNr
            // 
            this.laErrNr.AutoSize = true;
            this.laErrNr.Location = new System.Drawing.Point(16, 79);
            this.laErrNr.Name = "laErrNr";
            this.laErrNr.Size = new System.Drawing.Size(16, 13);
            this.laErrNr.TabIndex = 1;
            this.laErrNr.Text = "...";
            // 
            // btnReadErrors
            // 
            this.btnReadErrors.Location = new System.Drawing.Point(19, 19);
            this.btnReadErrors.Name = "btnReadErrors";
            this.btnReadErrors.Size = new System.Drawing.Size(87, 23);
            this.btnReadErrors.TabIndex = 0;
            this.btnReadErrors.Text = "Read Error";
            this.btnReadErrors.UseVisualStyleBackColor = true;
            this.btnReadErrors.Click += new System.EventHandler(this.btnReadErrors_Click);
            // 
            // udErrIdx
            // 
            this.udErrIdx.Location = new System.Drawing.Point(160, 19);
            this.udErrIdx.Maximum = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.udErrIdx.Name = "udErrIdx";
            this.udErrIdx.Size = new System.Drawing.Size(54, 20);
            this.udErrIdx.TabIndex = 3;
            // 
            // laErrorNode
            // 
            this.laErrorNode.AutoSize = true;
            this.laErrorNode.Location = new System.Drawing.Point(143, 61);
            this.laErrorNode.Name = "laErrorNode";
            this.laErrorNode.Size = new System.Drawing.Size(16, 13);
            this.laErrorNode.TabIndex = 4;
            this.laErrorNode.Text = "...";
            // 
            // laErrorTimeStamp
            // 
            this.laErrorTimeStamp.AutoSize = true;
            this.laErrorTimeStamp.Location = new System.Drawing.Point(16, 61);
            this.laErrorTimeStamp.Name = "laErrorTimeStamp";
            this.laErrorTimeStamp.Size = new System.Drawing.Size(16, 13);
            this.laErrorTimeStamp.TabIndex = 5;
            this.laErrorTimeStamp.Text = "...";
            // 
            // frmEM01Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 505);
            this.Controls.Add(this.gbErrors);
            this.Controls.Add(this.gbParameter);
            this.Controls.Add(this.gbOutputs);
            this.Controls.Add(this.gbStatus);
            this.Controls.Add(this.gbControl);
            this.Controls.Add(this.gbConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmEM01Demo";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "EM01 Driver Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEM01Demo_FormClosing);
            this.gbConnect.ResumeLayout(false);
            this.gbConnect.PerformLayout();
            this.gbControl.ResumeLayout(false);
            this.gbParameter.ResumeLayout(false);
            this.gbParameter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udSpeed)).EndInit();
            this.gbStatus.ResumeLayout(false);
            this.gbStatus.PerformLayout();
            this.gbOutputs.ResumeLayout(false);
            this.gbOutputs.PerformLayout();
            this.gbErrors.ResumeLayout(false);
            this.gbErrors.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udErrIdx)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbIp1;
        private System.Windows.Forms.TextBox tbIp2;
        private System.Windows.Forms.TextBox tbIp4;
        private System.Windows.Forms.TextBox tbIp3;
        private System.Windows.Forms.GroupBox gbConnect;
        private System.Windows.Forms.GroupBox gbControl;
        private System.Windows.Forms.GroupBox gbParameter;
        private System.Windows.Forms.NumericUpDown udSpeed;
        private System.Windows.Forms.Button btnSpeed;
        private System.Windows.Forms.GroupBox gbStatus;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Label laStatus;
        private System.Windows.Forms.Label laBoomAngle;
        private System.Windows.Forms.Button btnProgMode;
        private System.Windows.Forms.Label laProgMode;
        private System.Windows.Forms.GroupBox gbOutputs;
        private System.Windows.Forms.Button btnSetOut6Ext;
        private System.Windows.Forms.CheckBox cbToggleRelay6;
        private System.Windows.Forms.GroupBox gbErrors;
        private System.Windows.Forms.Label laErrorTimeStamp;
        private System.Windows.Forms.Label laErrorNode;
        private System.Windows.Forms.NumericUpDown udErrIdx;
        private System.Windows.Forms.Label laErrText;
        private System.Windows.Forms.Label laErrNr;
        private System.Windows.Forms.Button btnReadErrors;
    }
}

