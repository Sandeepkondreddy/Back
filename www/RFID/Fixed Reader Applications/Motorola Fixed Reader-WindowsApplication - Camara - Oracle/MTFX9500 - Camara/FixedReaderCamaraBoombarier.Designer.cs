namespace CS_RFID3_Host_Sample1
{
    partial class FixedReaderCamaraBoombarier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FixedReaderCamaraBoombarier));
            this.dataGridViewTagDetails = new System.Windows.Forms.DataGridView();
            this.lblNetworkStatusText = new System.Windows.Forms.Label();
            this.lblErrorMessageText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblReaderPortText = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblParkingArea = new System.Windows.Forms.Label();
            this.lblReaderPort = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblReaderIP = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblReaderStatusText = new System.Windows.Forms.Label();
            this.lblReaderStatus_Parking = new System.Windows.Forms.Label();
            this.lblReaderIPText = new System.Windows.Forms.Label();
            this.timerReader = new System.Windows.Forms.Timer(this.components);
            this.timerNetworkConnection = new System.Windows.Forms.Timer(this.components);
            this.pictureBoxImgatLoc1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxImgatLoc2 = new System.Windows.Forms.PictureBox();
            this.lblLoc1 = new System.Windows.Forms.Label();
            this.lblLoc2 = new System.Windows.Forms.Label();
            this.Antenna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TagDetails = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AntennaStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BoomBarrierStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTagDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgatLoc1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgatLoc2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewTagDetails
            // 
            this.dataGridViewTagDetails.AllowUserToAddRows = false;
            this.dataGridViewTagDetails.AllowUserToDeleteRows = false;
            this.dataGridViewTagDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTagDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Antenna,
            this.TagDetails,
            this.ReadTime,
            this.AntennaStatus,
            this.BoomBarrierStatus});
            this.dataGridViewTagDetails.Location = new System.Drawing.Point(6, 91);
            this.dataGridViewTagDetails.Name = "dataGridViewTagDetails";
            this.dataGridViewTagDetails.ReadOnly = true;
            this.dataGridViewTagDetails.Size = new System.Drawing.Size(743, 111);
            this.dataGridViewTagDetails.TabIndex = 260;
            // 
            // lblNetworkStatusText
            // 
            this.lblNetworkStatusText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetworkStatusText.ForeColor = System.Drawing.Color.Maroon;
            this.lblNetworkStatusText.Location = new System.Drawing.Point(154, 436);
            this.lblNetworkStatusText.Name = "lblNetworkStatusText";
            this.lblNetworkStatusText.Size = new System.Drawing.Size(471, 18);
            this.lblNetworkStatusText.TabIndex = 259;
            // 
            // lblErrorMessageText
            // 
            this.lblErrorMessageText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessageText.ForeColor = System.Drawing.Color.Maroon;
            this.lblErrorMessageText.Location = new System.Drawing.Point(363, 75);
            this.lblErrorMessageText.Name = "lblErrorMessageText";
            this.lblErrorMessageText.Size = new System.Drawing.Size(373, 18);
            this.lblErrorMessageText.TabIndex = 258;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(9, 438);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 257;
            this.label1.Text = "Network Status : ";
            // 
            // lblReaderPortText
            // 
            this.lblReaderPortText.AutoSize = true;
            this.lblReaderPortText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPortText.Location = new System.Drawing.Point(199, 72);
            this.lblReaderPortText.Name = "lblReaderPortText";
            this.lblReaderPortText.Size = new System.Drawing.Size(85, 16);
            this.lblReaderPortText.TabIndex = 256;
            this.lblReaderPortText.Text = "Reader Port";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.Black;
            this.btnDisconnect.Location = new System.Drawing.Point(577, 42);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(131, 30);
            this.btnDisconnect.TabIndex = 252;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnConnect.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(413, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(138, 30);
            this.btnConnect.TabIndex = 246;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblParkingArea
            // 
            this.lblParkingArea.AutoSize = true;
            this.lblParkingArea.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParkingArea.Location = new System.Drawing.Point(41, 6);
            this.lblParkingArea.Name = "lblParkingArea";
            this.lblParkingArea.Size = new System.Drawing.Size(160, 16);
            this.lblParkingArea.TabIndex = 251;
            this.lblParkingArea.Text = "RFID Reader Details :";
            // 
            // lblReaderPort
            // 
            this.lblReaderPort.AutoSize = true;
            this.lblReaderPort.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort.Location = new System.Drawing.Point(60, 71);
            this.lblReaderPort.Name = "lblReaderPort";
            this.lblReaderPort.Size = new System.Drawing.Size(85, 16);
            this.lblReaderPort.TabIndex = 249;
            this.lblReaderPort.Text = "Reader Port";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(154, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 16);
            this.label10.TabIndex = 250;
            this.label10.Text = ":";
            // 
            // lblReaderIP
            // 
            this.lblReaderIP.AutoSize = true;
            this.lblReaderIP.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP.Location = new System.Drawing.Point(60, 42);
            this.lblReaderIP.Name = "lblReaderIP";
            this.lblReaderIP.Size = new System.Drawing.Size(71, 16);
            this.lblReaderIP.TabIndex = 247;
            this.lblReaderIP.Text = "Reader IP";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(154, 42);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 16);
            this.label13.TabIndex = 248;
            this.label13.Text = ":";
            // 
            // lblReaderStatusText
            // 
            this.lblReaderStatusText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatusText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblReaderStatusText.Location = new System.Drawing.Point(537, 6);
            this.lblReaderStatusText.Name = "lblReaderStatusText";
            this.lblReaderStatusText.Size = new System.Drawing.Size(171, 18);
            this.lblReaderStatusText.TabIndex = 254;
            this.lblReaderStatusText.Text = "# Not Connected";
            // 
            // lblReaderStatus_Parking
            // 
            this.lblReaderStatus_Parking.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus_Parking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblReaderStatus_Parking.Location = new System.Drawing.Point(410, 6);
            this.lblReaderStatus_Parking.Name = "lblReaderStatus_Parking";
            this.lblReaderStatus_Parking.Size = new System.Drawing.Size(121, 16);
            this.lblReaderStatus_Parking.TabIndex = 253;
            this.lblReaderStatus_Parking.Text = "Reader Status :";
            // 
            // lblReaderIPText
            // 
            this.lblReaderIPText.AutoSize = true;
            this.lblReaderIPText.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIPText.Location = new System.Drawing.Point(199, 43);
            this.lblReaderIPText.Name = "lblReaderIPText";
            this.lblReaderIPText.Size = new System.Drawing.Size(71, 16);
            this.lblReaderIPText.TabIndex = 255;
            this.lblReaderIPText.Text = "Reader IP";
            // 
            // timerReader
            // 
            this.timerReader.Tick += new System.EventHandler(this.timerReader_Tick);
            // 
            // timerNetworkConnection
            // 
            this.timerNetworkConnection.Tick += new System.EventHandler(this.timerNetworkConnection_Tick);
            // 
            // pictureBoxImgatLoc1
            // 
            this.pictureBoxImgatLoc1.Location = new System.Drawing.Point(113, 213);
            this.pictureBoxImgatLoc1.Name = "pictureBoxImgatLoc1";
            this.pictureBoxImgatLoc1.Size = new System.Drawing.Size(225, 205);
            this.pictureBoxImgatLoc1.TabIndex = 261;
            this.pictureBoxImgatLoc1.TabStop = false;
            // 
            // pictureBoxImgatLoc2
            // 
            this.pictureBoxImgatLoc2.Location = new System.Drawing.Point(360, 213);
            this.pictureBoxImgatLoc2.Name = "pictureBoxImgatLoc2";
            this.pictureBoxImgatLoc2.Size = new System.Drawing.Size(225, 205);
            this.pictureBoxImgatLoc2.TabIndex = 262;
            this.pictureBoxImgatLoc2.TabStop = false;
            // 
            // lblLoc1
            // 
            this.lblLoc1.AutoSize = true;
            this.lblLoc1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblLoc1.Location = new System.Drawing.Point(189, 420);
            this.lblLoc1.Name = "lblLoc1";
            this.lblLoc1.Size = new System.Drawing.Size(37, 13);
            this.lblLoc1.TabIndex = 263;
            this.lblLoc1.Text = "Loc1";
            // 
            // lblLoc2
            // 
            this.lblLoc2.AutoSize = true;
            this.lblLoc2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoc2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblLoc2.Location = new System.Drawing.Point(427, 420);
            this.lblLoc2.Name = "lblLoc2";
            this.lblLoc2.Size = new System.Drawing.Size(37, 13);
            this.lblLoc2.TabIndex = 264;
            this.lblLoc2.Text = "Loc2";
            // 
            // Antenna
            // 
            this.Antenna.HeaderText = "Antenna";
            this.Antenna.Name = "Antenna";
            this.Antenna.ReadOnly = true;
            this.Antenna.Width = 60;
            // 
            // TagDetails
            // 
            this.TagDetails.HeaderText = "Tag Details";
            this.TagDetails.Name = "TagDetails";
            this.TagDetails.ReadOnly = true;
            this.TagDetails.Width = 90;
            // 
            // ReadTime
            // 
            this.ReadTime.HeaderText = "Read Time";
            this.ReadTime.Name = "ReadTime";
            this.ReadTime.ReadOnly = true;
            this.ReadTime.Width = 150;
            // 
            // AntennaStatus
            // 
            this.AntennaStatus.HeaderText = "Antenna Status";
            this.AntennaStatus.Name = "AntennaStatus";
            this.AntennaStatus.ReadOnly = true;
            this.AntennaStatus.Width = 280;
            // 
            // BoomBarrierStatus
            // 
            this.BoomBarrierStatus.HeaderText = "BoomBarrier Status";
            this.BoomBarrierStatus.Name = "BoomBarrierStatus";
            this.BoomBarrierStatus.ReadOnly = true;
            this.BoomBarrierStatus.Width = 120;
            // 
            // FixedReaderCamaraBoombarier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 463);
            this.Controls.Add(this.lblLoc2);
            this.Controls.Add(this.lblLoc1);
            this.Controls.Add(this.pictureBoxImgatLoc2);
            this.Controls.Add(this.pictureBoxImgatLoc1);
            this.Controls.Add(this.dataGridViewTagDetails);
            this.Controls.Add(this.lblNetworkStatusText);
            this.Controls.Add(this.lblErrorMessageText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblReaderPortText);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblParkingArea);
            this.Controls.Add(this.lblReaderPort);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblReaderIP);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblReaderStatusText);
            this.Controls.Add(this.lblReaderStatus_Parking);
            this.Controls.Add(this.lblReaderIPText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FixedReaderCamaraBoombarier";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FixedReaderCamara";
            this.Load += new System.EventHandler(this.FixedReaderCamara_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTagDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgatLoc1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImgatLoc2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewTagDetails;
        private System.Windows.Forms.Label lblNetworkStatusText;
        private System.Windows.Forms.Label lblErrorMessageText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblReaderPortText;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblParkingArea;
        private System.Windows.Forms.Label lblReaderPort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblReaderIP;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblReaderStatusText;
        private System.Windows.Forms.Label lblReaderStatus_Parking;
        private System.Windows.Forms.Label lblReaderIPText;
        private System.Windows.Forms.Timer timerReader;
        private System.Windows.Forms.Timer timerNetworkConnection;
        private System.Windows.Forms.PictureBox pictureBoxImgatLoc1;
        private System.Windows.Forms.PictureBox pictureBoxImgatLoc2;
        private System.Windows.Forms.Label lblLoc1;
        private System.Windows.Forms.Label lblLoc2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Antenna;
        private System.Windows.Forms.DataGridViewTextBoxColumn TagDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReadTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn AntennaStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn BoomBarrierStatus;
    }
}