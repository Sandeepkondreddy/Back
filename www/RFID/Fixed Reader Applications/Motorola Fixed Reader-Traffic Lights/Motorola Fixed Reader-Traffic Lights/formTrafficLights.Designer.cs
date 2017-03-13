namespace Motorola_Fixed_Reader_Traffic_Lights
{
    partial class formTrafficLights
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formTrafficLights));
            this.txtReaderPort = new System.Windows.Forms.Label();
            this.lblReaderHdr = new System.Windows.Forms.Label();
            this.lblReaderPort = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblReaderIP = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtReaderIP = new System.Windows.Forms.Label();
            this.connectBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.functionCallStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.connectionStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.readButton = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtReaderPort
            // 
            this.txtReaderPort.AutoSize = true;
            this.txtReaderPort.Font = new System.Drawing.Font("Verdana", 8.75F);
            this.txtReaderPort.ForeColor = System.Drawing.Color.Maroon;
            this.txtReaderPort.Location = new System.Drawing.Point(584, 55);
            this.txtReaderPort.Name = "txtReaderPort";
            this.txtReaderPort.Size = new System.Drawing.Size(82, 14);
            this.txtReaderPort.TabIndex = 241;
            this.txtReaderPort.Text = "Reader Port";
            // 
            // lblReaderHdr
            // 
            this.lblReaderHdr.AutoSize = true;
            this.lblReaderHdr.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderHdr.Location = new System.Drawing.Point(21, 53);
            this.lblReaderHdr.Name = "lblReaderHdr";
            this.lblReaderHdr.Size = new System.Drawing.Size(122, 16);
            this.lblReaderHdr.TabIndex = 239;
            this.lblReaderHdr.Text = "Reader Details :";
            // 
            // lblReaderPort
            // 
            this.lblReaderPort.AutoSize = true;
            this.lblReaderPort.Font = new System.Drawing.Font("Verdana", 8.75F);
            this.lblReaderPort.Location = new System.Drawing.Point(445, 54);
            this.lblReaderPort.Name = "lblReaderPort";
            this.lblReaderPort.Size = new System.Drawing.Size(82, 14);
            this.lblReaderPort.TabIndex = 237;
            this.lblReaderPort.Text = "Reader Port";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.75F);
            this.label10.Location = new System.Drawing.Point(539, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 14);
            this.label10.TabIndex = 238;
            this.label10.Text = ":";
            // 
            // lblReaderIP
            // 
            this.lblReaderIP.AutoSize = true;
            this.lblReaderIP.Font = new System.Drawing.Font("Verdana", 8.75F);
            this.lblReaderIP.Location = new System.Drawing.Point(147, 54);
            this.lblReaderIP.Name = "lblReaderIP";
            this.lblReaderIP.Size = new System.Drawing.Size(69, 14);
            this.lblReaderIP.TabIndex = 235;
            this.lblReaderIP.Text = "Reader IP";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.75F);
            this.label13.Location = new System.Drawing.Point(241, 54);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(12, 14);
            this.label13.TabIndex = 236;
            this.label13.Text = ":";
            // 
            // txtReaderIP
            // 
            this.txtReaderIP.AutoSize = true;
            this.txtReaderIP.Font = new System.Drawing.Font("Verdana", 8.75F);
            this.txtReaderIP.ForeColor = System.Drawing.Color.Maroon;
            this.txtReaderIP.Location = new System.Drawing.Point(286, 55);
            this.txtReaderIP.Name = "txtReaderIP";
            this.txtReaderIP.Size = new System.Drawing.Size(69, 14);
            this.txtReaderIP.TabIndex = 240;
            this.txtReaderIP.Text = "Reader IP";
            // 
            // connectBackgroundWorker
            // 
            this.connectBackgroundWorker.WorkerReportsProgress = true;
            this.connectBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.connectBackgroundWorker_DoWork);
            this.connectBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.connectBackgroundWorker_RunWorkerCompleted);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionCallStatusLabel,
            this.connectionStatusLabel,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel1,
            this.connectionStatus});
            this.statusStrip.Location = new System.Drawing.Point(0, 389);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(788, 25);
            this.statusStrip.TabIndex = 242;
            this.statusStrip.Text = "statusStrip";
            // 
            // functionCallStatusLabel
            // 
            this.functionCallStatusLabel.AutoSize = false;
            this.functionCallStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.functionCallStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.functionCallStatusLabel.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.functionCallStatusLabel.Name = "functionCallStatusLabel";
            this.functionCallStatusLabel.Size = new System.Drawing.Size(716, 20);
            this.functionCallStatusLabel.Text = "Ready";
            this.functionCallStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // connectionStatusLabel
            // 
            this.connectionStatusLabel.Name = "connectionStatusLabel";
            this.connectionStatusLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AutoSize = false;
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel1.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusLabel1.Margin = new System.Windows.Forms.Padding(2, 3, 0, 2);
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(716, 20);
            this.toolStripStatusLabel1.Text = "Ready";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // connectionStatus
            // 
            this.connectionStatus.AutoSize = false;
            this.connectionStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.connectionStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.connectionStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.connectionStatus.Name = "connectionStatus";
            this.connectionStatus.Size = new System.Drawing.Size(50, 20);
            this.connectionStatus.Text = "Disconnected";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.AutoSize = false;
            this.toolStripStatusLabel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(50, 20);
            this.toolStripStatusLabel2.Text = "Disconnected";
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(682, 50);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(94, 23);
            this.readButton.TabIndex = 243;
            this.readButton.Text = "Start Reading";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // formTrafficLights
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 414);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.txtReaderPort);
            this.Controls.Add(this.lblReaderHdr);
            this.Controls.Add(this.lblReaderPort);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblReaderIP);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtReaderIP);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formTrafficLights";
            this.Text = "formTrafficLights";
            this.Load += new System.EventHandler(this.formTrafficLights_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formTrafficLights_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtReaderPort;
        private System.Windows.Forms.Label lblReaderHdr;
        private System.Windows.Forms.Label lblReaderPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblReaderIP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label txtReaderIP;
        internal System.ComponentModel.BackgroundWorker connectBackgroundWorker;
        private System.Windows.Forms.StatusStrip statusStrip;
        internal System.Windows.Forms.ToolStripStatusLabel functionCallStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
        internal System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button readButton;
    }
}