namespace SendingFiles
{
    partial class GenerateExcelFileandUploadtoFTPManual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateExcelFileandUploadtoFTPManual));
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.lblReportStatus = new System.Windows.Forms.Label();
            this.lblSendingStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblErrorMsg = new System.Windows.Forms.Label();
            this.PresentTimer = new System.Windows.Forms.Timer(this.components);
            this.lblCopiedTime = new System.Windows.Forms.Label();
            this.lblMailStatus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
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
            this.groupBox7.Location = new System.Drawing.Point(17, 37);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(664, 76);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "FTP Configurations";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(444, 33);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(189, 21);
            this.txtPassword.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(441, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(225, 33);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(189, 21);
            this.txtUsername.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(222, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Username";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Enabled = false;
            this.txtServerIP.Location = new System.Drawing.Point(6, 33);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.Size = new System.Drawing.Size(189, 21);
            this.txtServerIP.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 15);
            this.label4.TabIndex = 4;
            this.label4.Text = "Server IP";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblTime.Location = new System.Drawing.Point(492, 10);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 15);
            this.lblTime.TabIndex = 19;
            // 
            // btnGenerate
            // 
            this.btnGenerate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnGenerate.Location = new System.Drawing.Point(243, 121);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(118, 27);
            this.btnGenerate.TabIndex = 24;
            this.btnGenerate.Text = "Generate Report";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblReportStatus
            // 
            this.lblReportStatus.AutoSize = true;
            this.lblReportStatus.Location = new System.Drawing.Point(239, 182);
            this.lblReportStatus.Name = "lblReportStatus";
            this.lblReportStatus.Size = new System.Drawing.Size(22, 15);
            this.lblReportStatus.TabIndex = 9;
            this.lblReportStatus.Text = " ---";
            // 
            // lblSendingStatus
            // 
            this.lblSendingStatus.AutoSize = true;
            this.lblSendingStatus.Location = new System.Drawing.Point(239, 228);
            this.lblSendingStatus.Name = "lblSendingStatus";
            this.lblSendingStatus.Size = new System.Drawing.Size(22, 15);
            this.lblSendingStatus.TabIndex = 25;
            this.lblSendingStatus.Text = " ---";
            // 
            // btnStart
            // 
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStart.Location = new System.Drawing.Point(380, 121);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(118, 27);
            this.btnStart.TabIndex = 26;
            this.btnStart.Text = "Start Sending";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblErrorMsg
            // 
            this.lblErrorMsg.AutoSize = true;
            this.lblErrorMsg.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblErrorMsg.Location = new System.Drawing.Point(29, 323);
            this.lblErrorMsg.Name = "lblErrorMsg";
            this.lblErrorMsg.Size = new System.Drawing.Size(0, 22);
            this.lblErrorMsg.TabIndex = 27;
            // 
            // PresentTimer
            // 
            this.PresentTimer.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lblCopiedTime
            // 
            this.lblCopiedTime.AutoSize = true;
            this.lblCopiedTime.Location = new System.Drawing.Point(240, 271);
            this.lblCopiedTime.Name = "lblCopiedTime";
            this.lblCopiedTime.Size = new System.Drawing.Size(22, 15);
            this.lblCopiedTime.TabIndex = 28;
            this.lblCopiedTime.Text = " ---";
            // 
            // lblMailStatus
            // 
            this.lblMailStatus.AutoSize = true;
            this.lblMailStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblMailStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMailStatus.Location = new System.Drawing.Point(472, 226);
            this.lblMailStatus.Name = "lblMailStatus";
            this.lblMailStatus.Size = new System.Drawing.Size(22, 15);
            this.lblMailStatus.TabIndex = 44;
            this.lblMailStatus.Text = " ---";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 269);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 15);
            this.label9.TabIndex = 50;
            this.label9.Text = "File Upload Time";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 15);
            this.label6.TabIndex = 49;
            this.label6.Text = "Upload Excel File Status";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(151, 15);
            this.label7.TabIndex = 45;
            this.label7.Text = "Generate Excel File Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(119, 358);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(366, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "© 2014 Krishnapatnam Port Company Limited, All rights reserved.";
            // 
            // GenerateExcelFileandUploadtoFTPManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaShell;
            this.ClientSize = new System.Drawing.Size(693, 377);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblMailStatus);
            this.Controls.Add(this.lblCopiedTime);
            this.Controls.Add(this.lblErrorMsg);
            this.Controls.Add(this.lblSendingStatus);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblReportStatus);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.lblTime);
            this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "GenerateExcelFileandUploadtoFTPManual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenerateExcelFileand UploadtoFTPManual";
            this.Load += new System.EventHandler(this.SendDatatoExcel_Load);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label lblReportStatus;
        private System.Windows.Forms.Label lblSendingStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblErrorMsg;
        private System.Windows.Forms.Timer PresentTimer;
        private System.Windows.Forms.Label lblCopiedTime;
        private System.Windows.Forms.Label lblMailStatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
    }
}