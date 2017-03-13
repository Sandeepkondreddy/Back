namespace WAP3
{
    partial class formUpdateTime
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSetTime
            // 
            this.btnSetTime.Location = new System.Drawing.Point(88, 45);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(72, 20);
            this.btnSetTime.TabIndex = 0;
            this.btnSetTime.Text = "Set Time";
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // formUpdateTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.btnSetTime);
            this.Menu = this.mainMenu1;
            this.Name = "formUpdateTime";
            this.Text = "Update Time";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSetTime;
    }
}