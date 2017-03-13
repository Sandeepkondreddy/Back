namespace CS_RFID3_Host_Sample1
{
    partial class formReader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formReader));
            this.kryptonManager = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonPanel = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnDisconnect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnConnect = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblReaderStatusValue = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblReaderStatus = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtReaderPort = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblReaderPort = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.txtReaderIP = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.lblReaderIP = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.lblReaderDetails = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.dataGridViewTagDetails = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.lblRaderInformation = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.Antenna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTagDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.lblRaderInformation);
            this.kryptonPanel.Controls.Add(this.dataGridViewTagDetails);
            this.kryptonPanel.Controls.Add(this.btnDisconnect);
            this.kryptonPanel.Controls.Add(this.btnConnect);
            this.kryptonPanel.Controls.Add(this.lblReaderStatusValue);
            this.kryptonPanel.Controls.Add(this.lblReaderStatus);
            this.kryptonPanel.Controls.Add(this.txtReaderPort);
            this.kryptonPanel.Controls.Add(this.lblReaderPort);
            this.kryptonPanel.Controls.Add(this.txtReaderIP);
            this.kryptonPanel.Controls.Add(this.lblReaderIP);
            this.kryptonPanel.Controls.Add(this.lblReaderDetails);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 0);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.kryptonPanel.Size = new System.Drawing.Size(731, 425);
            this.kryptonPanel.TabIndex = 0;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(543, 70);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.btnDisconnect.Size = new System.Drawing.Size(90, 25);
            this.btnDisconnect.TabIndex = 17;
            this.btnDisconnect.Values.Text = "Disconnect";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(405, 70);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.btnConnect.Size = new System.Drawing.Size(90, 25);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Values.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblReaderStatusValue
            // 
            this.lblReaderStatusValue.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.ItalicControl;
            this.lblReaderStatusValue.Location = new System.Drawing.Point(521, 13);
            this.lblReaderStatusValue.Name = "lblReaderStatusValue";
            this.lblReaderStatusValue.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblReaderStatusValue.Size = new System.Drawing.Size(94, 20);
            this.lblReaderStatusValue.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusValue.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusValue.StateNormal.LongText.Color1 = System.Drawing.Color.Maroon;
            this.lblReaderStatusValue.StateNormal.LongText.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblReaderStatusValue.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusValue.TabIndex = 15;
            this.lblReaderStatusValue.Values.Text = "# Reader Status";
            // 
            // lblReaderStatus
            // 
            this.lblReaderStatus.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblReaderStatus.Location = new System.Drawing.Point(405, 13);
            this.lblReaderStatus.Name = "lblReaderStatus";
            this.lblReaderStatus.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblReaderStatus.Size = new System.Drawing.Size(97, 20);
            this.lblReaderStatus.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus.TabIndex = 14;
            this.lblReaderStatus.Values.Text = "Reader Status :";
            // 
            // txtReaderPort
            // 
            this.txtReaderPort.Enabled = false;
            this.txtReaderPort.Location = new System.Drawing.Point(127, 75);
            this.txtReaderPort.Name = "txtReaderPort";
            this.txtReaderPort.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.txtReaderPort.ReadOnly = true;
            this.txtReaderPort.Size = new System.Drawing.Size(130, 20);
            this.txtReaderPort.TabIndex = 13;
            this.txtReaderPort.Text = "Reader Port";
            // 
            // lblReaderPort
            // 
            this.lblReaderPort.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblReaderPort.Location = new System.Drawing.Point(12, 75);
            this.lblReaderPort.Name = "lblReaderPort";
            this.lblReaderPort.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblReaderPort.Size = new System.Drawing.Size(86, 20);
            this.lblReaderPort.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.TabIndex = 12;
            this.lblReaderPort.Values.Text = "Reader Port :";
            // 
            // txtReaderIP
            // 
            this.txtReaderIP.Enabled = false;
            this.txtReaderIP.Location = new System.Drawing.Point(127, 49);
            this.txtReaderIP.Name = "txtReaderIP";
            this.txtReaderIP.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.txtReaderIP.ReadOnly = true;
            this.txtReaderIP.Size = new System.Drawing.Size(130, 20);
            this.txtReaderIP.TabIndex = 11;
            this.txtReaderIP.Text = "ReaderIP";
            // 
            // lblReaderIP
            // 
            this.lblReaderIP.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldControl;
            this.lblReaderIP.Location = new System.Drawing.Point(12, 49);
            this.lblReaderIP.Name = "lblReaderIP";
            this.lblReaderIP.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblReaderIP.Size = new System.Drawing.Size(73, 20);
            this.lblReaderIP.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.TabIndex = 10;
            this.lblReaderIP.Values.Text = "Reader IP :";
            // 
            // lblReaderDetails
            // 
            this.lblReaderDetails.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblReaderDetails.Location = new System.Drawing.Point(12, 13);
            this.lblReaderDetails.Name = "lblReaderDetails";
            this.lblReaderDetails.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblReaderDetails.Size = new System.Drawing.Size(94, 20);
            this.lblReaderDetails.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderDetails.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderDetails.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderDetails.TabIndex = 9;
            this.lblReaderDetails.Values.Text = "Reader Details";
            // 
            // dataGridViewTagDetails
            // 
            this.dataGridViewTagDetails.AllowUserToAddRows = false;
            this.dataGridViewTagDetails.AllowUserToDeleteRows = false;
            this.dataGridViewTagDetails.AllowUserToOrderColumns = true;
            this.dataGridViewTagDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTagDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Antenna});
            this.dataGridViewTagDetails.Location = new System.Drawing.Point(12, 138);
            this.dataGridViewTagDetails.Name = "dataGridViewTagDetails";
            this.dataGridViewTagDetails.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparklePurple;
            this.dataGridViewTagDetails.ReadOnly = true;
            this.dataGridViewTagDetails.Size = new System.Drawing.Size(707, 231);
            this.dataGridViewTagDetails.TabIndex = 18;
            // 
            // lblRaderInformation
            // 
            this.lblRaderInformation.LabelStyle = ComponentFactory.Krypton.Toolkit.LabelStyle.BoldPanel;
            this.lblRaderInformation.Location = new System.Drawing.Point(12, 112);
            this.lblRaderInformation.Name = "lblRaderInformation";
            this.lblRaderInformation.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.lblRaderInformation.Size = new System.Drawing.Size(134, 20);
            this.lblRaderInformation.StateCommon.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaderInformation.StateDisabled.LongText.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaderInformation.StateNormal.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRaderInformation.TabIndex = 19;
            this.lblRaderInformation.Values.Text = "Reader Status Details";
            // 
            // Antenna
            // 
            this.Antenna.HeaderText = "Antenna";
            this.Antenna.Name = "Antenna";
            this.Antenna.ReadOnly = true;
            // 
            // formReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 425);
            this.Controls.Add(this.kryptonPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formReader";
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.SparkleOrange;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "&Fixed Reader";
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.kryptonPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTagDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ComponentFactory.Krypton.Toolkit.KryptonManager kryptonManager;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnDisconnect;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnConnect;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderStatusValue;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderStatus;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtReaderPort;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderPort;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtReaderIP;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderIP;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblReaderDetails;
        private ComponentFactory.Krypton.Toolkit.KryptonLabel lblRaderInformation;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dataGridViewTagDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn Antenna;
    }
}

