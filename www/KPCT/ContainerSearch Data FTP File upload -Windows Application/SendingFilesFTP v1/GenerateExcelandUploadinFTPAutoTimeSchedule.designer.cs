namespace SendingFiles
{
    partial class GenerateExcelandUploadinFTPAutoTimeSchedule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateExcelandUploadinFTPAutoTimeSchedule));
            this.lblCopiedTime = new System.Windows.Forms.Label();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.lblSendingStatus = new System.Windows.Forms.Label();
            this.lblReportStatus = new System.Windows.Forms.Label();
            this.UploadTimer = new System.Windows.Forms.Timer(this.components);
            this.PresentTimer = new System.Windows.Forms.Timer(this.components);
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStopSending = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblMailStatus = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCopiedTime
            // 
            this.lblCopiedTime.AutoSize = true;
            this.lblCopiedTime.BackColor = System.Drawing.Color.Transparent;
            this.lblCopiedTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopiedTime.Location = new System.Drawing.Point(204, 244);
            this.lblCopiedTime.Name = "lblCopiedTime";
            this.lblCopiedTime.Size = new System.Drawing.Size(22, 15);
            this.lblCopiedTime.TabIndex = 38;
            this.lblCopiedTime.Text = " ---";
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblErrorMsg.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorMsg.Location = new System.Drawing.Point(24, 281);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(0, 15);
            this.lblErrorMsg.TabIndex = 37;
            // 
            // lblSendingStatus
            // 
            this.lblSendingStatus.AutoSize = true;
            this.lblSendingStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblSendingStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendingStatus.Location = new System.Drawing.Point(203, 204);
            this.lblSendingStatus.Name = "lblSendingStatus";
            this.lblSendingStatus.Size = new System.Drawing.Size(22, 15);
            this.lblSendingStatus.TabIndex = 35;
            this.lblSendingStatus.Text = " ---";
            // 
            // lblReportStatus
            // 
            this.lblReportStatus.AutoSize = true;
            this.lblReportStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblReportStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportStatus.Location = new System.Drawing.Point(203, 166);
            this.lblReportStatus.Name = "lblReportStatus";
            this.lblReportStatus.Size = new System.Drawing.Size(22, 15);
            this.lblReportStatus.TabIndex = 29;
            this.lblReportStatus.Text = " ---";
            // 
            // UploadTimer
            // 
            this.UploadTimer.Tick += new System.EventHandler(this.UploadTimer_Tick);
            // 
            // PresentTimer
            // 
            this.PresentTimer.Tick += new System.EventHandler(this.PresentTimer_Tick);
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(381, 31);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(165, 21);
            this.txtPassword.TabIndex = 7;
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.Transparent;
            this.groupBox7.Controls.Add(this.txtPassword);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Controls.Add(this.txtUsername);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.txtServerIP);
            this.groupBox7.Controls.Add(this.label4);
            this.groupBox7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.Location = new System.Drawing.Point(13, 40);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(569, 71);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "FTP Configurations";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(378, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(193, 31);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(165, 21);
            this.txtUsername.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Enabled = false;
            this.txtServerIP.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerIP.Location = new System.Drawing.Point(5, 31);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(165, 21);
            this.txtServerIP.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Server IP";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblTime.Location = new System.Drawing.Point(420, 15);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 15);
            this.lblTime.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Generate Excel File Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 15);
            this.label6.TabIndex = 39;
            this.label6.Text = "Upload Excel File Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(109, 310);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(366, 15);
            this.label7.TabIndex = 40;
            this.label7.Text = "© 2014 Krishnapatnam Port Company Limited, All rights reserved.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(557, 311);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 9;
            this.label8.Text = "V 1.0";
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Transparent;
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStart.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(18, 117);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(210, 25);
            this.btnStart.TabIndex = 41;
            this.btnStart.Text = "Start Generating Excel and Sending File";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStopSending
            // 
            this.btnStopSending.BackColor = System.Drawing.Color.Transparent;
            this.btnStopSending.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStopSending.Enabled = false;
            this.btnStopSending.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopSending.Location = new System.Drawing.Point(250, 117);
            this.btnStopSending.Name = "btnStopSending";
            this.btnStopSending.Size = new System.Drawing.Size(210, 25);
            this.btnStopSending.TabIndex = 41;
            this.btnStopSending.Text = "Stop Generating and Sending File";
            this.btnStopSending.UseVisualStyleBackColor = false;
            this.btnStopSending.Click += new System.EventHandler(this.btnStopSending_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 241);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 15);
            this.label9.TabIndex = 42;
            this.label9.Text = "File Upload Time";
            // 
            // lblMailStatus
            // 
            this.lblMailStatus.AutoSize = true;
            this.lblMailStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblMailStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMailStatus.Location = new System.Drawing.Point(398, 204);
            this.lblMailStatus.Name = "lblMailStatus";
            this.lblMailStatus.Size = new System.Drawing.Size(22, 15);
            this.lblMailStatus.TabIndex = 43;
            this.lblMailStatus.Text = " ---";
            // 
            // GenerateExcelandUploadinFTPAutoTimeSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Linen;
            this.BackgroundImage = global::SendingFiles.Properties.Resources.LogoutBg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(594, 327);
            this.Controls.Add(this.lblMailStatus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnStopSending);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCopiedTime);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblSendingStatus);
            this.Controls.Add(this.lblReportStatus);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lblTime);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GenerateExcelandUploadinFTPAutoTimeSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenerateExcelandUploadinFTPAutoTimeSchedule";
            this.Load += new System.EventHandler(this.GenerateExcelandUploadinFTPAutoTimeSchedule_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCopiedTime;
        private System.Windows.Forms.Label lblErrorMsg;
        private System.Windows.Forms.Label lblSendingStatus;
        private System.Windows.Forms.Label lblReportStatus;
        private System.Windows.Forms.Timer UploadTimer;
        private System.Windows.Forms.Timer PresentTimer;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStopSending;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblMailStatus;
    }
}