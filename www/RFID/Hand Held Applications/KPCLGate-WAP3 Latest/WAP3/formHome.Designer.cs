namespace WAP3
{
    partial class formHome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formHome));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.imageButtonRoad = new Resco.Controls.OutlookControls.ImageButton();
            this.imageButtonConfig = new Resco.Controls.OutlookControls.ImageButton();
            this.imageButtonInternal = new Resco.Controls.OutlookControls.ImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonRoad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonInternal)).BeginInit();
            this.SuspendLayout();
            // 
            // imageButtonRoad
            // 
            this.imageButtonRoad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.imageButtonRoad.ImageDefault = ((System.Drawing.Image)(resources.GetObject("imageButtonRoad.ImageDefault")));
            this.imageButtonRoad.Location = new System.Drawing.Point(22, 187);
            this.imageButtonRoad.Name = "imageButtonRoad";
            this.imageButtonRoad.Size = new System.Drawing.Size(44, 39);
            this.imageButtonRoad.TabIndex = 0;
            this.imageButtonRoad.TextDefault = "Road";
            // 
            // imageButtonConfig
            // 
            this.imageButtonConfig.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.imageButtonConfig.ImageDefault = ((System.Drawing.Image)(resources.GetObject("imageButtonConfig.ImageDefault")));
            this.imageButtonConfig.Location = new System.Drawing.Point(168, 187);
            this.imageButtonConfig.Name = "imageButtonConfig";
            this.imageButtonConfig.Size = new System.Drawing.Size(44, 39);
            this.imageButtonConfig.TabIndex = 1;
            this.imageButtonConfig.TextDefault = "Config";
            this.imageButtonConfig.Click += new System.EventHandler(this.imageButtonConfig_Click);
            // 
            // imageButtonInternal
            // 
            this.imageButtonInternal.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.imageButtonInternal.ImageDefault = ((System.Drawing.Image)(resources.GetObject("imageButtonInternal.ImageDefault")));
            this.imageButtonInternal.Location = new System.Drawing.Point(95, 187);
            this.imageButtonInternal.Name = "imageButtonInternal";
            this.imageButtonInternal.Size = new System.Drawing.Size(44, 39);
            this.imageButtonInternal.TabIndex = 2;
            this.imageButtonInternal.TextDefault = "Intra";
            this.imageButtonInternal.Click += new System.EventHandler(this.imageButtonInternal_Click);
            // 
            // formHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.imageButtonInternal);
            this.Controls.Add(this.imageButtonConfig);
            this.Controls.Add(this.imageButtonRoad);
            this.Menu = this.mainMenu1;
            this.Name = "formHome";
            this.Text = "Home";
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonRoad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageButtonInternal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Resco.Controls.OutlookControls.ImageButton imageButtonRoad;
        private Resco.Controls.OutlookControls.ImageButton imageButtonConfig;
        private Resco.Controls.OutlookControls.ImageButton imageButtonInternal;

    }
}