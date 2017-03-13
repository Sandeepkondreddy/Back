﻿namespace CS_RFID3_Host_Sample1
{
	partial class TestEPMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestEPMS));
            this.lblWBOUT = new System.Windows.Forms.Label();
            this.lblWBIN = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.tmrParkingArea = new System.Windows.Forms.Timer(this.components);
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAntennaStatus_ParkingIn_Text = new System.Windows.Forms.Label();
            this.lblReaderPort_Parking_Text = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnParkingAreaReaderDisconnect = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblAntenna_ParkingIn_Text = new System.Windows.Forms.Label();
            this.lblAntenna_ParkingIn = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_ParkingIn_Text = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_ParkingIn = new System.Windows.Forms.Label();
            this.btnParkingAreaReaderConnect = new System.Windows.Forms.Button();
            this.lblParkingArea = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.clearReports_CB = new System.Windows.Forms.CheckBox();
            this.lblAntenna_ParkingOut_Time_Text = new System.Windows.Forms.Label();
            this.lblAntenna_ParkingIn_Time_Text = new System.Windows.Forms.Label();
            this.lblReaderStatus_Parking_Text = new System.Windows.Forms.Label();
            this.lblReaderStatus_Parking = new System.Windows.Forms.Label();
            this.lblParkingOutAntenna = new System.Windows.Forms.Label();
            this.lblParkingInAntenna = new System.Windows.Forms.Label();
            this.lblAntennaStatus_ParkingOut_Text = new System.Windows.Forms.Label();
            this.lblReaderIP_Parking_Text = new System.Windows.Forms.Label();
            this.lblAntennaStatus_ParkingOut = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblAntenna_ParkingOut_Text = new System.Windows.Forms.Label();
            this.lblAntenna_ParkingOut = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_ParkingOut_Text = new System.Windows.Forms.Label();
            this.lblRFIDTagValue_ParkingOut = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWBOUT
            // 
            this.lblWBOUT.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBOUT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblWBOUT.Location = new System.Drawing.Point(217, 267);
            this.lblWBOUT.Name = "lblWBOUT";
            this.lblWBOUT.Size = new System.Drawing.Size(76, 26);
            this.lblWBOUT.TabIndex = 281;
            // 
            // lblWBIN
            // 
            this.lblWBIN.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWBIN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblWBIN.Location = new System.Drawing.Point(204, 140);
            this.lblWBIN.Name = "lblWBIN";
            this.lblWBIN.Size = new System.Drawing.Size(76, 26);
            this.lblWBIN.TabIndex = 282;
            // 
            // lblmsg
            // 
            this.lblmsg.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Maroon;
            this.lblmsg.Location = new System.Drawing.Point(235, 465);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(171, 18);
            this.lblmsg.TabIndex = 279;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.Black;
            this.lblVersion.Location = new System.Drawing.Point(715, 468);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(39, 18);
            this.lblVersion.TabIndex = 280;
            // 
            // tmrParkingArea
            // 
            this.tmrParkingArea.Tick += new System.EventHandler(this.tmrParkingArea_Tick);
            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Maroon;
            this.lblErrorMessage.Location = new System.Drawing.Point(235, 413);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(471, 18);
            this.lblErrorMessage.TabIndex = 278;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(90, 467);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 16);
            this.label1.TabIndex = 277;
            this.label1.Text = "Network Status : ";
            // 
            // lblAntennaStatus_ParkingIn_Text
            // 
            this.lblAntennaStatus_ParkingIn_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaStatus_ParkingIn_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaStatus_ParkingIn_Text.Location = new System.Drawing.Point(235, 229);
            this.lblAntennaStatus_ParkingIn_Text.Name = "lblAntennaStatus_ParkingIn_Text";
            this.lblAntennaStatus_ParkingIn_Text.Size = new System.Drawing.Size(471, 36);
            this.lblAntennaStatus_ParkingIn_Text.TabIndex = 276;
            this.lblAntennaStatus_ParkingIn_Text.Text = "-";
            // 
            // lblReaderPort_Parking_Text
            // 
            this.lblReaderPort_Parking_Text.AutoSize = true;
            this.lblReaderPort_Parking_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderPort_Parking_Text.Location = new System.Drawing.Point(197, 100);
            this.lblReaderPort_Parking_Text.Name = "lblReaderPort_Parking_Text";
            this.lblReaderPort_Parking_Text.Size = new System.Drawing.Size(85, 16);
            this.lblReaderPort_Parking_Text.TabIndex = 275;
            this.lblReaderPort_Parking_Text.Text = "Reader Port";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(90, 229);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 16);
            this.label4.TabIndex = 259;
            this.label4.Text = "Antenna Status :";
            // 
            // btnParkingAreaReaderDisconnect
            // 
            this.btnParkingAreaReaderDisconnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnParkingAreaReaderDisconnect.Enabled = false;
            this.btnParkingAreaReaderDisconnect.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParkingAreaReaderDisconnect.ForeColor = System.Drawing.Color.Black;
            this.btnParkingAreaReaderDisconnect.Location = new System.Drawing.Point(575, 71);
            this.btnParkingAreaReaderDisconnect.Name = "btnParkingAreaReaderDisconnect";
            this.btnParkingAreaReaderDisconnect.Size = new System.Drawing.Size(131, 30);
            this.btnParkingAreaReaderDisconnect.TabIndex = 258;
            this.btnParkingAreaReaderDisconnect.Text = "Disconnect";
            this.btnParkingAreaReaderDisconnect.UseVisualStyleBackColor = false;
            this.btnParkingAreaReaderDisconnect.Click += new System.EventHandler(this.btnParkingAreaReaderDisconnect_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(197, 168);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 16);
            this.label5.TabIndex = 257;
            this.label5.Text = ":";
            // 
            // lblAntenna_ParkingIn_Text
            // 
            this.lblAntenna_ParkingIn_Text.AutoSize = true;
            this.lblAntenna_ParkingIn_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_ParkingIn_Text.Location = new System.Drawing.Point(217, 168);
            this.lblAntenna_ParkingIn_Text.Name = "lblAntenna_ParkingIn_Text";
            this.lblAntenna_ParkingIn_Text.Size = new System.Drawing.Size(0, 16);
            this.lblAntenna_ParkingIn_Text.TabIndex = 256;
            // 
            // lblAntenna_ParkingIn
            // 
            this.lblAntenna_ParkingIn.AutoSize = true;
            this.lblAntenna_ParkingIn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_ParkingIn.Location = new System.Drawing.Point(101, 168);
            this.lblAntenna_ParkingIn.Name = "lblAntenna_ParkingIn";
            this.lblAntenna_ParkingIn.Size = new System.Drawing.Size(63, 16);
            this.lblAntenna_ParkingIn.TabIndex = 255;
            this.lblAntenna_ParkingIn.Text = "Antenna";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(197, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 16);
            this.label6.TabIndex = 254;
            this.label6.Text = ":";
            // 
            // lblRFIDTagValue_ParkingIn_Text
            // 
            this.lblRFIDTagValue_ParkingIn_Text.AutoSize = true;
            this.lblRFIDTagValue_ParkingIn_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_ParkingIn_Text.Location = new System.Drawing.Point(217, 195);
            this.lblRFIDTagValue_ParkingIn_Text.Name = "lblRFIDTagValue_ParkingIn_Text";
            this.lblRFIDTagValue_ParkingIn_Text.Size = new System.Drawing.Size(0, 16);
            this.lblRFIDTagValue_ParkingIn_Text.TabIndex = 253;
            // 
            // lblRFIDTagValue_ParkingIn
            // 
            this.lblRFIDTagValue_ParkingIn.AutoSize = true;
            this.lblRFIDTagValue_ParkingIn.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_ParkingIn.Location = new System.Drawing.Point(101, 195);
            this.lblRFIDTagValue_ParkingIn.Name = "lblRFIDTagValue_ParkingIn";
            this.lblRFIDTagValue_ParkingIn.Size = new System.Drawing.Size(91, 16);
            this.lblRFIDTagValue_ParkingIn.TabIndex = 252;
            this.lblRFIDTagValue_ParkingIn.Text = "Readed Tag ";
            // 
            // btnParkingAreaReaderConnect
            // 
            this.btnParkingAreaReaderConnect.BackColor = System.Drawing.SystemColors.Control;
            this.btnParkingAreaReaderConnect.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParkingAreaReaderConnect.ForeColor = System.Drawing.Color.Black;
            this.btnParkingAreaReaderConnect.Location = new System.Drawing.Point(411, 71);
            this.btnParkingAreaReaderConnect.Name = "btnParkingAreaReaderConnect";
            this.btnParkingAreaReaderConnect.Size = new System.Drawing.Size(138, 30);
            this.btnParkingAreaReaderConnect.TabIndex = 246;
            this.btnParkingAreaReaderConnect.Text = "Connect";
            this.btnParkingAreaReaderConnect.UseVisualStyleBackColor = false;
            this.btnParkingAreaReaderConnect.Click += new System.EventHandler(this.btnParkingAreaReaderConnect_Click);
            // 
            // lblParkingArea
            // 
            this.lblParkingArea.AutoSize = true;
            this.lblParkingArea.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParkingArea.Location = new System.Drawing.Point(39, 28);
            this.lblParkingArea.Name = "lblParkingArea";
            this.lblParkingArea.Size = new System.Drawing.Size(226, 16);
            this.lblParkingArea.TabIndex = 251;
            this.lblParkingArea.Text = "WB Area RFID Reader Details :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(58, 100);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 16);
            this.label9.TabIndex = 249;
            this.label9.Text = "Reader Port";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(152, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 16);
            this.label10.TabIndex = 250;
            this.label10.Text = ":";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(58, 71);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(71, 16);
            this.label12.TabIndex = 247;
            this.label12.Text = "Reader IP";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(152, 71);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 16);
            this.label13.TabIndex = 248;
            this.label13.Text = ":";
            // 
            // clearReports_CB
            // 
            this.clearReports_CB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearReports_CB.AutoSize = true;
            this.clearReports_CB.Checked = true;
            this.clearReports_CB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.clearReports_CB.Location = new System.Drawing.Point(1024, 23);
            this.clearReports_CB.Name = "clearReports_CB";
            this.clearReports_CB.Size = new System.Drawing.Size(50, 17);
            this.clearReports_CB.TabIndex = 245;
            this.clearReports_CB.Text = "Clear";
            this.clearReports_CB.UseVisualStyleBackColor = true;
            // 
            // lblAntenna_ParkingOut_Time_Text
            // 
            this.lblAntenna_ParkingOut_Time_Text.AutoSize = true;
            this.lblAntenna_ParkingOut_Time_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_ParkingOut_Time_Text.Location = new System.Drawing.Point(318, 307);
            this.lblAntenna_ParkingOut_Time_Text.Name = "lblAntenna_ParkingOut_Time_Text";
            this.lblAntenna_ParkingOut_Time_Text.Size = new System.Drawing.Size(15, 16);
            this.lblAntenna_ParkingOut_Time_Text.TabIndex = 273;
            this.lblAntenna_ParkingOut_Time_Text.Text = "-";
            // 
            // lblAntenna_ParkingIn_Time_Text
            // 
            this.lblAntenna_ParkingIn_Time_Text.AutoSize = true;
            this.lblAntenna_ParkingIn_Time_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_ParkingIn_Time_Text.Location = new System.Drawing.Point(319, 168);
            this.lblAntenna_ParkingIn_Time_Text.Name = "lblAntenna_ParkingIn_Time_Text";
            this.lblAntenna_ParkingIn_Time_Text.Size = new System.Drawing.Size(15, 16);
            this.lblAntenna_ParkingIn_Time_Text.TabIndex = 272;
            this.lblAntenna_ParkingIn_Time_Text.Text = "-";
            // 
            // lblReaderStatus_Parking_Text
            // 
            this.lblReaderStatus_Parking_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus_Parking_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblReaderStatus_Parking_Text.Location = new System.Drawing.Point(535, 28);
            this.lblReaderStatus_Parking_Text.Name = "lblReaderStatus_Parking_Text";
            this.lblReaderStatus_Parking_Text.Size = new System.Drawing.Size(171, 18);
            this.lblReaderStatus_Parking_Text.TabIndex = 271;
            this.lblReaderStatus_Parking_Text.Text = "# Not Connected";
            // 
            // lblReaderStatus_Parking
            // 
            this.lblReaderStatus_Parking.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderStatus_Parking.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.lblReaderStatus_Parking.Location = new System.Drawing.Point(408, 28);
            this.lblReaderStatus_Parking.Name = "lblReaderStatus_Parking";
            this.lblReaderStatus_Parking.Size = new System.Drawing.Size(121, 16);
            this.lblReaderStatus_Parking.TabIndex = 270;
            this.lblReaderStatus_Parking.Text = "Reader Status :";
            // 
            // lblParkingOutAntenna
            // 
            this.lblParkingOutAntenna.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParkingOutAntenna.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblParkingOutAntenna.Location = new System.Drawing.Point(58, 265);
            this.lblParkingOutAntenna.Name = "lblParkingOutAntenna";
            this.lblParkingOutAntenna.Size = new System.Drawing.Size(191, 16);
            this.lblParkingOutAntenna.TabIndex = 269;
            this.lblParkingOutAntenna.Text = "# WB Out - Antenna";
            // 
            // lblParkingInAntenna
            // 
            this.lblParkingInAntenna.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParkingInAntenna.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblParkingInAntenna.Location = new System.Drawing.Point(58, 139);
            this.lblParkingInAntenna.Name = "lblParkingInAntenna";
            this.lblParkingInAntenna.Size = new System.Drawing.Size(191, 16);
            this.lblParkingInAntenna.TabIndex = 268;
            this.lblParkingInAntenna.Text = "# WB In - Antenna";
            // 
            // lblAntennaStatus_ParkingOut_Text
            // 
            this.lblAntennaStatus_ParkingOut_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaStatus_ParkingOut_Text.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaStatus_ParkingOut_Text.Location = new System.Drawing.Point(235, 364);
            this.lblAntennaStatus_ParkingOut_Text.Name = "lblAntennaStatus_ParkingOut_Text";
            this.lblAntennaStatus_ParkingOut_Text.Size = new System.Drawing.Size(499, 36);
            this.lblAntennaStatus_ParkingOut_Text.TabIndex = 267;
            this.lblAntennaStatus_ParkingOut_Text.Text = "-";
            // 
            // lblReaderIP_Parking_Text
            // 
            this.lblReaderIP_Parking_Text.AutoSize = true;
            this.lblReaderIP_Parking_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReaderIP_Parking_Text.Location = new System.Drawing.Point(197, 71);
            this.lblReaderIP_Parking_Text.Name = "lblReaderIP_Parking_Text";
            this.lblReaderIP_Parking_Text.Size = new System.Drawing.Size(71, 16);
            this.lblReaderIP_Parking_Text.TabIndex = 274;
            this.lblReaderIP_Parking_Text.Text = "Reader IP";
            // 
            // lblAntennaStatus_ParkingOut
            // 
            this.lblAntennaStatus_ParkingOut.AutoSize = true;
            this.lblAntennaStatus_ParkingOut.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntennaStatus_ParkingOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAntennaStatus_ParkingOut.Location = new System.Drawing.Point(90, 364);
            this.lblAntennaStatus_ParkingOut.Name = "lblAntennaStatus_ParkingOut";
            this.lblAntennaStatus_ParkingOut.Size = new System.Drawing.Size(121, 16);
            this.lblAntennaStatus_ParkingOut.TabIndex = 266;
            this.lblAntennaStatus_ParkingOut.Text = "Antenna Status :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(197, 298);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 16);
            this.label11.TabIndex = 265;
            this.label11.Text = ":";
            // 
            // lblAntenna_ParkingOut_Text
            // 
            this.lblAntenna_ParkingOut_Text.AutoSize = true;
            this.lblAntenna_ParkingOut_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_ParkingOut_Text.Location = new System.Drawing.Point(217, 300);
            this.lblAntenna_ParkingOut_Text.Name = "lblAntenna_ParkingOut_Text";
            this.lblAntenna_ParkingOut_Text.Size = new System.Drawing.Size(0, 16);
            this.lblAntenna_ParkingOut_Text.TabIndex = 264;
            // 
            // lblAntenna_ParkingOut
            // 
            this.lblAntenna_ParkingOut.AutoSize = true;
            this.lblAntenna_ParkingOut.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAntenna_ParkingOut.Location = new System.Drawing.Point(103, 298);
            this.lblAntenna_ParkingOut.Name = "lblAntenna_ParkingOut";
            this.lblAntenna_ParkingOut.Size = new System.Drawing.Size(63, 16);
            this.lblAntenna_ParkingOut.TabIndex = 263;
            this.lblAntenna_ParkingOut.Text = "Antenna";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(197, 328);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 16);
            this.label14.TabIndex = 262;
            this.label14.Text = ":";
            // 
            // lblRFIDTagValue_ParkingOut_Text
            // 
            this.lblRFIDTagValue_ParkingOut_Text.AutoSize = true;
            this.lblRFIDTagValue_ParkingOut_Text.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_ParkingOut_Text.Location = new System.Drawing.Point(217, 337);
            this.lblRFIDTagValue_ParkingOut_Text.Name = "lblRFIDTagValue_ParkingOut_Text";
            this.lblRFIDTagValue_ParkingOut_Text.Size = new System.Drawing.Size(0, 16);
            this.lblRFIDTagValue_ParkingOut_Text.TabIndex = 261;
            // 
            // lblRFIDTagValue_ParkingOut
            // 
            this.lblRFIDTagValue_ParkingOut.AutoSize = true;
            this.lblRFIDTagValue_ParkingOut.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFIDTagValue_ParkingOut.Location = new System.Drawing.Point(103, 328);
            this.lblRFIDTagValue_ParkingOut.Name = "lblRFIDTagValue_ParkingOut";
            this.lblRFIDTagValue_ParkingOut.Size = new System.Drawing.Size(91, 16);
            this.lblRFIDTagValue_ParkingOut.TabIndex = 260;
            this.lblRFIDTagValue_ParkingOut.Text = "Readed Tag ";
            // 
            // TestEPMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 502);
            this.Controls.Add(this.lblWBOUT);
            this.Controls.Add(this.lblWBIN);
            this.Controls.Add(this.lblmsg);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAntennaStatus_ParkingIn_Text);
            this.Controls.Add(this.lblReaderPort_Parking_Text);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnParkingAreaReaderDisconnect);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblAntenna_ParkingIn_Text);
            this.Controls.Add(this.lblAntenna_ParkingIn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblRFIDTagValue_ParkingIn_Text);
            this.Controls.Add(this.lblRFIDTagValue_ParkingIn);
            this.Controls.Add(this.btnParkingAreaReaderConnect);
            this.Controls.Add(this.lblParkingArea);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.clearReports_CB);
            this.Controls.Add(this.lblAntenna_ParkingOut_Time_Text);
            this.Controls.Add(this.lblAntenna_ParkingIn_Time_Text);
            this.Controls.Add(this.lblReaderStatus_Parking_Text);
            this.Controls.Add(this.lblReaderStatus_Parking);
            this.Controls.Add(this.lblParkingOutAntenna);
            this.Controls.Add(this.lblParkingInAntenna);
            this.Controls.Add(this.lblAntennaStatus_ParkingOut_Text);
            this.Controls.Add(this.lblReaderIP_Parking_Text);
            this.Controls.Add(this.lblAntennaStatus_ParkingOut);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblAntenna_ParkingOut_Text);
            this.Controls.Add(this.lblAntenna_ParkingOut);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblRFIDTagValue_ParkingOut_Text);
            this.Controls.Add(this.lblRFIDTagValue_ParkingOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TestEPMS";
            this.Text = "TestEPMS";
            this.Load += new System.EventHandler(this.TestEPMS_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label lblWBOUT;
        private System.Windows.Forms.Label lblWBIN;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Timer tmrParkingArea;
        private System.Windows.Forms.Label lblErrorMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAntennaStatus_ParkingIn_Text;
        private System.Windows.Forms.Label lblReaderPort_Parking_Text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnParkingAreaReaderDisconnect;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblAntenna_ParkingIn_Text;
        private System.Windows.Forms.Label lblAntenna_ParkingIn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRFIDTagValue_ParkingIn_Text;
        private System.Windows.Forms.Label lblRFIDTagValue_ParkingIn;
        private System.Windows.Forms.Button btnParkingAreaReaderConnect;
        private System.Windows.Forms.Label lblParkingArea;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox clearReports_CB;
        private System.Windows.Forms.Label lblAntenna_ParkingOut_Time_Text;
        private System.Windows.Forms.Label lblAntenna_ParkingIn_Time_Text;
        private System.Windows.Forms.Label lblReaderStatus_Parking_Text;
        private System.Windows.Forms.Label lblReaderStatus_Parking;
        private System.Windows.Forms.Label lblParkingOutAntenna;
        private System.Windows.Forms.Label lblParkingInAntenna;
        private System.Windows.Forms.Label lblAntennaStatus_ParkingOut_Text;
        private System.Windows.Forms.Label lblReaderIP_Parking_Text;
        private System.Windows.Forms.Label lblAntennaStatus_ParkingOut;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblAntenna_ParkingOut_Text;
        private System.Windows.Forms.Label lblAntenna_ParkingOut;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblRFIDTagValue_ParkingOut_Text;
        private System.Windows.Forms.Label lblRFIDTagValue_ParkingOut;

    }
}